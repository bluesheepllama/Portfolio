// =================== EXAMPLE CAMERA CODE
//
// Display a color cube, a color tube, and view them with a moving camera
//

#include <unistd.h>
#include <cstdio>
#include <cstdlib>
#include <cmath>
#include <iostream>
using namespace std;

#include <GLUT/glut.h>

#include "Angel.h"

typedef Angel::vec4  color4;
typedef Angel::vec4  point4;

GLuint loadBMP_custom(unsigned char** data, int* w, int* h, const char * imagepath);



//---- some variables for our camera
//----------------------------------------------------------------------------

// array of rotation angles (in degrees) for each axis
enum { Xaxis = 0, Yaxis = 1, Zaxis = 2, NumAxes = 3 };
int      Axis = Zaxis;
GLfloat  Theta1[NumAxes] = { 0.0, 0.0, 0.0 };

float r = 4.0;

mat4 view_matrix, default_view_matrix;
mat4 proj_matrix;

bool spherical_cam_on = false;


//---- cube model
//----------------------------------------------------------------------------
const int NumVerticesCube = 36; //(6 faces)(2 triangles/face)(3 vertices/triangle)
point4 points_cube[NumVerticesCube];
color4 colors[NumVerticesCube];
vec3   normals[NumVerticesCube];
vec2   tex_coords[NumVerticesCube];

// Vertices of a unit cube centered at origin, sides aligned with axes
point4 vertices[8] = {
    point4( -0.5, -0.5,  0.5, 1.0 ),
    point4( -0.5,  0.5,  0.5, 1.0 ),
    point4(  0.5,  0.5,  0.5, 1.0 ),
    point4(  0.5, -0.5,  0.5, 1.0 ),
    point4( -0.5, -0.5, -0.5, 1.0 ),
    point4( -0.5,  0.5, -0.5, 1.0 ),
    point4(  0.5,  0.5, -0.5, 1.0 ),
    point4(  0.5, -0.5, -0.5, 1.0 )
};

// RGBA colors
color4 vertex_colors[8] = {
    color4( 0.1, 0.1, 0.1, 1.0 ),  // black
    color4( 1.0, 0.0, 0.0, 1.0 ),  // red
    color4( 1.0, 1.0, 0.0, 1.0 ),  // yellow
    color4( 0.0, 1.0, 0.0, 1.0 ),  // green
    color4( 0.0, 0.0, 1.0, 1.0 ),  // blue
    color4( 1.0, 0.0, 1.0, 1.0 ),  // magenta
    color4( 0.9, 0.9, 0.9, 1.0 ),  // white
    color4( 0.0, 1.0, 1.0, 1.0 )   // cyan
};

// quad generates two triangles for each face and assigns colors to the vertices
int Index;
void
quad_cube( int a, int b, int c, int d )
{
    // Initialize temporary vectors along the quad's edge to compute its face normal
    vec4 u = vertices[b] - vertices[a];
    vec4 v = vertices[c] - vertices[b];
    
    vec3 normal = normalize( cross(u, v) );
    
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[a]; tex_coords[Index] = vec2( 1.0, 1.0 ); Index++;
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[b]; tex_coords[Index] = vec2( 1.0, 0.0 ); Index++;
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[c]; tex_coords[Index] = vec2( 0.0, 0.0 ); Index++;
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[a]; tex_coords[Index] = vec2( 1.0, 1.0 ); Index++;
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[c]; tex_coords[Index] = vec2( 0.0, 0.0 ); Index++;
    normals[Index] = normal; colors[Index] = vertex_colors[a]; points_cube[Index] = vertices[d]; tex_coords[Index] = vec2( 0.0, 1.0 ); Index++;
}

// generate 12 triangles: 36 vertices and 36 colors
void
colorcube()
{
    Index = 0;
    quad_cube( 1, 0, 3, 2 );
    quad_cube( 2, 3, 7, 6 );
    quad_cube( 3, 0, 4, 7 );
    quad_cube( 6, 5, 1, 2 );
    quad_cube( 4, 5, 6, 7 );
    quad_cube( 5, 4, 0, 1 );
}


//---- cylinder model
//----------------------------------------------------------------------------
const int segments = 64;
const int NumVerticesCylinder = segments*6 + segments*3*2;
point4 points_cylinder[NumVerticesCylinder];
vec3   tnormals[NumVerticesCylinder];
point4 vertices_cylinder[4];

// quad generates two triangles for each face and assigns colors to the vertices
void
quad_cylinder( int a, int b, int c, int d )
{
    points_cylinder[Index] = vertices_cylinder[a];
    tnormals[Index] = vec3(vertices_cylinder[a].x, 0.0, vertices_cylinder[a].z); Index++;
    points_cylinder[Index] = vertices_cylinder[b];
    tnormals[Index] = vec3(vertices_cylinder[b].x, 0.0, vertices_cylinder[b].z); Index++;
    points_cylinder[Index] = vertices_cylinder[c];
    tnormals[Index] = vec3(vertices_cylinder[c].x, 0.0, vertices_cylinder[c].z); Index++;
    points_cylinder[Index] = vertices_cylinder[a];
    tnormals[Index] = vec3(vertices_cylinder[a].x, 0.0, vertices_cylinder[a].z); Index++;
    points_cylinder[Index] = vertices_cylinder[c];
    tnormals[Index] = vec3(vertices_cylinder[c].x, 0.0, vertices_cylinder[c].z); Index++;
    points_cylinder[Index] = vertices_cylinder[d];
    tnormals[Index] = vec3(vertices_cylinder[d].x, 0.0, vertices_cylinder[d].z); Index++;
}


float const bottom = -0.5;
float const top    = 0.5;

void
colortube(void)
{
    float r = 0.5;
    Index = 0;
    
    for ( int n = 0; n < segments; n++ )
    {
        GLfloat const t0 = 2 * M_PI * (float)n / (float)segments;
        GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)segments;
        
        points_cylinder[Index].x = 0.0;
        points_cylinder[Index].y = top;
        points_cylinder[Index].z = 0.0;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, 1.0, 0.0);
        Index++;
        points_cylinder[Index].x = cos(t0) * r;
        points_cylinder[Index].y = top;
        points_cylinder[Index].z = sin(t0) * r;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, 1.0, 0.0);
        Index++;
        points_cylinder[Index].x = cos(t1) * r;
        points_cylinder[Index].y = top;
        points_cylinder[Index].z = sin(t1) * r;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, 1.0, 0.0);
        Index++;
    }
    
    
    int num;
    for ( int n = 0; n < segments; n++ )
    {
        num = 0;
        float x = 0.0, y = 0.0, r = 0.5;
        
        GLfloat const t0 = 2 * M_PI * (float)n / (float)segments;
        GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)segments;
        
        //quad vertex 0
        vertices_cylinder[num].x = cos(t0) * r;
        vertices_cylinder[num].y = bottom;
        vertices_cylinder[num].z = sin(t0) * r;
        vertices_cylinder[num++].w = 1.0;
        //quad vertex 1
        vertices_cylinder[num].x = cos(t1) * r;
        vertices_cylinder[num].y = bottom;
        vertices_cylinder[num].z = sin(t1) * r;
        vertices_cylinder[num++].w = 1.0;
        //quad vertex 2
        vertices_cylinder[num].x = cos(t1) * r;
        vertices_cylinder[num].y = top;
        vertices_cylinder[num].z = sin(t1) * r;
        vertices_cylinder[num++].w = 1.0;
        //quad vertex 3
        vertices_cylinder[num].x = cos(t0) * r;
        vertices_cylinder[num].y = top;
        vertices_cylinder[num].z = sin(t0) * r;
        vertices_cylinder[num++].w = 1.0;
        
        quad_cylinder( 0, 1, 2, 3 );
    }
    
    for ( int n = 0; n < segments; n++ )
    {
        GLfloat const t0 = 2 * M_PI * (float)n / (float)segments;
        GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)segments;
        
        points_cylinder[Index].x = 0.0;
        points_cylinder[Index].y = bottom;
        points_cylinder[Index].z = 0.0;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, -1.0, 0.0);
        Index++;
        points_cylinder[Index].x = cos(t1) * r;
        points_cylinder[Index].y = bottom;
        points_cylinder[Index].z = sin(t1) * r;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, -1.0, 0.0);
        Index++;
        points_cylinder[Index].x = cos(t0) * r;
        points_cylinder[Index].y = bottom;
        points_cylinder[Index].z = sin(t0) * r;
        points_cylinder[Index].w = 1.0;
        tnormals[Index] = vec3(0.0, -1.0, 0.0);
        Index++;
    }
    
}


