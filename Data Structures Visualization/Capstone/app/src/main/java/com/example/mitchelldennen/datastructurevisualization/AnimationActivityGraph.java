package com.example.mitchelldennen.datastructurevisualization;

import android.app.Activity;
import android.content.Context;

import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.media.MediaPlayer;
import android.view.View;
/**
 * Created by mitchelldennen on 4/11/18.
 */

public class AnimationActivityGraph extends View {
    Activity myActivity;
    Paint redPaintBrushFill, bluePaintBrushFill, blackPaintBrushFill, white;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    int x,y,y_dir;
    final MediaPlayer mp1, mp2;
    Boolean mp1b,mp2b;

    public AnimationActivityGraph(Context context,Activity activity) {
        super(context);
        myActivity = activity;
        setBackgroundResource(R.drawable.white);
        x = 10;
        y = 5;
        y_dir = 0;
        mp1 = MediaPlayer.create(context, R.raw.song11);
        mp2 = MediaPlayer.create(context, R.raw.chimes);
        mp1b = mp2b = true;

    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        redPaintBrushFill = new Paint();
        redPaintBrushFill.setColor(Color.RED);
        redPaintBrushFill.setStyle(Paint.Style.FILL);
        redPaintBrushFill.setTextSize(60);

        bluePaintBrushFill = new Paint();
        bluePaintBrushFill.setColor(Color.BLUE);
        bluePaintBrushFill.setStyle(Paint.Style.FILL);
        bluePaintBrushFill.setTextSize(60);

        blackPaintBrushFill = new Paint();
        blackPaintBrushFill.setColor(Color.BLACK);
        blackPaintBrushFill.setStyle(Paint.Style.FILL);
        blackPaintBrushFill.setTextSize(60);

        redPaintBrushStroke = new Paint();
        redPaintBrushStroke.setColor(Color.RED);
        redPaintBrushStroke.setStyle(Paint.Style.STROKE);
        redPaintBrushStroke.setStrokeWidth(10);

        bluePaintBrushStroke = new Paint();
        bluePaintBrushStroke.setColor(Color.BLUE);
        bluePaintBrushStroke.setStyle(Paint.Style.STROKE);
        bluePaintBrushStroke.setStrokeWidth(10);

        white = new Paint();
        white.setColor(Color.WHITE);
        white.setStyle(Paint.Style.FILL);

        canvas.drawText("Finding the cheapest path", 50, 2000, blackPaintBrushFill);


        if(mp1b) {
            mp1.start();
            mp1b = false;
        }


        canvas.drawCircle(500, 100, 50, bluePaintBrushFill);
        canvas.drawCircle(275, 400, 50, redPaintBrushFill);
        canvas.drawCircle(775, 400, 50, redPaintBrushFill);
        canvas.drawCircle(500, 700, 50, redPaintBrushFill);
        canvas.drawCircle(275, 1000, 50, redPaintBrushFill);
        canvas.drawCircle(775, 1000, 50, redPaintBrushFill);
        canvas.drawCircle(500, 1300, 50, redPaintBrushFill);
        canvas.drawCircle(275, 1600, 50, redPaintBrushFill);
        canvas.drawCircle(775, 1600, 50, redPaintBrushFill);
        canvas.drawCircle(500, 1900, 50, redPaintBrushFill);


        canvas.drawLine(475, 135, 275, 400, redPaintBrushStroke);
        canvas.drawText("11", 320, 270, blackPaintBrushFill);
        canvas.drawLine(535, 135, 775, 400, redPaintBrushStroke);
        canvas.drawText("12", 660, 270, blackPaintBrushFill);
        canvas.drawLine(275, 400, 500, 700, redPaintBrushStroke);
        canvas.drawText("6", 335, 570, blackPaintBrushFill);
        canvas.drawLine(275, 400, 275, 1000, redPaintBrushStroke);
        canvas.drawText("9", 230, 700, blackPaintBrushFill);
        canvas.drawLine(775, 400, 775, 1000, redPaintBrushStroke);
        canvas.drawText("8", 790, 700, blackPaintBrushFill);
        canvas.drawLine(775, 400, 500, 700, redPaintBrushStroke);
        canvas.drawText("5", 660, 570, blackPaintBrushFill);//-^good
        canvas.drawLine(500, 700, 775, 1000, redPaintBrushStroke);

        canvas.drawText("1", 660, 870, blackPaintBrushFill);
        canvas.drawLine(500, 700, 275, 1000, redPaintBrushStroke);
        canvas.drawText("2", 330, 870, blackPaintBrushFill);

        canvas.drawLine(775, 1000, 500, 1300, redPaintBrushStroke);
        canvas.drawText("9", 660, 1170, blackPaintBrushFill);
        canvas.drawLine(775, 1000, 775, 1600, redPaintBrushStroke);
        canvas.drawText("12", 790, 1300, blackPaintBrushFill);
        canvas.drawLine(275, 1000, 500, 1300, redPaintBrushStroke);
        canvas.drawText("11", 320, 1170, blackPaintBrushFill);
        canvas.drawLine(275, 1000, 275, 1600, redPaintBrushStroke);
        canvas.drawText("8", 230, 1300, blackPaintBrushFill);

        canvas.drawLine(500, 1300, 775, 1600, redPaintBrushStroke);
        canvas.drawText("7", 660, 1470, blackPaintBrushFill);
        canvas.drawLine(500, 1300, 275, 1600, redPaintBrushStroke);
        canvas.drawText("5", 320, 1470, blackPaintBrushFill);
        canvas.drawLine(500, 1300, 500, 1850, redPaintBrushStroke);
        canvas.drawText("13", 515, 1600, blackPaintBrushFill);
        canvas.drawLine(775, 1600, 525, 1875, redPaintBrushStroke);
        canvas.drawText("8", 660, 1770, blackPaintBrushFill);
        canvas.drawLine(275, 1600, 500, 1900, redPaintBrushStroke);
        canvas.drawText("7", 330, 1770, blackPaintBrushFill);

        canvas.drawCircle(x, y, 2, white);
        y_dir = 1;
        y = y + y_dir;

        if (y>100) {
            canvas.drawLine(500, 100, 275, 400, bluePaintBrushStroke);
            canvas.drawCircle(275, 400, 50, bluePaintBrushFill);
            canvas.drawText("11 < 12", 50, 300, blackPaintBrushFill);


        }
        if (y>200) {
            canvas.drawLine(275, 400, 500, 700, bluePaintBrushStroke);
            canvas.drawCircle(500, 700, 50, bluePaintBrushFill);
            canvas.drawText("6 < 9", 50, 600, blackPaintBrushFill);



        }
        if(y>300) {
            canvas.drawLine(500, 700, 775, 1000, bluePaintBrushStroke);
            canvas.drawCircle(775, 1000, 50, bluePaintBrushFill);
            canvas.drawText("1 < 2", 800, 800, blackPaintBrushFill);



        }
        if(y>400) {
            canvas.drawLine(775, 1000, 500, 1300, bluePaintBrushStroke);
            canvas.drawCircle(500, 1300, 50, bluePaintBrushFill);
            canvas.drawText("9 < 12", 800, 1150, blackPaintBrushFill);



        }
        if(y>500) {
            canvas.drawLine(500, 1300, 275, 1600, bluePaintBrushStroke);
            canvas.drawCircle(275, 1600, 50, bluePaintBrushFill);
            canvas.drawText("5 < 7 < 13", 5, 1450, blackPaintBrushFill);


        }
        if(y>600) {
            canvas.drawLine(275, 1600, 500, 1900, bluePaintBrushStroke);
            canvas.drawCircle(500, 1900, 50, bluePaintBrushFill);

            if(mp2b) {
                mp2.start();
                mp2b=false;
                //mp1.stop();
            }

        }
        if (y > 700) {
            if(y%10==5)
                canvas.drawText("TOTAL WEIGHT OF 39", 10, 100, bluePaintBrushFill);
            if(y%10==0)
                canvas.drawText("TOTAL WEIGHT OF 39", 10, 100, redPaintBrushFill);
        }







        invalidate();
    }
}
