package com.example.mitchelldennen.datastructurevisualization;

import android.app.Activity;
import android.content.Intent;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Display;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;

public class HomeActivity extends AppCompatActivity {

    //private Button animationB;
    private RadioButton arrayRB;
    private RadioButton llRB;
    private RadioButton stackRB;
    private RadioButton queueRB;
    private RadioButton hashRB;
    private RadioButton treeRB;
    private RadioButton graphRB;
    private Button start;
    private Button campare;
    private TextView tV;
    private AnimationActivity mDrawViewA;
    private AnimationActivityLL mDrawViewL;
    private AnimationActivityStack mDrawViewS;
    private AnimationActivityQueue mDrawViewQ;
    private AnimationActivityHash mDrawViewH;
    private AnimationActivityTree mDrawViewT;
    private String viewArr;
    @Override
    protected void onResume() {
        super.onResume();
        //setContentView(R.layout.activity_home);

    }

    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        start = (Button) findViewById(R.id.startButton);
        //mDrawView = new AnimationActivity(this);
        mDrawViewA = new AnimationActivity(this);//getApplicationContext()
        mDrawViewL = new AnimationActivityLL(this,this);
        mDrawViewS = new AnimationActivityStack(this,this);
        mDrawViewQ = new AnimationActivityQueue(this,this);
        mDrawViewH = new AnimationActivityHash(this,this);
        mDrawViewT = new AnimationActivityTree(this,this);

        arrayRB = (RadioButton) findViewById(R.id.arrayButton);
        llRB = (RadioButton) findViewById(R.id.linkedListButton);
        stackRB = (RadioButton) findViewById(R.id.stackButton);
        queueRB = (RadioButton) findViewById(R.id.queueButton);
        hashRB = (RadioButton) findViewById(R.id.hashButton);
        treeRB = (RadioButton) findViewById(R.id.treeButton);
        graphRB = (RadioButton) findViewById(R.id.graphButton);
        //animationB = (Button) findViewById(R.id.compareButton);
        start.setOnClickListener(new View.OnClickListener() {
            @Override

            public void onClick(View v) {
                 /*

        AnimationView animationView = new AnimationView(getApplicationContext());

        // I'm using ConstraintLayout as an example, since I don't know exactly what layout you're using
        ConstraintLayout.LayoutParams params = new ConstraintLayout.LayoutParams(WRAP_CONTENT, WRAP_CONTENT);
        // Set the layout params the way you want

        addContentView(animationView, params); // This is where the magic happens
    }
});
             */
                //AnimationActivity mDrawViewA = new AnimationActivity(getApplicationContext(),this);
                ConstraintLayout.LayoutParams params = new ConstraintLayout.LayoutParams(1000,1400);
                Intent intent = new Intent(getBaseContext(),ViewActivity.class);
                intent.putExtra("VIEW_ARR", viewArr);
                startActivity(intent);
                if(arrayRB.isChecked()) {
                    //setContentView(mDrawViewA);
                    viewArr = "1";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //addContentView(mDrawViewA, params); // This is where the magic happens
                }
                if(llRB.isChecked()) {
                    viewArr = "2";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewL);
                }
                if(stackRB.isChecked()) {
                    viewArr = "3";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewS);
                }
                if(queueRB.isChecked()) {
                    viewArr = "4";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewQ);
                }
                if(hashRB.isChecked()) {
                    viewArr = "5";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewH);
                }
                if(treeRB.isChecked()) {
                    viewArr = "6";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewT);
                }
                if(graphRB.isChecked()) {
                    viewArr = "7";
                    intent.putExtra("VIEW_ARR", viewArr);
                    startActivity(intent);
                    //setContentView(mDrawViewT);
                }


            }
        });


        arrayRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
            });
        llRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
        });
        stackRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
        });
        queueRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
        });
        hashRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
        });
        treeRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
                if(graphRB.isChecked()) {
                    graphRB.setChecked(false);
                }
            }
        });
        graphRB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(llRB.isChecked()) {
                    llRB.setChecked(false);
                }
                if(stackRB.isChecked()) {
                    stackRB.setChecked(false);
                }
                if(queueRB.isChecked()) {
                    queueRB.setChecked(false);
                }
                if(hashRB.isChecked()) {
                    hashRB.setChecked(false);
                }
                if(treeRB.isChecked()) {
                    treeRB.setChecked(false);
                }
                if(arrayRB.isChecked()) {
                    arrayRB.setChecked(false);
                }
            }
        });
        campare = (Button) findViewById(R.id.compareButton);
        campare.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                //saves name and passes it to the next activity
                //next activity
                Intent intent = new Intent(getBaseContext(), CompareActivity.class);//getBaseContext()
                startActivity(intent);
            }
        });



    }

}