//---- sphere model
//----------------------------------------------------------------------------
const int ssegments = 512;
const int NumVerticesSphere = ssegments*6*(ssegments-2) + ssegments*3*2;
point4 points_sphere[NumVerticesSphere];
vec3   bnormals[NumVerticesSphere];
point4 vertices_sphere[4];
vec2   stex_coords[NumVerticesSphere];

// quad generates two triangles for each face and assigns colors to the vertices
void
quad_sphere( int a, int b, int c, int d , float t0, float t1, float p0, float p1)
{
    points_sphere[Index] = vertices_sphere[a];
    bnormals[Index] = vec3(vertices_sphere[a].x, vertices_sphere[a].y, vertices_sphere[a].z);
    stex_coords[Index] = vec2(t0/(2*M_PI), -(p1-M_PI/2.0)/M_PI);
    Index++;
    points_sphere[Index] = vertices_sphere[b];
    bnormals[Index] = vec3(vertices_sphere[b].x, vertices_sphere[b].y, vertices_sphere[b].z);
    stex_coords[Index] = vec2(t1/(2*M_PI), -(p1-M_PI/2.0)/M_PI);
    Index++;
    points_sphere[Index] = vertices_sphere[c];
    bnormals[Index] = vec3(vertices_sphere[c].x, vertices_sphere[c].y, vertices_sphere[c].z);
    stex_coords[Index] = vec2(t1/(2*M_PI), -(p0-M_PI/2.0)/M_PI);
    Index++;
    points_sphere[Index] = vertices_sphere[a];
    bnormals[Index] = vec3(vertices_sphere[a].x, vertices_sphere[a].y, vertices_sphere[a].z);
    stex_coords[Index] = vec2(t0/(2*M_PI), -(p1-M_PI/2.0)/M_PI);
    Index++;
    points_sphere[Index] = vertices_sphere[c];
    bnormals[Index] = vec3(vertices_sphere[c].x, vertices_sphere[c].y, vertices_sphere[c].z);
    stex_coords[Index] = vec2(t1/(2*M_PI), -(p1-M_PI/2.0)/M_PI);//t,p?
    Index++;
    points_sphere[Index] = vertices_sphere[d];
    bnormals[Index] = vec3(vertices_sphere[d].x, vertices_sphere[d].y, vertices_sphere[d].z);
    stex_coords[Index] = vec2(t0/(2*M_PI), -(p0-M_PI/2.0)/M_PI);//t,p?
    Index++;
}

void
colorbube(void)
{
    //textures on spheres messing up beacuse of the size of ssegments, and Numvertices_Sphere
    float r = 0.5;
    Index = 0;
    float ph_top = (float)((ssegments/2)-1)/(float)(ssegments/2) * M_PI/2.0;
    float ph_bottom = -ph_top;
    
    for ( int n = 0; n < ssegments; n++ )
    {
        GLfloat const t0 = 2 * M_PI * (float)n / (float)ssegments;
        GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)ssegments;
        
        points_sphere[Index].x = 0.0;
        points_sphere[Index].y = top;
        points_sphere[Index].z = 0.0;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(0.0,0.0);
        Index++;
        points_sphere[Index].x = cos(ph_top) * cos(t0) * r;
        points_sphere[Index].y = sin(ph_top) * r;
        points_sphere[Index].z = cos(ph_top) * sin(t0) * r;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(t0/(2*M_PI), -(ph_top-M_PI/2.0)/M_PI);
        Index++;
        // * the top variable
        points_sphere[Index].x = cos(ph_top) * cos(t1) * r;
        points_sphere[Index].y = sin(ph_top) * r;
        points_sphere[Index].z = cos(ph_top) * sin(t1) * r;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(t1/(2*M_PI), -(ph_top-M_PI/2.0)/M_PI);
        Index++;
    }
    
    for( int m = 1; m < ssegments-1; m++ )
    {
        float p0 = M_PI/2.0 - m * M_PI/(float)ssegments;
        float p1 = M_PI/2.0 - (m+1) * M_PI/(float)ssegments;
        
        int num;
        for ( int n = 0; n < ssegments; n++ )
        {
            num = 0;
            float x = 0.0, y = 0.0, r = 0.5;
            
            GLfloat const t0 = 2 * M_PI * (float)n / (float)ssegments;
            GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)ssegments;
            
            //quad vertex 0
            vertices_sphere[num].x = cos(p1)*cos(t0) * r;
            vertices_sphere[num].y = sin(p1) * r;
            vertices_sphere[num].z = cos(p1)*sin(t0) * r;
            vertices_sphere[num++].w = 1.0;
            //quad vertex 1
            vertices_sphere[num].x = cos(p1)*cos(t1) * r;
            vertices_sphere[num].y = sin(p1) * r;
            vertices_sphere[num].z = cos(p1)*sin(t1) * r;
            vertices_sphere[num++].w = 1.0;
            //quad vertex 2
            vertices_sphere[num].x = cos(p0)*cos(t1) * r;
            vertices_sphere[num].y = sin(p0) * r;
            vertices_sphere[num].z = cos(p0)*sin(t1) * r;
            vertices_sphere[num++].w = 1.0;
            //quad vertex 3
            vertices_sphere[num].x = cos(p0)*cos(t0) * r;
            vertices_sphere[num].y = sin(p0) * r;
            vertices_sphere[num].z = cos(p0)*sin(t0) * r;
            vertices_sphere[num++].w = 1.0;
            
            quad_sphere( 0, 1, 2, 3, t0, t1, p0, p1);
        }
    }
    
    for ( int n = 0; n < ssegments; n++ )
    {
        GLfloat const t0 = 2 * M_PI * (float)n / (float)ssegments;
        GLfloat const t1 = 2 * M_PI * (float)(n+1) / (float)ssegments;
        
        points_sphere[Index].x = 0.0;
        points_sphere[Index].y = bottom;
        points_sphere[Index].z = 0.0;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(0.0,1.0);
        Index++;
        points_sphere[Index].x = cos(ph_bottom) * cos(t0) * r;
        points_sphere[Index].y = sin(ph_bottom) * r;
        points_sphere[Index].z = cos(ph_bottom) * sin(t0) * r;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(t0/(2*M_PI), -(ph_bottom-M_PI/2.0)/M_PI);
        Index++;
        points_sphere[Index].x = cos(ph_bottom) * cos(t1) * r;
        points_sphere[Index].y = sin(ph_bottom) * r;
        points_sphere[Index].z = cos(ph_bottom) * sin(t1) * r;
        points_sphere[Index].w = 1.0;
        bnormals[Index] = vec3(points_sphere[Index].x, points_sphere[Index].y, points_sphere[Index].z);
        stex_coords[Index] = vec2(t1/(2*M_PI), -(ph_bottom-M_PI/2.0)/M_PI);
        Index++;
    }
    
}







