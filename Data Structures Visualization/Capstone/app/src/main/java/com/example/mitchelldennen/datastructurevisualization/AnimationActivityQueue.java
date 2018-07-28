package com.example.mitchelldennen.datastructurevisualization;

        import android.app.Activity;
        import android.content.Context;
        import android.graphics.Bitmap;
        import android.graphics.BitmapFactory;
        import android.graphics.Canvas;
        import android.graphics.Color;
        import android.graphics.Paint;
        import android.media.MediaPlayer;
        import android.view.View;
/**
 * Created by mitchelldennen on 3/7/18.
 */

public class AnimationActivityQueue extends View {
    Activity myActivity;
    Paint redPaintBrushFill, bluePaintBrushFill,blackPaintBrushFill;
    Paint redPaintBrushStroke, bluePaintBrushStroke;
    Bitmap bitmap1, bitmap5, bitmap9, bitmap15, bitmap19, bitmap13, bitmap31, bitmap12, bitmap24, bitmap26, bitmap23;
    int  bitmap13x;
    int x_dir13;
    final MediaPlayer mp1, mp2;
    Boolean mp1b,mp2b;


    public AnimationActivityQueue(Context context,Activity activity) {
        super(context);
        myActivity = activity;
        setBackgroundResource(R.drawable.white);
        bitmap13x = 0;
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
        mp1 = MediaPlayer.create(context, R.raw.song5);
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

        canvas.drawText("Inserting an element into a queue", 50, 1900, blackPaintBrushFill);


        if(mp1b) {
            mp1.start();
        }
        if(bitmap13x >= 180) {
            if(mp2b) {
                mp2.start();
                mp2b=false;
            }
        }
        if(bitmap13x <= 200) {
            bitmap13x = bitmap13x + x_dir13;
            canvas.drawText("13 offers to the left", 250, 900, blackPaintBrushFill);

        } else{
            mp1.stop();
        }
        if(bitmap13x >= 195) {
            canvas.drawText("cannot insert in order", 250, 1300, blackPaintBrushFill);

            mp1.stop();
        }

        canvas.drawBitmap(bitmap1, 280, 1100, null);
        canvas.drawBitmap(bitmap5, 360, 1100, null);
        canvas.drawBitmap(bitmap9, 440, 1100, null);
        canvas.drawBitmap(bitmap12, 520, 1100, null);
        canvas.drawBitmap(bitmap13, bitmap13x, 1100, null);
        canvas.drawBitmap(bitmap15, 600, 1100, null);
        canvas.drawBitmap(bitmap19, 680, 1100, null);
        canvas.drawBitmap(bitmap23, 760, 1100, null);
        canvas.drawBitmap(bitmap24, 840, 1100, null);
        canvas.drawBitmap(bitmap26, 920, 1100, null);
        canvas.drawBitmap(bitmap31, 1000, 1100, null);

        invalidate();
    }
}