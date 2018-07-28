package com.example.mitchelldennen.datastructurevisualization;

import android.util.Log;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Stack;

/**
 * Created by mitchelldennen on 2/10/18.
 */

public class StackClass {

    private Stack<Integer> stackSmall;
    private Stack<Integer> stackBig;
    private long[] timesSort;
    private long[] timesSearch;
    private long[] timesInsert;
    private long[] timesDelete;


     StackClass() {
        this.stackSmall = new Stack<>();
        this.stackBig = new Stack<>();
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];
    }

    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {
        /*for(int i = 0; i <= small.size(); i++) {
            stackSmall.push(small.get(i));
        }
        */
        for(Integer i:small) {
            //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            stackSmall.push(small.get(i));
        }
        for(int i = 0; i < big.size(); i++) {
                stackBig.push(big.get(i));
        }

    }

    public long[] sort() {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        ArrayList<Integer> tempSmall = new ArrayList<Integer>(100);
        ArrayList<Integer> tempBig = new ArrayList<Integer>(10000);

        startTimeSmall = System.nanoTime();
        for(int i = 0; i < 100; i++) {
            tempSmall.add(this.stackSmall.pop());
        }
        Collections.sort(tempSmall);

        //Log.d("stackclass","" + tempSmall.get(51));
        for(Integer i:tempSmall) {
            stackSmall.push(tempSmall.get(i));
        }
        //Collections.sort(this.stackSmall);// is this the best way??=------------

        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        for(int i = 0; i < 10000; i++) {
            tempBig.add(this.stackBig.pop());
        }
        Collections.sort(tempBig);
        for(int i = 10000-1; i > 0; i--) {
            //Log.d("stackclass","" + tempBig.get(51));

            this.stackBig.push(tempBig.get(i));
        }
        //Collections.sort(this.stackBig);


        endTimeBig = System.nanoTime();
        this.timesSort[1] = endTimeBig - startTimeBig;
        //display array for testing purposes
        return this.timesSort;
    }

    public long[] search(Integer tofind) {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();
        this.stackSmall.contains(tofind);//will this method screw up time or should i search myself?----------------lookup documentation
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.stackBig.contains(tofind); ////////////////------------
        endTimeBig = System.nanoTime();
        this.timesSearch[1] = endTimeBig - startTimeBig;
        return this.timesSearch;
    }

    public long[] insert(Integer value) {

        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        //populate(small,big);
        //sort(small,big); // pass in class variables?----------------------
        startTimeSmall = System.nanoTime();

        //insert---------------
        ArrayList<Integer> tempSmall = new ArrayList<Integer>(100);
        ArrayList<Integer> tempBig = new ArrayList<Integer>(10000);

        //stack into array
        for(int i = 0; i < 100; i++) {
            tempSmall.add(this.stackSmall.pop());
        }
        //insert small
        for(int i = tempSmall.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (tempSmall.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            tempSmall.add(i+1,tempSmall.get(i));      // shift elements forward
            if (tempSmall.get(i) <= value) {       // if we found the right spot
                tempSmall.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }
        //array into stack
        for(int i = 100-1; i > 0; i--) {
            this.stackSmall.push(tempSmall.get(i));
        }


        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------
        for(int i = 0; i < 10000; i++) {
            tempBig.add(this.stackBig.pop());
        }
        //insert bigl
        for(int i = 10000 - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (tempBig.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            tempBig.add(i+1,tempBig.get(i));      // shift elements forward
            if (tempBig.get(i) <= value) {       // if we found the right spot
                tempBig.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }
        for(int i = 10000-1; i > 0; i--) {
            this.stackBig.push(tempBig.get(i));
        }


        endTimeBig = System.nanoTime();
        this.timesInsert[1] = endTimeBig - startTimeBig;
        return this.timesInsert;
    }
    public long[] delete(Integer value) {

        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        //populate(small,big);
        //sort(small,big); // pass in class variables?----------------------
        startTimeSmall = System.nanoTime();
        this.stackSmall.remove(value);
        //delete---------------

        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.stackBig.remove(value);

        //delete------------

        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        return this.timesDelete;
    }
}