//---- OpenGL initialization

GLuint program;
GLuint vPosition;
GLuint vColor;
GLuint vNormal;
GLuint vTexCoord;

GLuint textures[20]; //number of textures, 20 is too much for now


size_t CUBE_OFFSET;
size_t CUBE_N_OFFSET;

size_t CYLINDER_OFFSET;
size_t CYLINDER_N_OFFSET;

size_t SPHERE_OFFSET;
size_t SPHERE_N_OFFSET;

size_t CUBE_TEX_OFFSET;
size_t SPHERE_TEX_OFFSET;
float tr_y = Theta1[Yaxis]* M_PI/180.0;
float tr_z = Theta1[Zaxis]* M_PI/180.0;
float eye_x =0;
float eye_z = 0;
float eye_y = 0;


void
init()
{
    // Load shaders and use the resulting shader program
    program = InitShader( "vshader_lt.glsl", "fshader_lt.glsl" );
    glUseProgram( program );
    
    //---------------------------------------------------------------------
    colorcube();
    colortube();
    colorbube();
    
    //----- generate a checkerboard pattern for texture mapping
    const int  TextureSize  = 512;
    GLubyte checker_tex[TextureSize][TextureSize][3];
    
    for ( int i = 0; i < TextureSize; i++ )
    {
        for ( int j = 0; j < TextureSize; j++ )
        {
            //GLubyte c = (((i & 0x4) == 0) ^ ((j & 0x1)  == 0)) * 255;
            GLubyte c = (((j & 0x10)  == 0)) * 255;
            checker_tex[i][j][0]  = c;
            checker_tex[i][j][1]  = c;
            checker_tex[i][j][2]  = c;
        }
    }
    
    //---- Initialize texture objects
    glGenTextures( 20, textures );
    
    glActiveTexture( GL_TEXTURE0 );
    
    glBindTexture( GL_TEXTURE_2D, textures[0] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, TextureSize, TextureSize, 0, GL_BGR, GL_UNSIGNED_BYTE, checker_tex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    
    
    //make a functioon that takes in teture name and does sets the texure
    //for llop with the index being wchich tex obj
    unsigned char* pic1 = NULL;
    int w,h;
    loadBMP_custom(&pic1, &w, &h, "ceelogreene.bmp");
    
    glBindTexture( GL_TEXTURE_2D, textures[1] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, pic1 );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    
    
    //---------------------------------------------------------------------
    //
    unsigned char* mercuryTex= NULL;
    loadBMP_custom( &mercuryTex, &w, &h, "mercury_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[2] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, mercuryTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    
    unsigned char* venusTex= NULL;
    loadBMP_custom( &venusTex, &w, &h, "venus_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[3] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, venusTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    
    unsigned char* earthTex= NULL;
    loadBMP_custom( &earthTex, &w, &h, "earth_tex.bmp");

    glBindTexture( GL_TEXTURE_2D, textures[4] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, earthTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    
    unsigned char* marsTex= NULL;
    loadBMP_custom( &marsTex, &w, &h, "mars_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[5] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, marsTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    
    unsigned char* jupiterTex= NULL;
    loadBMP_custom( &jupiterTex, &w, &h, "jupiter_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, jupiterTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    
    unsigned char* saturnTex= NULL;
    loadBMP_custom( &saturnTex, &w, &h, "saturn_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, saturnTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //
    unsigned char* uranusTex= NULL;
    loadBMP_custom( &uranusTex, &w, &h, "uranus_tex2.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[8] );// doesnt like texture
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, uranusTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //
    unsigned char* neptuneTex= NULL;
    loadBMP_custom( &neptuneTex, &w, &h, "neptune_tex.bmp");
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, neptuneTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //
    unsigned char* plutoTex= NULL;
    loadBMP_custom( &plutoTex, &w, &h, "pluto_tex0.bmp");
    //glActiveTexture( GL_TEXTURE0 );
    glBindTexture( GL_TEXTURE_2D, textures[10] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, plutoTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //
    unsigned char* sunTex= NULL;
    loadBMP_custom( &sunTex, &w, &h, "sun1_tex.bmp");
    //glActiveTexture( GL_TEXTURE0 );
    glBindTexture( GL_TEXTURE_2D, textures[11] ); // doesnt like texture
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, sunTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //
    unsigned char* starTex= NULL;
    loadBMP_custom( &starTex, &w, &h, "starrysky_tex2.bmp");
    //glActiveTexture( GL_TEXTURE0 );
    glBindTexture( GL_TEXTURE_2D, textures[12] );// doesnt like texture
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, sunTex );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //

    
    /*
    unsigned char* pic2 = NULL;
    loadBMP_custom(&pic2, &w, &h, "granite.bmp");
    
    glBindTexture( GL_TEXTURE_2D, textures[2] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, pic2 );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    
    unsigned char* pic3 = NULL;
    loadBMP_custom(&pic3, &w, &h, "girls.bmp");
    
    glBindTexture( GL_TEXTURE_2D, textures[3] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, pic3 );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    
    unsigned char* pic4 = NULL;
    loadBMP_custom(&pic4, &w, &h, "cilo.bmp");
    
    glBindTexture( GL_TEXTURE_2D, textures[4] );
    glTexImage2D( GL_TEXTURE_2D, 0, GL_RGB, w, h, 0, GL_BGR, GL_UNSIGNED_BYTE, pic4 );
    glGenerateMipmap(GL_TEXTURE_2D);
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR );
    glTexParameterf( GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR );
    */
    
    //---------------------------------------------------------------------
    
    //----set offset variables
    
    CUBE_OFFSET = 0;
    CUBE_N_OFFSET = sizeof(points_cube) + sizeof(colors) + sizeof(points_cylinder) + sizeof(points_sphere);
    
    CYLINDER_OFFSET = sizeof(points_cube) + sizeof(colors);
    CYLINDER_N_OFFSET = CUBE_N_OFFSET + sizeof(normals);
    
    SPHERE_OFFSET = CYLINDER_OFFSET + sizeof(points_cylinder);
    SPHERE_N_OFFSET = CYLINDER_N_OFFSET + sizeof(tnormals);
    
    CUBE_TEX_OFFSET = SPHERE_N_OFFSET + sizeof(bnormals);
    SPHERE_TEX_OFFSET = CUBE_TEX_OFFSET + sizeof(tex_coords);
    
    //---- Create a vertex array object
    
    GLuint vao;
    //glGenVertexArraysAPPLE( 1, &vao );
    //glBindVertexArrayAPPLE( vao );
    glGenVertexArrays( 1, &vao );  // removed 'APPLE' suffix for 3.2
    glBindVertexArray( vao );
    
    // Create and initialize a buffer object
    GLuint buffer;
    glGenBuffers( 1, &buffer );
    glBindBuffer( GL_ARRAY_BUFFER, buffer );
    glBufferData( GL_ARRAY_BUFFER, sizeof(points_cube) + sizeof(colors) +
                 sizeof(points_cylinder) + sizeof(points_sphere)+
                 sizeof(normals) + sizeof(tnormals) + sizeof(bnormals) + sizeof(tex_coords) + sizeof(stex_coords),
                 NULL, GL_STATIC_DRAW );
    glBufferSubData( GL_ARRAY_BUFFER, 0, sizeof(points_cube), points_cube );
    glBufferSubData( GL_ARRAY_BUFFER, sizeof(points_cube), sizeof(colors), colors );
    glBufferSubData( GL_ARRAY_BUFFER, CYLINDER_OFFSET, sizeof(points_cylinder), points_cylinder );
    glBufferSubData( GL_ARRAY_BUFFER, SPHERE_OFFSET, sizeof(points_sphere), points_sphere );
    glBufferSubData( GL_ARRAY_BUFFER, CUBE_N_OFFSET, sizeof(normals), normals );
    glBufferSubData( GL_ARRAY_BUFFER, CYLINDER_N_OFFSET, sizeof(tnormals), tnormals );
    glBufferSubData( GL_ARRAY_BUFFER, SPHERE_N_OFFSET, sizeof(bnormals), bnormals );
    glBufferSubData( GL_ARRAY_BUFFER, CUBE_TEX_OFFSET, sizeof(tex_coords), tex_coords );
    glBufferSubData( GL_ARRAY_BUFFER, SPHERE_TEX_OFFSET, sizeof(stex_coords), stex_coords );
    
    //---------------------------------------------------------------------
    
    // set up vertex arrays
    vPosition = glGetAttribLocation( program, "vPosition" );
    glEnableVertexAttribArray( vPosition );
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(0) );
    
    vColor = glGetAttribLocation( program, "vColor" );
    glEnableVertexAttribArray( vColor );
    glVertexAttribPointer( vColor, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(sizeof(points_cube)) );
    
    vNormal = glGetAttribLocation( program, "vNormal" );
    glEnableVertexAttribArray( vNormal );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(sizeof(points_cube) + sizeof(colors) + sizeof(points_cylinder) + sizeof(points_sphere)) );
    
    vTexCoord = glGetAttribLocation( program, "vTexCoord" );
    glEnableVertexAttribArray( vTexCoord );
    glVertexAttribPointer( vTexCoord, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CUBE_TEX_OFFSET) );
    
    // Set the value of the fragment shader texture sampler variable (myTextureSampler) to GL_TEXTURE0
    glUniform1i( glGetUniformLocation(program, "myTextureSampler"), 0 );
    
    //---------------------------------------------------------------------
    
    //---- setup initial view
   /* float tr_y = Theta1[Yaxis]* M_PI/180.0;
    float tr_z = Theta1[Zaxis]* M_PI/180.0;
    float eye_x = r * (cos(tr_z)) * cos(tr_y);
    float eye_z = r * (cos(tr_z)) * sin(tr_y);
    float eye_y = r * sin(tr_z);
    */
    
     tr_y = Theta1[Yaxis]* M_PI/180.0;
     tr_z = Theta1[Zaxis]* M_PI/180.0;
     eye_x =0;
     eye_z = 0;
     eye_y = 100;

    
    view_matrix = LookAt( vec4(eye_x+5, eye_y, eye_z, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));//default camera posm
    default_view_matrix = view_matrix;
    
    glEnable( GL_DEPTH_TEST );
    glClearColor( 0.1, 0.1, 0.1, 1.0 );   //bkg?
}

//----------------------------------------------------------------------------

void
SetMaterial( vec4 ka, vec4 kd, vec4 ks, float s )
{
    glUniform4fv( glGetUniformLocation(program, "ka"), 1, ka );
    glUniform4fv( glGetUniformLocation(program, "kd"), 1, kd );
    glUniform4fv( glGetUniformLocation(program, "ks"), 1, ks );
    glUniform1f( glGetUniformLocation(program, "Shininess"), s );
}

//----------------------------------------------------------------------------

void
SetLight( vec4 lpos, vec4 la, vec4 ld, vec4 ls )
{
    glUniform4fv( glGetUniformLocation(program, "LightPosition"), 1, lpos);
    glUniform4fv( glGetUniformLocation(program, "AmbientLight"), 1, la);
    glUniform4fv( glGetUniformLocation(program, "DiffuseLight"), 1, ld);
    glUniform4fv( glGetUniformLocation(program, "SpecularLight"), 1, ls);
}

//used to reduce the display function
//----------------------------------------------------------------------------
void ReduceCube(mat4 transform) {
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform );
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(0) ); //for cube  or
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CUBE_N_OFFSET) );
    glVertexAttribPointer( vTexCoord, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CUBE_TEX_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesCube );
    
}

void ReduceCylinder(mat4 transform) {
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform );
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_N_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesCylinder );
    
}
//modify to take vertices and it can reduce everything
void ReduceSphere(mat4 transform) {
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform );
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_N_OFFSET) );
    glVertexAttribPointer( vTexCoord, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_TEX_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesSphere );
}

