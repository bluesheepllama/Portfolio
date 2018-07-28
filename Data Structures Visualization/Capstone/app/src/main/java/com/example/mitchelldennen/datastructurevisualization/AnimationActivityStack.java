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

public class AnimationActivityStack extends View {
    Activity myActivity;
    Paint redPaintBrushFill, bluePaintBrushFill, blackPaintBrushFill;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    Bitmap bitmap1, bitmap5, bitmap9, bitmap15, bitmap19, bitmap13, bitmap31, bitmap12, bitmap24, bitmap26, bitmap23;
    int  bitmap13y;
    int y_dir13;
    final MediaPlayer mp1, mp2;
    Boolean mp1b,mp2b;


    public AnimationActivityStack(Context context,Activity activity) {
        super(context);
        myActivity = activity;
        setBackgroundResource(R.drawable.white);
        bitmap13y = 1300;
        y_dir13 = 1;
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
        mp1 = MediaPlayer.create(context, R.raw.song2);
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


        if (mp1b) {
            mp1.start();
        }

        if(bitmap13y <=1050 &&mp2b) {
            mp2.start();
            mp2b=false;
        }
        if(bitmap13y >= 1000) {
            bitmap13y = bitmap13y - y_dir13;
            canvas.drawText("13 pushes to the bottom", 250, 140, blackPaintBrushFill);

        }
        else {
            mp1.stop();
            canvas.drawText("cannot insert in order", 250, 1200, blackPaintBrushFill);

        }

        canvas.drawText("Inserting an element into an stack", 50, 1900, blackPaintBrushFill);


        canvas.drawBitmap(bitmap1, 500, 200, null);
        canvas.drawBitmap(bitmap5, 500, 280, null);
        canvas.drawBitmap(bitmap9, 500, 360, null);
        canvas.drawBitmap(bitmap12, 500, 440, null);
        canvas.drawBitmap(bitmap13, 500, bitmap13y, null);
        canvas.drawBitmap(bitmap15, 500, 520, null);
        canvas.drawBitmap(bitmap19, 500, 600, null);
        canvas.drawBitmap(bitmap23, 500, 680, null);
        canvas.drawBitmap(bitmap24, 500, 760, null);
        canvas.drawBitmap(bitmap26, 500, 840, null);
        canvas.drawBitmap(bitmap31, 500, 920, null);


        invalidate();
    }
}