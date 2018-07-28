package com.example.mitchelldennen.datastructurevisualization;

import android.support.annotation.NonNull;
import android.util.Log;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Queue;

/**
 * Created by mitchelldennen on 2/10/18.
 */

public class QueueClass {
    private Queue<Integer> queueSmall;
    private Queue<Integer> queueBig;
    private long[] timesSearch;
    private long[] timesSort;
    private long[] timesInsert;
    private long[] timesDelete;

     QueueClass() {
        this.queueSmall = new LinkedList<Integer>();
        this.queueBig = new LinkedList<Integer>();
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];

     }

    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {
        /*for(int i = 0; i <= small.size(); i++) {
            this.queueSmall.add(small.get(i));
        }*/

        for(Integer i:small) {
            //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            this.queueSmall.add(i);
            Log.d("queuePopulate", "" + this.queueSmall.peek());
            Log.d("queuePopulateArray", "" + small.get(i));
        }

        for(int i = 0; i < big.size(); i++) {
            this.queueBig.offer(big.get(i));
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

        for(int i = 0; i <= 100; i++) {
            tempSmall.add(this.queueSmall.poll());//------------
        }
        //Collections.sort(tempSmall);//doesnt like----------------------------------------------------------------

        for(int i = 100-1; i > 0; i--) {
            this.queueSmall.offer(tempSmall.get(i));//---------------
        }
        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        for(int i = 0; i <= 10000; i++) {
            tempBig.add(this.queueBig.poll());
        }
        //Collections.sort(tempBig);---------------------------------------

        for(int i = 10000-1; i >= 0; i--) {
            this.queueBig.offer(tempBig.get(i));
        }

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
        //populate(small,big);
        startTimeSmall = System.nanoTime();
        this.queueSmall.contains(tofind);//will this method screw up time or should i search myself?----------------
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.queueBig.contains(tofind); ////////////////------------
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
        ArrayList<Integer> tempSmall = new ArrayList<>();
        ArrayList<Integer> tempBig = new ArrayList<>();

        //queue into array
        for(int i = 0; i <= 100; i++) {
            tempSmall.add(this.queueSmall.poll());
        }
        Log.d("queueclassqueue"," " + this.queueSmall.peek());

        Log.d("queueclass"," " + tempSmall.get(5));
        //insert small
        for(int i = tempSmall.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (tempSmall.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            tempSmall.add(i+1,tempSmall.get(i));      // shift elements forward
            if (tempSmall.get(i) <= value) {       // if we found the right spot
                tempSmall.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }
        //array into queue
        for(int i = 100-1; i >= 0; i--) {
            this.queueSmall.add(tempSmall.get(i));
        }


        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------
        for(int i = 0; i <= 10000; i++) {
            tempSmall.add(this.queueBig.poll());
        }
        //insert big
        for(int i = tempBig.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (tempBig.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            tempBig.add(i+1,tempBig.get(i));      // shift elements forward
            if (tempBig.get(i) <= value) {       // if we found the right spot
                tempBig.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }
        for(int i = 10000-1; i >= 0; i--) {
            this.queueBig.add(tempSmall.get(i));
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

        //delete---------------
        this.queueSmall.remove(value);

        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //delete------------
        this.queueBig.remove(value);

        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        return this.timesDelete;
    }
}