bool isScale = false;
int actualScale(float scale, float distance) {//pass in the bool from key switch,
    //return sunSize
    int sunSize = 0;
    if(isScale == false) {
        sunSize = 8; //change later
        scale *= 10;
        distance = log(distance);
    }
    else {
        sunSize = 41.32944;
    }
    return sunSize;
}

//----------------------------------------------------------------------------

//----------------------------------------------------------------------------

//used for orbits days, how quick they rotate
float cameraOrbit = 0;
float rotx = 0.0;
float mercuryOrbit = 0.0;
float venusOrbit = 0.0;
float earthOrbit = 0.0;
float marsOrbit = 0.0;
float jupiterOrbit = 0.0;
float saturnOrbit = 0.0;
float uranusOrbit = 0.0;
float neptuneOrbit = 0.0;
float plutoOrbit = 0.0;
//moon orbits
float EMoon = 1;
float MMoon1 = 1;
float MMoon2 = 1;
float JMoon1 = 1;
float JMoon2 = 1;
float JMoon3 = 1;
float JMoon4 = 1;
float JMoon5 = 1;
float JMoon6 = 1;
float JMoon7 = 1;
float JMoon8 = 1;
float JMoon9 = 1;
float JMoon10 = 1;
float SMoon1 = 1;
float SMoon2 = 1;
float SMoon3 = 1;
float SMoon4 = 1;
float SMoon5 = 1;
float SMoon6 = 1;
float SMoon7 = 1;
float SMoon8 = 1;
float SMoon9 = 1;
float UMoon1 = 1;
float UMoon2 = 1;
float UMoon3 = 1;
float UMoon4 = 1;
float UMoon5 = 1;
float UMoon6 = 1;
float UMoon7 = 1;
float NMoon1 = 1;
float NMoon2 = 1;
float NMoon3 = 1;
float NMoon4 = 1;
float NMoon5 = 1;
float PMoon1 = 1;
float PMoon2 = 1;


//moon scales
float EMoonS = 1;
float MMoonS1 = 1;
float MMoonS2 = 1;
float JMoonS1 = 1;
float JMoonS2 = 1;
float JMoonS3 = 1;
float JMoonS4 = 1;
float JMoonS5 = 1;
float JMoonS6 = 1;
float JMoonS7 = 1;
float JMoonS8 = 1;
float JMoonS9 = 1;
float JMoonS10 = 1;
float SMoonS1 = 1;
float SMoonS2 = 1;
float SMoonS3 = 1;
float SMoonS4 = 1;
float SMoonS5 = 1;
float SMoonS6 = 1;
float SMoonS7 = 1;
float SMoonS8 = 1;
float SMoonS9 = 1;
float UMoonS1 = 1;
float UMoonS2 = 1;
float UMoonS3 = 1;
float UMoonS4 = 1;
float UMoonS5 = 1;
float UMoonS6 = 1;
float UMoonS7 = 1;
float NMoonS1 = 1;
float NMoonS2 = 1;
float NMoonS3 = 1;
float NMoonS4 = 1;
float NMoonS5 = 1;
float PMoonS1 = 1;
float PMoonS2 = 1;


