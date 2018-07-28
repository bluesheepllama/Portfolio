package com.example.mitchelldennen.datastructurevisualization;

import android.content.Intent;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

public class ViewActivity extends AppCompatActivity {

    private AnimationActivity mDrawViewA;
    private AnimationActivityLL mDrawViewL;
    private AnimationActivityStack mDrawViewS;
    private AnimationActivityQueue mDrawViewQ;
    private AnimationActivityHash mDrawViewH;
    private AnimationActivityTree mDrawViewT;
    private AnimationActivityGraph mDrawViewG;
    private int viewArr;
    private String temp;
    private Button back;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view);

        mDrawViewA = new AnimationActivity(this);
        mDrawViewL = new AnimationActivityLL(this,this);
        mDrawViewS = new AnimationActivityStack(this,this);
        mDrawViewQ = new AnimationActivityQueue(this,this);
        mDrawViewH = new AnimationActivityHash(this,this);
        mDrawViewT = new AnimationActivityTree(this,this);
        mDrawViewG = new AnimationActivityGraph(this,this);

        temp = new String("");
        temp ="";
        //String temp;
        temp = getIntent().getStringExtra("VIEW_ARR");
        ConstraintLayout.LayoutParams params = new ConstraintLayout.LayoutParams(1000,1400);
        //Log.d("temp", " " + temp);
        Intent intent = new Intent(getBaseContext(),HomeActivity.class);

        if( temp == null) {
            temp = "10";
            startActivity(intent);
        }
        //viewArr = Integer
        Log.d("temp", " " + temp);
        if(temp.equals("1")) {
            setContentView(mDrawViewA);
            //addContentView(mDrawViewA, params); // This is where the magic happens
        }
        else if(temp.equals("2")) {
            setContentView(mDrawViewL);
        }
        else if(temp.equals("3")) {
            setContentView(mDrawViewS);
        }
        else if(temp.equals("4")) {
            setContentView(mDrawViewQ);
        }
        else if(temp.equals("5")) {
            setContentView(mDrawViewH);
        }
        else if(temp.equals("6")) {
            setContentView(mDrawViewT);
        }
        else if(temp.equals("7")) {
            setContentView(mDrawViewG);
        }
        back = (Button) findViewById(R.id.backButton);
        /*back.setOnClickListener(new View.OnClickListener() {
            @Override

            public void onClick(View v) {

                Intent intent = new Intent(getBaseContext(),HomeActivity.class);
                startActivity(intent);
            }
        });*/
    }
}
