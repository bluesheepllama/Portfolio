package com.example.mitchelldennen.datastructurevisualization;

import android.util.Log;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
/**
 * Created by mitchelldennen on 2/10/18.
 */

public class HashClass {

    private HashSet<Integer> hashSmall;
    private HashSet<Integer> hashBig;
    private long[] timesSearch;
    private long[] timesSort;
    private long[] timesInsert;
    private long[] timesDelete;

     HashClass() {
        this.hashSmall = new HashSet<>();
        this.hashBig = new HashSet<>();
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];
    }

    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {
        for(Integer i:small) {
            //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            this.hashSmall.add(i);
        }
        for(int i = 0; i < big.size(); i++) {
            this.hashBig.add(big.get(i));
        }

    }

    public long[] sort() {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();
        /*
        List sortedList = new ArrayList(yourHashSet);
        Collections.sort(sortedList);
        */
         List tempSmall = new ArrayList(this.hashSmall);
         Collections.sort(tempSmall);

        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        List tempBig = new ArrayList(this.hashBig);
        Collections.sort(tempBig);

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
        this.hashSmall.contains(tofind);//will this method screw up time or should i search myself?----------------
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.hashSmall.contains(tofind); ////////////////------------
        endTimeBig = System.nanoTime();
        this.timesSearch[1] = endTimeBig - startTimeBig;
        return this.timesSearch;
    }

    public long[] insert(Integer value) {

        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        //insert---------------
        //ArrayList<Integer> tempSmall = new ArrayList<>();
        //ArrayList<Integer> tempBig = new ArrayList<>();

        /*

         */
        //stack into array
        //ArrayList<Integer> tempSmall = new ArrayList<>();
        //ArrayList<Integer> tempBig = new ArrayList<>();
        //Integer[] tempSmall;
        //tempSmall = new Integer[100];
        Integer[] tempSmall = this.hashSmall.toArray(new Integer[this.hashSmall.size()+1]);

        //insert small
        Log.d("hashsmall","" + value + " " + tempSmall[5]);
        for(int i = this.hashSmall.size()-1; i >= 0; i--) {//does add overright elements when inserted?--------------
            if (tempSmall[i] == 0) continue; // skip last elements to avoid array index out of bound
            tempSmall[i+1] = tempSmall[i];      // shift elements forward //out of bounds ---------------------
            if (tempSmall[i] <= value) {       // if we found the right spot
                tempSmall[i] = value;        // place the new element and
                break;                 // break out the loop
            }
        }
        //array into stack
        for(int i = this.hashSmall.size(); i >= 0; i--) {
            this.hashSmall.add(tempSmall[i]);
        }


        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------

        Integer[] tempBig = this.hashBig.toArray(new Integer[this.hashBig.size()+1]);
        //insert big
        /*for(int i = tempBig.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (tempBig.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            tempBig.add(i+1,tempBig.get(i));      // shift elements forward
            if (tempBig.get(i) <= value) {       // if we found the right spot
                tempBig.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }*/
        for(int i = this.hashBig.size()-1; i >= 0; i--) {//does add overright elements when inserted?--------------
            if (tempBig[i] == 0) continue; // skip last elements to avoid array index out of bound
            tempBig[i+1] = tempBig[i];      // shift elements forward
            if (tempBig[i] <= value) {       // if we found the right spot
                tempBig[i] = value;        // place the new element and
                break;                 // break out the loop
            }
        }
        for(int i = this.hashBig.size(); i >= 0; i--) {
            this.hashBig.add(tempBig[i]);
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
        this.hashSmall.remove(value);
        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //delete------------
        this.hashBig.remove(value);
        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        return this.timesDelete;
    }
}