float myX = 11.9;
float myZ = 0;
float vX = 13;
float vZ = (13);
float eX = 2;
float eZ = (17.5);
float maX = -19.5;
float maZ =(17.5);
float jX = -56;
float jZ = -(5);
float sX = -56;
float sZ = -(100);
float uX = -10;
float uZ = -(170);
float nX = 160;
float nZ = -(150);
float pX = 350;
float pZ = -(100);


void
display( void )
{
    glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );
    
    //---- camera
    
    float left = -1.0;
    float right = 1.0;
    float bottom = -1.0;
    float top = 1.0;
    float zNear = 1.0;
    float zFar = 10000.0;
    
    proj_matrix = Frustum( left, right, bottom, top, zNear, zFar );
    GLuint proj = glGetUniformLocation( program, "projection" );
    glUniformMatrix4fv( proj, 1, GL_TRUE, proj_matrix );
    
    if (spherical_cam_on)
    {
        float tr_y = Theta1[Yaxis]* M_PI/180.0;
        float tr_z = Theta1[Zaxis]* M_PI/180.0;
        //float eye_x = r * (cos(tr_z)) * cos(tr_y);
        //float eye_z = r * (cos(tr_z)) * sin(tr_y);
        //float eye_y = r * sin(tr_z);
        float eye_x = 400;
        float eye_z = 400;
        float eye_y = r * sin(tr_z);
        
        vec4 up = vec4(0.0, cos(tr_z), 0.0, 0.0);
        //cout << up << ' ' << normalize(up) << endl;
        
        view_matrix = LookAt( vec4(eye_x, eye_y, eye_z, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
    }
    
    //cout << view_matrix << endl;
    
    GLuint view = glGetUniformLocation( program, "view" );
    glUniformMatrix4fv( view, 1, GL_TRUE, view_matrix );
    mat4 transform_tube;
    /*
    //---- action
    
    //---- lamp post 1
    
    //---- metal stand
    SetMaterial( vec4(0.1, 0.1, 0.1, 1.0), vec4(0.1, 0.1, 0.1, 1.0), vec4(0.3, 0.3, 0.3, 1.0), 0.5);
    
    mat4 transform_tube = Translate( 1.0, 0.5, 2.0 ) * Scale(0.02, 1.0, 0.02);
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_tube );
    
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_N_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesCylinder );
    
    transform_tube = Translate( 1.0, 0.05, 2.0 ) * Scale(0.5, 0.1, 0.5);
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_tube );
    
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_N_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesCylinder );
    */
    
    //---- spherical light
    SetLight( vec4( 1.0, 1.17, 2.0, 1.0 ), vec4(0.4, 0.4, 0.4, 1.0), vec4(0.7, 0.7, 0.7, 1.0), vec4(0.7, 0.7, 0.7, 1.0) );
    
    SetMaterial( vec4(0.9, 0.9, 0.7, 1.0), vec4(0.9, 0.9, 0.7, 1.0), vec4(0.9, 0.9, 0.7, 1.0), 0.5);
    glUniform1i( glGetUniformLocation(program, "light_out"), true );
    
    mat4 transform_bube = Translate( 0.0, 0.0, 0.0 ) * Scale(0.3, 0.3, 0.3);
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_bube );
    
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_N_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesSphere );
    float sunS = 8;
    
    float myS = 0.1464;
    //float myD = 45.22944;
    float vS = .36312;
    //float vD = 48.55944;
    float eS = 0.38262;
    //float eD = 51.32944;
    float maS = 0.20364;
    //float maD =56.56944;
    float jS = 4.2822;
    //float jD = 91.53244;
    float sS = 3.60102;
    //float sD = 136.71944;
    float uS = 1.53456;
    //float uD = 231.50944;
    float nS = 1.4886;
    //float nD = 341.92944;
    float pS = .006888;
    //float pD = 436.62944;
    
    //vec 3 planetPos, position of planets saved so a moon can orbit
    //or save pX,pY,pZ
    /*
    float myX = 11.9;
    float myZ = 0;
    float vX = 13;
    float vZ = (13);
    float eX = 2;
    float eZ = (17.5);
    float maX = -19.5;
    float maZ =(17.5);
    float jX = -56;
    float jZ = -(5);
    float sX = -56;
    float sZ = -(100);
    float uX = -10;
    float uZ = -(170);
    float nX = 160;
    float nZ = -(150);
    float pX = 350;
    float pZ = -(100);
     */

    if(isScale == false) {
        sunS = 8;
        myS = 0.1464;
        //myD = 45.22944 - 33.32988;
        vS = .36312;
        //vD = 48.55944- 33.32988;
        eS = 0.38262;
        //eD = 51.32944- 33.32988;
        maS = 0.20364;
        //maD =56.56944- 33.32988;
        jS = 4.2822;
        //jD = 91.53244- 33.32988;
        sS = 3.60102;
        //sD = 136.71944- 33.32988;
        uS = 1.53456;
        //uD = 231.50944- 33.32988;
        nS = 1.4886;
        //nD = 341.92944- 33.32988;
        pS = .006888;
        //pD = 436.62944 - 33.32988;
        
    }
 

    // ------> ceelotest
    /*
    //seems to map half of the texture, has something to do with vTexCoord and SPHERE_TEX_OFFSET
    sunS =  actualScale( myS,  myX);
    glBindTexture( GL_TEXTURE_2D, textures[2] );//5
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    //glUniform1i( glGetUniformLocation(program, "st_factor"), 1 );
    //SetLight( vec4( 0.0, 0.0, 0.0, 1.0 ), vec4(0.4, 0.4, 0.4, 1.0), vec4(0.7, 0.7, 0.7, 1.0), vec4(0.7, 0.7, 0.7, 1.0) );//1.0, 1.17, 2.0, 1.0
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = Translate( 0, 5, 0.0 ) * RotateY(180) * Scale(3.5, 3.5, 3.5);//translate x and ------------axis with sin and cos, in idle make it rotate-------------------
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_bube );
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_N_OFFSET) );
    glVertexAttribPointer( vTexCoord, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_TEX_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesSphere );
    //ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    //---- sphere
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "light_out"), false );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    //glUniform1f( glGetUniformLocation(program, "st_factor"), 1.0 );
    SetMaterial( vec4(1.0,1.0,1.0,1.0), vec4(1.0,1.0,1.0,1.0), vec4(1.0,1.0,1.0,1.0), 2.0);
    
     transform_bube = Translate( 0.0, 1.0, -1.0 ) * RotateY(180) * Scale(2.0, 2.0, 2.0);
    glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_bube );
    
    glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_OFFSET) );
    glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_N_OFFSET) );
    glVertexAttribPointer( vTexCoord, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(SPHERE_TEX_OFFSET) );
    glDrawArrays( GL_TRIANGLES, 0, NumVerticesSphere );
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
     */
    
    //----------->Starry Sky
    //glUniform1i( glGetUniformLocation(program, "light_out"), true );
    //SetLight( vec4( 0.0, 0.0, 0.0, 1.0 ), vec4(0.4, 0.4, 0.4, 1.0), vec4(0.7, 0.7, 0.7, 1.0), vec4(0.7, 0.7, 0.7, 1.0) );//1.0, 1.17, 2.0, 1.0
    //SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    glBindTexture( GL_TEXTURE_2D, textures[12] );//5
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    transform_bube = RotateY( rotx) * Translate( 0.0, 0.0, 0.0 ) * RotateY(360) * Scale(1500, 1500, 1500);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //glUniform1i( glGetUniformLocation(program, "light_out"), false );

    
    
    
    //--------->Sun
    
    //inital sunvalue probably not set   //mess with to get sun working
    SetLight( vec4( 0.0, 0.0, 0.0, 1.0 ), vec4(0.4, 0.4, 0.4, 1.0), vec4(0.7, 0.7, 0.7, 1.0), vec4(0.7, 0.7, 0.7, 1.0) );//1.0, 1.17, 2.0, 1.0
    SetMaterial( vec4(0.9, 0.9, 0.7, 1.0), vec4(0.9, 0.9, 0.7, 1.0), vec4(0.9, 0.9, 0.7, 1.0), 0.5);
    glUniform1i( glGetUniformLocation(program, "light_out"), true );
    transform_bube = Translate( 0.0, 0.0, 0.0 ) * Scale(sunS, sunS, sunS);
    ReduceSphere(transform_bube);
    //glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    glUniform1i( glGetUniformLocation(program, "light_out"), false );
    
    
    // ------> Mecury
    sunS =  actualScale( myS,  myX);
    glBindTexture( GL_TEXTURE_2D, textures[2] );//5
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(mercuryOrbit) * Translate( myX, 0.0, myZ ) * RotateY(mercuryOrbit) * Scale(myS+1, myS+1, myS+1);//translate x and
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    // ------> Venus
    sunS =  actualScale( vS,  vX);
    glBindTexture( GL_TEXTURE_2D, textures[3] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(venusOrbit) * Translate( vX, 0.0, vZ) * RotateY(180) * Scale(vS+1, vS+1, vS+1);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    // ------> Earth
    sunS =  actualScale( eS,  eX);
    glBindTexture( GL_TEXTURE_2D, textures[4] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube =RotateY(earthOrbit) * Translate( eX, 0.0, eZ) * RotateY(earthOrbit) * Scale(eS+1,eS+1,eS+1);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon
    glBindTexture( GL_TEXTURE_2D, textures[4] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);

    transform_bube = RotateY(EMoon) * Translate( 1.5, 0.0, 0.0)*RotateY(EMoon) * Scale(.3826, .3826, .3826) ;
    transform_bube =  RotateY(earthOrbit) * Translate( eX, 0.0, eZ) * transform_bube; // * Scale(eS,eS,eS);
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
        // ------> Mars
    sunS =  actualScale( maS,  maX);
    glBindTexture( GL_TEXTURE_2D, textures[5] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(marsOrbit) * Translate( maX, 0.0, maZ) * RotateY(marsOrbit) * Scale(maS+1,maS+1,maS+1);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[5] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(MMoon1) * Translate( 1.5, 0.0, 0.0)*RotateY(MMoon1) * Scale(.20364, .20364, .20364) ;
    transform_bube =  RotateY(marsOrbit) * Translate( maX, 0.0, maZ) * transform_bube; // * Scale(eS,eS,eS);

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[5] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(MMoon2) * Translate( 2.5, 0.0, 0.0)*RotateY(MMoon2) * Scale(.20364+.3, .20364+.3, .20364+.3) ;
    transform_bube =  RotateY(marsOrbit) * Translate( maX, 0.0, maZ) * transform_bube; // * Scale(eS,eS,eS);    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    
    
    // ------> Jupiter
    sunS =  actualScale( jS,  jX);
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube =RotateY(jupiterOrbit)* Translate( jX, 0.0, jZ) * RotateY(jupiterOrbit) * Scale(jS,jS,jS);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    
    transform_bube = RotateY(JMoon1) * Translate( 1.7, 0.0, 0.0)*RotateY(JMoon1) * Scale(1, 1, 1) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube; // * Scale(eS,eS,eS);

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon2) * Translate( 2.3, 0.0, 0.0)*RotateY(JMoon2) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon3
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon3) * Translate( 2.9, 0.0, 0.0)*RotateY(JMoon3) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon4
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon4) * Translate( 3.8, 0.0, 0.0)*RotateY(JMoon4) * Scale(1-.15, 1-.15, 1-.15) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon5
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon5) * Translate( 4.8, 0.0, 0.0)*RotateY(JMoon5) * Scale(1, 1, 1) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon6
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon6) * Translate( 5.8, 0.0, 0.0)*RotateY(JMoon6) * Scale(1-.005, 1-.005, 1-.005) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon7
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon7) * Translate( 6.8, 0.0, 0.0)*RotateY(JMoon7) * Scale(1-.3, 1-.3, 1-.3) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon8
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon8) * Translate( 7.8, 0.0, 0.0)*RotateY(JMoon8) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon9
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon9) * Translate( 8.8, 0.0, 0.0)*RotateY(JMoon9) * Scale(1-.5, 1-.5, 1-.5) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon10
    glBindTexture( GL_TEXTURE_2D, textures[6] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(JMoon10) * Translate( 11, 0.0, 0.0)*RotateY(JMoon10) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(jupiterOrbit) * Translate( jX, 0.0, jZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    
    // ------> Saturn
    sunS =  actualScale( sS,  sX);
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * RotateY(saturnOrbit) * Scale(sS,sS,sS);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //cylinder ring here
    
    //---- Saturn Ring
    SetMaterial( vec4(0.1, 0.1, 0.1, 1.0), vec4(0.1, 0.1, 0.1, 1.0), vec4(0.3, 0.3, 0.3, 1.0), 0.5);
    
    // transform_tube = Translate( 1.0, 0.5, 2.0 ) * Scale(0.02, 1.0, 0.02);
    //glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_tube );
    
    //glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_OFFSET) );
    //glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_N_OFFSET) );
    //glDrawArrays( GL_TRIANGLES, 0, NumVerticesCylinder );
    
    transform_tube = RotateY(saturnOrbit) * Translate( 0, 0.00, 0.0 ) * Scale(8.5, 0.01, 8.5);
    //glUniformMatrix4fv( glGetUniformLocation( program, "model" ), 1, GL_TRUE, transform_tube );
    
    //glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_OFFSET) );
    //glVertexAttribPointer( vNormal, 3, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(CYLINDER_N_OFFSET) );
    //glDrawArrays( GL_TRIANGLES, 0, NumVerticesCylinder );
    ReduceCylinder(transform_tube);
    
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon1) * Translate( 3.0, 0.0, 0.0)*RotateY(SMoon1) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon2) * Translate( 4.0, 0.0, 0.0)*RotateY(SMoon2) * Scale(1-.05, 1-.05, 1-.05) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    //moon3
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon3) * Translate( 4.0, 0.0, 0.0)*RotateY(SMoon3) * Scale(1-.3, 1-.3, 1-.3) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon4
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon4) * Translate( 5.0, 0.0, 0.0)*RotateY(SMoon4) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon5
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon5) * Translate( 6.0, 0.0, 0.0)*RotateY(SMoon5) * Scale(1-.4, 1-.4, 1-.4) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    //moon6
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon6) * Translate( 7.0, 0.0, 0.0)*RotateY(SMoon6) * Scale(1-.25, 1-.25, 1-.25) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon7
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon7) * Translate( 8.0, 0.0, 0.0)*RotateY(SMoon7) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon8
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon8) * Translate( 9.0, 0.0, 0.0)*RotateY(SMoon8) * Scale(1-.4, 1-.4, 1-.4) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    //moon9
    glBindTexture( GL_TEXTURE_2D, textures[7] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(SMoon9) * Translate( 10.0, 0.0, 0.0)*RotateY(SMoon9) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(saturnOrbit) * Translate( sX, 0.0, sZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );




    
    // ------> Uranus
    sunS =  actualScale( uS,  uX);
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube =RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * RotateY(uranusOrbit) * Scale(uS+.5,uS+.5,uS+.5);
    ReduceSphere(transform_bube);
    //glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon1) * Translate( 2.0, 0.0, 0.0)*RotateY(UMoon1) * Scale(1-.5, 1-.5, 1-.5) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
   
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon2) * Translate( 3.0, 0.0, 0.0)*RotateY(UMoon2) * Scale(1-.7, 1-.7, 1-.7) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon3
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon3) * Translate( 4.0, 0.0, 0.0)*RotateY(UMoon3) * Scale(1-.3, 1-.3, 1-.3) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon4
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon4) * Translate( 5.0, 0.0, 0.0)*RotateY(UMoon4) * Scale(1-.5, 1-.5, 1-.5) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon5
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon5) * Translate( 7.0, 0.0, 0.0)*RotateY(UMoon5) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon6
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon6) * Translate( 8.0, 0.0, 0.0)*RotateY(UMoon6) * Scale(1-.4, 1-.4, 1-.4) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon7
    glBindTexture( GL_TEXTURE_2D, textures[8] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(UMoon7) * Translate( 9.0, 0.0, 0.0)*RotateY(UMoon7) * Scale(1-.6, 1-.6, 1-.6) ;
    transform_bube =  RotateY(uranusOrbit) * Translate( uX, 0.0, uZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    
    // ------> Neptune
    sunS =  actualScale( nS,  nX);
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * RotateY(neptuneOrbit) * Scale(nS+1,nS+1,nS+1);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(NMoon1) * Translate( 2.0, 0.0, 0.0)*RotateY(NMoon1) * Scale(1-.4, 1-.4, 1-.4) ;
    transform_bube =  RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * transform_bube;

    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(NMoon2) * Translate( 3.0, 0.0, 0.0)*RotateY(NMoon2) * Scale(1-.25, 1-.25, 1-.25) ;
    transform_bube =  RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon3
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(NMoon3) * Translate( 4.0, 0.0, 0.0)*RotateY(NMoon3) * Scale(1-.4, 1-.4, 1-.4) ;
    transform_bube =  RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon4
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(NMoon4) * Translate( 5.0, 0.0, 0.0)*RotateY(NMoon4) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon5
    glBindTexture( GL_TEXTURE_2D, textures[9] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(NMoon5) * Translate( 6.0, 0.0, 0.0)*RotateY(NMoon5) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(neptuneOrbit) * Translate( nX, 0.0, nZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    // ------> Pluto
    sunS =  actualScale( pS,  pX);
    glBindTexture( GL_TEXTURE_2D, textures[10] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    transform_bube = RotateY(plutoOrbit) * Translate( pX, 0.0, pZ ) * RotateY(plutoOrbit) * Scale(pS+1,pS+1,pS+1);
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    
    //moon1
    glBindTexture( GL_TEXTURE_2D, textures[10] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(PMoon1) * Translate( 1.0, 0.0, 0.0)*RotateY(PMoon1) * Scale(1-.1, 1-.1, 1-.1) ;
    transform_bube =  RotateY(plutoOrbit) * Translate( pX, 0.0, pZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );
    //moon2
    glBindTexture( GL_TEXTURE_2D, textures[10] );
    glUniform1i( glGetUniformLocation(program, "texture_on"), true );
    SetMaterial( vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0), 2);
    
    transform_bube = RotateY(PMoon2) * Translate( 2, 0.0, 0.0)*RotateY(PMoon2) * Scale(1-.2, 1-.2, 1-.2) ;
    transform_bube =  RotateY(plutoOrbit) * Translate( pX, 0.0, pZ) * transform_bube;
    
    ReduceSphere(transform_bube);
    glUniform1i( glGetUniformLocation(program, "texture_on"), false );

    


#if 0
    //---- spheres
    
    for ( int i = 0; i < 100; i++ )
    {
        mat4 transform_bube = Translate( rand()%10-5, rand()%10-5, rand()%10-5 ) * Scale(0.3, 0.7, 0.3);
        
        model = glGetUniformLocation( program, "model" );
        glUniformMatrix4fv( model, 1, GL_TRUE, transform_bube );
        
        glUniform1i( glGetUniformLocation(program, "obj_color_on"), true );
        glUniform4fv( glGetUniformLocation(program, "obj_color"), 1, vec4(0.8, 0.0, 0.0, 1.0) );
        
        glVertexAttribPointer( vPosition, 4, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(sizeof(points_cube) + sizeof(colors) + sizeof(points_cylinder)) );
        glDrawArrays( GL_TRIANGLES, 0, NumVerticesSphere );
    }
    
#endif
    
    glutSwapBuffers();
}

//----------------------------------------------------------------------------

void
keyboard( unsigned char key, int x, int y ) //also pass in planets transformation
{
    switch( key ) {
        case 033:    //---- escape key
        case 'q': case 'Q':
            exit( EXIT_SUCCESS );
            break;
            
        case 'u':
            view_matrix = default_view_matrix;
            //Theta1[Xaxis] = 0.0;
            //Theta1[Yaxis] = 0.0;
            //Theta1[Zaxis] = 0.0;
            glutPostRedisplay();
            break;
            
            //---- cases for spherical viewer
        case 's':    //---- spherical cam on
            Theta1[Xaxis] = 0.0;
            Theta1[Yaxis] = 0.0;
            Theta1[Zaxis] = 0.0;
            spherical_cam_on = true;
            glutPostRedisplay();
            break;
        case 'S':    //---- spherical cam off
            spherical_cam_on = false;
            glutPostRedisplay();
            break;
            
        case 'y':    //---- theta
            Axis = Yaxis;
            Theta1[Axis] += 5.0;
            Theta1[Axis] = fmod(Theta1[Axis], 360.0);
            glutPostRedisplay();
            break;
        case 'z':    //---- phi
            Axis = Zaxis;
            Theta1[Axis] += 5.0;
            Theta1[Axis] = fmod(Theta1[Axis], 360.0);
            glutPostRedisplay();
            break;
        case 'r':    //---- increase radius
            r += 0.5;
            glutPostRedisplay();
            break;
        case 'R':    //---- decrease radius
            r -= 0.5;
            glutPostRedisplay();
            break;
            
            //---- cases for flying viewer
            //F faster for space travel
        case 'f':    //---- forward
            view_matrix = Translate(0.0, 0.0, 0.4) * view_matrix;
            glutPostRedisplay();
            break;
        case 'F':    //---- forward
            view_matrix = Translate(0.0, 0.0, 2.0) * view_matrix;
            glutPostRedisplay();
            break;
            
        case 'b':    //---- back
            view_matrix = Translate(0.0, 0.0, -0.4) * view_matrix;
            glutPostRedisplay();
            break;
        case 'B':    //---- back
            view_matrix = Translate(0.0, 0.0, -2.0) * view_matrix;
            glutPostRedisplay();
            break;
            
        case 't' :   //---- up
            view_matrix = Translate(0.0, -0.4, 0.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'T' :   //---- up
            view_matrix = Translate(0.0, -0.8, 0.0) * view_matrix;
            glutPostRedisplay();
            break;

        case 'h' :   //----down
            view_matrix = Translate(0.0, 0.4, 0.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'H' :   //----down
            view_matrix = Translate(0.0, 0.8, 0.0) * view_matrix;
            glutPostRedisplay();
            break;

        case '9' :   //---- left
            view_matrix = Translate(0.4, 0.0, 0.0) * view_matrix;
            glutPostRedisplay();
            break;
        case '0' :   //----right
            view_matrix = Translate(-0.4, 0.0, 0.0) * view_matrix;
            glutPostRedisplay();
            break;
            
            
        case 'j':    //---- pan left
            view_matrix = RotateY(-1.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'J':    //---- pan left
            view_matrix = RotateY(-3.0) * view_matrix;
            glutPostRedisplay();
            break;

        case 'k':    //---- pan right
            view_matrix = RotateY(1.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'K':    //---- pan right
            view_matrix = RotateY(3.0) * view_matrix;
            glutPostRedisplay();
            break;

        case 'o':    //---- pan up
            view_matrix = RotateX(-1.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'O':    //---- pan UP
            view_matrix = RotateX(-3.0) * view_matrix;
            glutPostRedisplay();
            break;

        case 'l':    //---- pan down
            view_matrix = RotateX(1.0) * view_matrix;
            glutPostRedisplay();
            break;
        case 'L':    //---- pan DOWN
            view_matrix = RotateX(3.0) * view_matrix;
            glutPostRedisplay();
            break;

        case 'a':    //---- pan right
            isScale = false;
            glutPostRedisplay();
            break;
        case 'A':    //---- pan right
            //bool is true for actual
            isScale = true;
            glutPostRedisplay();
            //as of now, no way to put it back to actual size
            break;
        /*case '=':    //---- panflip 180
            view_matrix = RotateZ(180.0) * view_matrix;
            glutPostRedisplay();
            break;
         */
          
            /*
        case 'E':    //---- camera follows earth
            // start pos of earth
            for(int i = 0; i <= 360; i++) {
                view_matrix = LookAt( vec4(eX, 30, eZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
                /*
                cameraOrbit = fmod(earthOrbit +3, 360);
                eye_x = cameraOrbit;// earth start position x
                eye_y = 0;
                eye_z = 17.5;
             
                
                glutPostRedisplay();
                
            }
    */
            
            glutPostRedisplay();
            //as of now, no way to put it back to actual size
            break;
        case '-':    //---- camera follows mercury
            view_matrix = LookAt( vec4(myX,30, myZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
                glutPostRedisplay();
            break;
        case '=':    //---- camera follows venus
            view_matrix = LookAt( vec4(vX, 30, vZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case '[':    //---- camera follows earth
            view_matrix = LookAt( vec4(eX, 30, eZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case ']':    //---- camera follows mars
            view_matrix = LookAt( vec4(maX, 30, maZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case ';':    //---- camera follows jupiter
            view_matrix = LookAt( vec4(jX, 30, jZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case ':':    //---- camera follows saturn
            view_matrix = LookAt( vec4(sX, 30, sZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case ',':    //---- camera follows Uranus
            view_matrix = LookAt( vec4(uX, 30, uZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case '.':    //---- camera follows Neptune
            view_matrix = LookAt( vec4(nX, 30, nZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;
        case '/':    //---- camera follows Pluto
            view_matrix = LookAt( vec4(pX, 30, pZ, 1.0), vec4(0.0, 0.0, 0.0, 1.0), vec4(0.0, cos(tr_z), 0.0, 0.0));
            glutPostRedisplay();
            break;









    }
}


//----------------------------------------------------------------------------

void
mouse( int button, int state, int x, int y )
{
    if ( state == GLUT_DOWN ) {
        switch( button ) {
            case GLUT_LEFT_BUTTON:    Axis = Xaxis;  break;
            case GLUT_MIDDLE_BUTTON:  Axis = Yaxis;  break;
            case GLUT_RIGHT_BUTTON:   Axis = Zaxis;  break;
        }
    }
}

//----------------------------------------------------------------------------

void
idle( void )
{
    
    //.5 is slowest for the most part
    // 5 is the fastest
    rotx = fmod(rotx + 1.0, 360.0);// rotates around itself
    //orbit paths around the sun
    mercuryOrbit = fmod(mercuryOrbit + 1.0, 360.0); // use speeds of plants to proportion
    venusOrbit = fmod(venusOrbit + 2.0, 360.0); //elipse formula for better orbit
    earthOrbit = fmod(earthOrbit + 2.5, 360.0);
    marsOrbit = fmod(marsOrbit + 3.0, 360.0);
    jupiterOrbit = fmod(jupiterOrbit + 3.3, 360.0);
    saturnOrbit = fmod(saturnOrbit + 3.5, 360.0);
    uranusOrbit = fmod(uranusOrbit + 3.8, 360.0);
    neptuneOrbit = fmod(neptuneOrbit + 4.0, 360.0);
    plutoOrbit = fmod(plutoOrbit + 4.2, 360.0);
    
    EMoon = fmod(EMoon + 13, 360);//-----------------------------------change all the moons to match the submlime notes
    // 30 - 5;     .3  - 730 or 260
    MMoon1 = fmod(MMoon1 + 30, 360);
    MMoon2 = fmod(MMoon2 + 13, 360);
    SMoon1 = fmod(SMoon1 + 25, 360);
    SMoon2 = fmod(SMoon2 + 27, 360);
    SMoon3 = fmod(SMoon3 + 29, 360);
    SMoon4 = fmod(SMoon4 + 15, 360);
    SMoon5 = fmod(SMoon5 + 8.5, 360);
    SMoon6 = fmod(SMoon6 + 28, 360);
    SMoon7 = fmod(SMoon7 + 24, 360);
    SMoon8 = fmod(SMoon8 + 21, 360);
    SMoon9 = fmod(SMoon9 + 15, 360);
    JMoon1 = fmod(JMoon1 + 15, 360);
    JMoon2 = fmod(JMoon2 + 20, 360);
    JMoon3 = fmod(JMoon3 + 21, 360);
    JMoon4 = fmod(JMoon4 + 25, 360);
    JMoon5 = fmod(JMoon5 + 30, 360);
    JMoon6 = fmod(JMoon6 + 5, 360);
    JMoon7 = fmod(JMoon7 + 28, 360);
    JMoon8 = fmod(JMoon8 + 4, 360);
    JMoon9 = fmod(JMoon9 + 6, 360);
    JMoon10 = fmod(JMoon9 + 2, 360);
    UMoon1 = fmod(UMoon1 + 25, 360);
    UMoon2 = fmod(UMoon2 + 27, 360);
    UMoon3 = fmod(UMoon3 + 15, 360);
    UMoon4 = fmod(UMoon4 + 20, 360);
    UMoon5 = fmod(UMoon5 + 23, 360);
    UMoon6 = fmod(UMoon6 + 30, 360);
    UMoon7 = fmod(UMoon7 + 10, 360);
    NMoon1 = fmod(NMoon1 + 18, 360);
    NMoon2 = fmod(NMoon2 + 22, 360);
    NMoon3 = fmod(NMoon3 + 4, 360);
    NMoon4 = fmod(NMoon4 + 28, 360);
    NMoon5 = fmod(NMoon5 + 30, 360);
    PMoon1 = fmod(PMoon1 + 25, 360);
    PMoon2 = fmod(PMoon2 + 10, 360);
    
    //either call a function that moves the camera or move the camera right here

    
    
    //moon orbits
    
    glutPostRedisplay();
}

//----------------------------------------------------------------------------

void
reshape( int w, int h )
{
    //glViewport(0,0,w,h);
}

//----------------------------------------------------------------------------

int
main( int argc, char **argv )
{
    glutInit( &argc, argv );
    //glutInitDisplayMode( GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH );
    glutInitDisplayMode(GLUT_3_2_CORE_PROFILE | GLUT_RGBA | GLUT_DOUBLE | GLUT_DEPTH );
    glutInitWindowSize( 800, 800 );
    glutCreateWindow( "Color Cube" );
    
    init();
    
    glutDisplayFunc( display );
    glutReshapeFunc( reshape );
    glutKeyboardFunc( keyboard );
    glutMouseFunc( mouse );
    glutIdleFunc( idle );
    
    glutMainLoop();
    return 0;
}