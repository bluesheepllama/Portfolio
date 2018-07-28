package com.example.mitchelldennen.datastructurevisualization;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.media.MediaPlayer;
import android.util.Log;
import android.view.View;
/**
 * Created by mitchelldennen on 3/7/18.
 */

public class AnimationActivityLL extends View {
    Activity myActivity;
    Paint redPaintBrushFill, bluePaintBrushFill, blackPaintBrushFill;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    Path linebefore, lineafter, line1215, line1213, line1315, square, arow,line;
    Bitmap bitmap1, bitmap5, bitmap9, bitmap15, bitmap19, bitmap13, bitmap31, bitmap12, bitmap24, bitmap26, bitmap23;
    int bitmap13x;
    int  x_dir13;
    int x12, y12, x13, y13;
    Boolean b31, b26, b24, b23, b19, b15, move,mp1b,mp2b;
    final MediaPlayer mp1, mp2;



    public AnimationActivityLL(Context context, Activity activity) {
        super(context);
        myActivity = activity;
        setBackgroundResource(R.drawable.white);

        bitmap13x = 900;


        x_dir13 = 1;

        b31 = true;
        b26 = true;
        b24 = true;
        b23 = true;
        b19 = true;
        b15 = true;
        move = true;
        mp1b=mp2b=true;

        x12 = 380;
        y12 = 1100;
        x13 = 400;
        y13 = 900;
        bitmap1 = BitmapFactory.decodeResource(getResources(), R.drawable.one);//
        bitmap15 = BitmapFactory.decodeResource(getResources(), R.drawable.fifteen);//
        bitmap5 = BitmapFactory.decodeResource(getResources(), R.drawable.five);//
        bitmap9 = BitmapFactory.decodeResource(getResources(), R.drawable.nine);//
        bitmap19 = BitmapFactory.decodeResource(getResources(), R.drawable.nineteen);//
        bitmap13 = BitmapFactory.decodeResource(getResources(), R.drawable.thirteen);//
        bitmap31 = BitmapFactory.decodeResource(getResources(), R.drawable.thirtyone);
        bitmap12 = BitmapFactory.decodeResource(getResources(), R.drawable.twelve);//
        bitmap24 = BitmapFactory.decodeResource(getResources(), R.drawable.twentyfour);//
        bitmap26 = BitmapFactory.decodeResource(getResources(), R.drawable.twentysix);//
        bitmap23 = BitmapFactory.decodeResource(getResources(), R.drawable.twentythree);//
        mp1 = MediaPlayer.create(context, R.raw.song12);
        mp2 = MediaPlayer.create(context, R.raw.chimes);

    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        redPaintBrushFill = new Paint();
        redPaintBrushFill.setColor(Color.RED);
        redPaintBrushFill.setStyle(Paint.Style.FILL);

        bluePaintBrushFill = new Paint();
        bluePaintBrushFill.setColor(Color.BLUE);
        bluePaintBrushFill.setStyle(Paint.Style.FILL);

        redPaintBrushStroke = new Paint();
        redPaintBrushStroke.setColor(Color.RED);
        redPaintBrushStroke.setStyle(Paint.Style.STROKE);
        redPaintBrushStroke.setStrokeWidth(10);

        bluePaintBrushStroke = new Paint();
        bluePaintBrushStroke.setColor(Color.BLUE);
        bluePaintBrushStroke.setStyle(Paint.Style.STROKE);
        bluePaintBrushStroke.setStrokeWidth(10);

        blackPaintBrushFill = new Paint();
        blackPaintBrushFill.setColor(Color.BLACK);
        blackPaintBrushFill.setStyle(Paint.Style.FILL);
        blackPaintBrushFill.setTextSize(60);


        canvas.drawText("Inserting an element into a linked list", 50, 1900, blackPaintBrushFill);

        mp1.start();
        //1
        if (bitmap13x >= 800 && bitmap13x <= 900 &&  b31) {
            x_dir13 = -1;
            canvas.drawText("13 smaller than 26", 100, 380, blackPaintBrushFill);
        } else {
            if (b31) {
                x_dir13 = 0;
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b31 = false;
            }
        }
        //2
        if(bitmap13x >= 700 && bitmap13x <= 800 && b26) {
            x_dir13 = -1;
            canvas.drawText("13 smaller than 24", 100, 440, blackPaintBrushFill);

        } else {
            if (b26 && !b31) {
                x_dir13 = 0;
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b26 = false;
            }
        }
        //3
        if(bitmap13x >= 600 && bitmap13x <= 700 &&  b24) {
            x_dir13 = -1;
            canvas.drawText("13 smaller than 23", 100, 500, blackPaintBrushFill);

        } else {
            if (b24 && !b26) {
                x_dir13 = 0;
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b24 = false;
            }
        }
        //4
        if(bitmap13x >= 500 && bitmap13x <= 600 && b23) {
            x_dir13 = -1;
            canvas.drawText("13 smaller than 19", 100, 560, blackPaintBrushFill);

        } else {
            if (b23 && !b24) {
                x_dir13 = 0;
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b23 = false;
            }
        }
        //5
        if(bitmap13x >= 400 && bitmap13x <= 500 && b19) {
            x_dir13 = -1;
            canvas.drawText("13 smaller than 15", 100, 620, blackPaintBrushFill);

        } else {
            if (b19 && !b23) {

                x_dir13 = 0;
                if(mp2b) {
                    mp2.start();
                    mp2b=false;
                }
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                    mp1.stop();
                b19 = false;
            }
        }
        linebefore = new Path();
        lineafter = new Path();
        line1213 = new Path();
        line1215 = new Path();
        line1315 = new Path();

        if(bitmap13x <= 401) {
            canvas.drawText("13 bigger than 12", 100, 740, blackPaintBrushFill);
            canvas.drawText("13 points to 15", 100, 800, blackPaintBrushFill);
            canvas.drawText("12 points to 13", 100, 860, blackPaintBrushFill);


            line1315.moveTo(400,900);
            line1315.moveTo(x13,y13);
            if(x13 <=480 || y13 <= 1100){

                x13 = x13 + 3;
                y13 = y13 +10;
                //Log.d("x13,y13: ", x13 + " " + y13);

            }
            canvas.drawLine(430,950,x13,y13, bluePaintBrushStroke);
            //Log.d("after 1315 ", " ");
            //this line should move second
            line1213.moveTo(380, 1100);
            line1213.lineTo(x12, y12);
            if((x12 <= 410 || y12 <= 910) && x13 >=460) {

                x12 = x12 + 2;
                y12 = y12 - 10;

                //Log.d("x12,y12: ", x12 + " " + y12);
            }
            canvas.drawPath(line1213, bluePaintBrushStroke);

        }else {

            canvas.drawLine(350,1140,450,1140, redPaintBrushStroke);

            //Log.d("after 1215 ", " ");

        }


        bitmap13x = bitmap13x + x_dir13;

        linebefore.moveTo(50,1140);
        linebefore.lineTo(350,1140);
        canvas.drawPath(linebefore, redPaintBrushStroke);
        lineafter.moveTo(450,1140);
        lineafter.lineTo(950,1140);
        canvas.drawPath(lineafter,redPaintBrushStroke);


        canvas.drawBitmap(bitmap1, 50, 1100, null);
        canvas.drawBitmap(bitmap5, 150, 1100, null);
        canvas.drawBitmap(bitmap9, 250, 1100, null);
        canvas.drawBitmap(bitmap12, 350, 1100, null);
        canvas.drawBitmap(bitmap13, bitmap13x, 900, null);
        canvas.drawBitmap(bitmap15, 450, 1100, null);
        canvas.drawBitmap(bitmap19, 550, 1100, null);
        canvas.drawBitmap(bitmap23, 650, 1100, null);
        canvas.drawBitmap(bitmap24, 750, 1100, null);
        canvas.drawBitmap(bitmap26, 850, 1100, null);
        canvas.drawBitmap(bitmap31, 950, 1100, null);



        invalidate();
    }
}