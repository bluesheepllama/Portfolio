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

public class AnimationActivityHash extends View {

    Activity myActivity;
    Paint redPaintBrushFill, bluePaintBrushFill, blackPaintBrushFill;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    Bitmap bitmap1, bitmap5, bitmap9, bitmap15, bitmap19, bitmap13, bitmap31, bitmap12, bitmap24, bitmap26, bitmap23;
    int bitmap13x;
    int x_dir13;
    final MediaPlayer mp1, mp2;
    Boolean mp1b,mp2b;


    public AnimationActivityHash(Context context, Activity activity) {
        super(context);
        myActivity = activity;
        setBackgroundResource(R.drawable.white);
        bitmap13x = 700;
        x_dir13 = 1;

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
        mp1 = MediaPlayer.create(context, R.raw.song6);
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

        canvas.drawText("Inserting an element into a hash", 50, 1900, blackPaintBrushFill);


        if(mp1b) {
            mp1.start();
            mp1b = false;
        }
        if(bitmap13x <= 210) {
            if(mp2b) {
                mp2.start();
                mp2b=false;
            }
        }

        if (bitmap13x >= 200) {
            canvas.drawText("13 hashes in", 300, 500, blackPaintBrushFill);
            bitmap13x = bitmap13x - x_dir13;
        } else {
            canvas.drawText("13 hashed in order", 300, 920, blackPaintBrushFill);
        }
        if(bitmap13x <= 205) {
            mp1.stop();
        }

        canvas.drawBitmap(bitmap1, 200, 200, null);
        canvas.drawBitmap(bitmap5, 200, 280, null);
        canvas.drawBitmap(bitmap9, 200, 520, null);
        canvas.drawBitmap(bitmap12, 200, 600, null);
        canvas.drawBitmap(bitmap13, bitmap13x, 680, null);
        canvas.drawBitmap(bitmap15, 200, 840, null);
        canvas.drawBitmap(bitmap19, 200, 920, null);
        canvas.drawBitmap(bitmap23, 200, 1000, null);
        canvas.drawBitmap(bitmap24, 200, 1160, null);
        canvas.drawBitmap(bitmap26, 200, 1240, null);
        canvas.drawBitmap(bitmap31, 200, 1400, null);

        canvas.drawLine(200,200,280,200, redPaintBrushStroke);
        canvas.drawLine(200,200,200,1480, redPaintBrushStroke);
        canvas.drawLine(200,1480,280,1480, redPaintBrushStroke);
        canvas.drawLine(280,1480,280,200, redPaintBrushStroke);

        invalidate();
    }
}