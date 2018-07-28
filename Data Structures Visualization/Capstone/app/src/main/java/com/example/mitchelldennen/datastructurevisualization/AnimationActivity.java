package com.example.mitchelldennen.datastructurevisualization;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.media.MediaPlayer;
import android.view.View;


public class AnimationActivity extends View {

    Paint redPaintBrushFill,bluePaintBrushFill,blackPaintBrushFill;
    Paint redPaintBrushStroke,bluePaintBrushStroke, white;

    Bitmap bitmap1,bitmap5,bitmap9,bitmap15,bitmap19,bitmap13,bitmap31,bitmap12,bitmap24,bitmap26,bitmap23;
    int bitmap31x,bitmap26x,bitmap24x,bitmap23x,bitmap19x,bitmap15x,bitmap13x,bitmap13y;
    int x_dir31,x_dir26,x_dir24,x_dir23,x_dir19,x_dir15,x_dir13, y_dir13;
    int x1,y1,x2,y2,x3,y3,x4,y4;
    Boolean b31,b26,b24,b23,b19,b15, mp1b, mp2b, mp3b;
    final MediaPlayer mp1, mp2, mp3;
    Rect whiteR;

    public AnimationActivity(Context context) {
        super(context);
        setBackgroundResource(R.drawable.white);
        bitmap13y = 1000;
        bitmap13x = 730;
        bitmap15x = 370;
        bitmap19x = 450;
        bitmap23x = 530;
        bitmap24x = 610;
        bitmap26x = 690;
        bitmap31x = 770;
        x_dir31 = 1;
        x_dir26 = 1;
        x_dir24 = 1;
        x_dir23 = 1;
        x_dir19 = 1;
        x_dir15 = 1;
        x_dir13 = 1;
        y_dir13 = 1;
        b31 = true;
        b26 = true;
        b24 = true;
        b23 = true;
        b19 = true;
        b15 = true;
        mp1b = true;
        mp2b = true;
        mp3b = true;
        x1 = 810;
        y1 = 900;
        x2 = 810;
        y2 = 1050;
        x3 = 840;
        y3 = 1030;
        x4 = 780;
        y4 = 1030;
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
        mp1 = MediaPlayer.create(context, R.raw.song10);
        mp2 = MediaPlayer.create(context, R.raw.chimes);
        mp3 = MediaPlayer.create(context, R.raw.cash);
        whiteR = new Rect();

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

        white = new Paint();
        white.setColor(Color.WHITE);
        white.setStyle(Paint.Style.FILL);

        canvas.drawText("Inserting an element into an array", 50, 1900, blackPaintBrushFill);


        if(mp1b) {
            mp1.start();
            mp1b = false;
        }


        if ((bitmap13x >= 650 &&  b31 && bitmap31x >=850) || (bitmap13x >= 570 &&  b26 && bitmap26x >= 770) || (bitmap13x >= 490 &&  b24 && bitmap24x >= 690) ||
                (bitmap13x >= 410  && b23 && bitmap23x >= 610) || (bitmap13x >= 372  && b19 && bitmap19x >=530)) {
            x_dir13 = -1;
        } else {
            x_dir13 = 0;

            }
        //1


        if (bitmap31x <= 850) {
            x_dir31 = 1;
            b31 = false;
            canvas.drawText("31 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir31 = 0;
            b31 = true;
            //canvas.drawRect(100, 800, 150, 900, white);

        }
        //2
        if (bitmap26x <= 770 && bitmap31x >= 850 && bitmap13x <= 650) {
            x_dir26 = 1;
            b26 = false;
            canvas.drawText("26 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir26 = 0;
            b26 = true;
        }
            //3
        if (bitmap24x <= 690 && bitmap26x >= 770 && bitmap13x <= 570) {
            x_dir24 = 1;
            canvas.drawText("24 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir24 = 0;
            b24 = true;
        }
        //4
        if (bitmap23x <= 610 && bitmap24x >= 690 && bitmap13x <= 490) {
            x_dir23 = 1;
            canvas.drawText("23 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir23 = 0;
            b23 = true;
        }
        //5
        if (bitmap19x <= 530 && bitmap23x >= 610 && bitmap13x <= 410) {
            x_dir19 = 1;
            canvas.drawText("19 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir19 = 0;
            b19 = true;

        }
        //6
        if (bitmap15x <= 450 && bitmap19x >= 530 && bitmap13x <= 372) {
            x_dir15 = 1;
            canvas.drawText("15 bigger than 13", 100, 800, blackPaintBrushFill);

        } else {
            x_dir15 = 0;
            b15 = true;

        }
        //move 13 down
        if (bitmap13y <= 1100 && bitmap15x >= 450) {
            if (mp2b) {
                mp1.stop();
                mp2.start();
                mp2b = false;

            }
            canvas.drawText("13 bigger than 12", 100, 800, blackPaintBrushFill);
            canvas.drawText("insert here", 100, 1300, blackPaintBrushFill);


            y_dir13 = 1;
        } else {
            y_dir13 = 0;
        }


        bitmap31x = bitmap31x + x_dir31;



        bitmap26x = bitmap26x + x_dir26;
        bitmap24x = bitmap24x + x_dir24;
        bitmap23x = bitmap23x + x_dir23;
        bitmap19x = bitmap19x + x_dir19;
        bitmap15x = bitmap15x + x_dir15;
        bitmap13x = bitmap13x + x_dir13;
        bitmap13y = bitmap13y + y_dir13;



        canvas.drawBitmap(bitmap1, 50, 1100, null);
        canvas.drawBitmap(bitmap5, 130, 1100, null);
        canvas.drawBitmap(bitmap9, 210, 1100, null);
        canvas.drawBitmap(bitmap12, 290, 1100, null);
        canvas.drawBitmap(bitmap13, bitmap13x, bitmap13y, null);
        canvas.drawBitmap(bitmap15, bitmap15x, 1100, null);
        canvas.drawBitmap(bitmap19, bitmap19x, 1100, null);
        canvas.drawBitmap(bitmap23, bitmap23x, 1100, null);
        canvas.drawBitmap(bitmap24, bitmap24x, 1100, null);
        canvas.drawBitmap(bitmap26, bitmap26x, 1100, null);
        canvas.drawBitmap(bitmap31, bitmap31x, 1100, null);



        this.invalidate();
    }
}
