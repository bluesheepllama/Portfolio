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

public class AnimationActivityTree extends View {
    Paint redPaintBrushFill, bluePaintBrushFill,blackPaintBrushFill;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    Bitmap bitmap1, bitmap5, bitmap9, bitmap15, bitmap19, bitmap13, bitmap31, bitmap12, bitmap24, bitmap26, bitmap23;
    int bitmap13x, bitmap13y;
    int x_dir13, y_dir13;
    int x12, y12, x13, y13;
    Boolean b31, b26, b24, b23, b12, b15, move;
    final MediaPlayer mp1, mp2;
    Boolean mp1b,mp2b;


    public AnimationActivityTree(Context context,Activity activity) {
        super(context);
        setBackgroundResource(R.drawable.white);

        bitmap13x = 500;
        bitmap13y = 50;
        x_dir13 = 1;
        b31 = true;
        b26 = true;
        b24 = true;
        b23 = true;
        b12 = true;
        b15 = true;
        move = true;
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
        mp1 = MediaPlayer.create(context, R.raw.song8);
        mp2 = MediaPlayer.create(context, R.raw.chimes);
        mp1b=mp2b=true;

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

        canvas.drawText("Inserting an element into a tree", 50, 1900, blackPaintBrushFill);


        if (mp1b) {
            mp1.start();
            mp1b=false;
        }
        //23 to 12
        if (bitmap13x >= 250 && bitmap13y <= 300 &&  b23) {
            x_dir13 = -1;
            y_dir13 =  1;
            canvas.drawText("13 smaller than 23", 250, 1000, blackPaintBrushFill);
            canvas.drawText("13 moves left", 250, 1060, blackPaintBrushFill);

            //Log.d("first move", "" + bitmap13x + ", " + bitmap13y);
        } else {
            if (b23) {
                x_dir13 = 0;
                y_dir13 = 0;
                try {
                    Thread.sleep(500);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b23 = false;
            }
        }
        //12 to 15
        if((bitmap13x <= 400 || bitmap13y <= 550) && b12 && !b23) {
            x_dir13 = 1;
            y_dir13 = 1;
            Log.d("second move", "" + bitmap13x + ", " + bitmap13y);
            canvas.drawText("13 bigger than 12", 250, 1000, blackPaintBrushFill);
            canvas.drawText("13 moves right", 250, 1060, blackPaintBrushFill);

        } else {
            if (b12 && !b23) {
                Log.d("b12 && !b13", "" + bitmap13x + ", " + bitmap13y);

                x_dir13 = 0;
                y_dir13 = 0;

                try {
                    Thread.sleep(500);
                } catch (InterruptedException ex) {
                    Thread.currentThread().interrupt();
                }
                b12 = false;
            }
        }
        if((bitmap13x >= 350 && bitmap13x <= 700) &&  b15 && !b12) { // 350 to 330? more to the left
            Log.d("third move", "" + bitmap13x + ", " + bitmap13y);
            x_dir13 = -1;
            y_dir13 = 1;
            canvas.drawText("13 smaller than 15", 250, 1000, blackPaintBrushFill);
            canvas.drawText("13 moves left", 250, 1060, blackPaintBrushFill);
        } else {
            if (b15 && !b12) {
                Log.d("b15 && !b12", "" + bitmap13x + ", " + bitmap13y);

                x_dir13 = 0;
                y_dir13 = 0;
                b15 = false;
            }
        }
        if(!b15 && bitmap13x <=350 && bitmap13y <=800) {
            y_dir13 = 1;
            canvas.drawText("13 smaller than 15", 250, 1000, blackPaintBrushFill);
            canvas.drawText("13 moves left", 250, 1060, blackPaintBrushFill);
        } else if (!b15 && bitmap13y >= 799) {
            if (mp2b) {
                mp2.start();//-------
                mp2b = false;//---------
                mp1.stop();
            }
            Log.d("last move", "" + bitmap13x + ", " + bitmap13y);
            canvas.drawLine(430,620,380,800, bluePaintBrushStroke);
            canvas.drawText("inserted in order", 250, 1000, blackPaintBrushFill);

            y_dir13 = 0;
        }





        bitmap13x = bitmap13x + x_dir13;
        bitmap13y = bitmap13y + y_dir13;

        canvas.drawBitmap(bitmap1, 0, 800, null);
        canvas.drawBitmap(bitmap5, 100, 550, null);
        canvas.drawBitmap(bitmap9, 200, 800, null);
        canvas.drawBitmap(bitmap12, 250, 300, null);
        canvas.drawBitmap(bitmap13, bitmap13x, bitmap13y, null);
        canvas.drawBitmap(bitmap15, 400, 550, null);
        canvas.drawBitmap(bitmap19, 500, 800, null);
        canvas.drawBitmap(bitmap23, 500, 50, null);
        canvas.drawBitmap(bitmap24, 600, 550, null);
        canvas.drawBitmap(bitmap26, 750, 300, null);
        canvas.drawBitmap(bitmap31, 900, 550, null);
        //23 to 12
        canvas.drawLine(550,120,280,300, redPaintBrushStroke);
        //23 to 26
        canvas.drawLine(550,120,785,300, redPaintBrushStroke);
        //12 to 5
        canvas.drawLine(280,370,135,550, redPaintBrushStroke);
        //12 to 15
        canvas.drawLine(280,370,430,550, redPaintBrushStroke);
        //26 to 24
        canvas.drawLine(780,370,650,550, redPaintBrushStroke);
        //26 to 31
        canvas.drawLine(780,370,950,550, redPaintBrushStroke);
        //5 to 1
        canvas.drawLine(130,620,30,800, redPaintBrushStroke);
        //5 to 9
        canvas.drawLine(130,620,230,800, redPaintBrushStroke);
        //15 to 19
        canvas.drawLine(430,620,530,800, redPaintBrushStroke);

        invalidate();
    }
}