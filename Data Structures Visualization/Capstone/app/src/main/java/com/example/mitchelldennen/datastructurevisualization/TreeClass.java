package com.example.mitchelldennen.datastructurevisualization;

import android.util.Log;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.TreeSet;

/**
 * Created by mitchelldennen on 2/10/18.
 */

public class TreeClass {

    private TreeSet<Integer> treeSmall;
    private TreeSet<Integer> treeBig;
    private long[] timesSort;
    private long[] timesSearch;
    private long[] timesInsert;
    private long[] timesDelete;


    TreeClass() {
        this.treeSmall = new TreeSet<>();
        this.treeBig = new TreeSet<>();
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];
    }


    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {
        for(Integer i:small) {
            //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            this.treeSmall.add(i);
        }
        for(int i = 0; i < big.size(); i++) {
            this.treeBig.add(big.get(i));
        }

    }

    public long[] sort() {

        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        List tempSmall = new ArrayList(this.treeSmall);
        Collections.sort(tempSmall);

        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        List tempBig = new ArrayList(this.treeBig);
        Collections.sort(tempBig);

        endTimeBig = System.nanoTime();
        this.timesSort[1] = endTimeBig - startTimeBig;
        //display array for testing purposes
        Log.d("treeSort", "" + this.timesSort[0] + " " + this.timesSort[1]);
        return this.timesSort;
    }

    public long[] search(Integer tofind) {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        //populate(small,big);
        startTimeSmall = System.nanoTime();
        this.treeSmall.contains(tofind);//will this method screw up time or should i search myself?----------------
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.treeSmall.contains(tofind); ////////////////------------
        endTimeBig = System.nanoTime();
        this.timesSearch[1] = endTimeBig - startTimeBig;
        Log.d("treeSearch", "" + this.timesSearch[0] + " " + this.timesSearch[1]);

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

        //stack into array
        for(int i = 0; i < this.treeSmall.size(); i++) {
            tempSmall.add(this.treeSmall.pollFirst());//----------------------
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
        for(int i = this.treeSmall.size(); i >= 0; i--) {
            this.treeSmall.add(tempSmall.get(i));
        }


        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------
        for(int i = 0; i < this.treeBig.size(); i++) {
            tempSmall.add(this.treeBig.pollFirst());
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
        for(int i = this.treeBig.size(); i >= 0; i--) {
            this.treeBig.add(tempSmall.get(i));
        }

        endTimeBig = System.nanoTime();
        this.timesInsert[1] = endTimeBig - startTimeBig;
        Log.d("treeInsert", "" + this.timesInsert[0] + " " + this.timesInsert[1]);

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
        this.treeSmall.remove(value);
        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //delete------------
        this.treeBig.remove(value);
        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        Log.d("treeDelete", "" + this.timesDelete[0] + " " + this.timesDelete[1]);

        return this.timesDelete;
    }
    public long[] getTimesSort() {
         return timesSort;
    }
    public long[] getTimesSearch() {
        return timesSearch;
    }
    public long[] getTimesInsert() {
        return timesInsert;
    }
    public long[] getTimesDelete() {
        return timesDelete;
    }
}

