package com.example.mitchelldennen.datastructurevisualization;

import org.w3c.dom.Node;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Collections;
/**
 * Created by mitchelldennen on 2/10/18.
 */

public class LinkedListClass {


    private LinkedList<Integer> linkedListSmall;
    private LinkedList<Integer> linkedListBig;
    private long[] timesSort;
    private long[] timesSearch;
    private long[] timesInsert;
    private long[] timesDelete;

     LinkedListClass() {
        linkedListSmall = new LinkedList<>();
        linkedListBig = new LinkedList<>();
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];
    }

    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {
    /*for(int i = 0; i < 100-1; i++) {
        this.linkedListSmall.add(small.get(i));
    }*/
        for(Integer i:small) {
            //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            this.linkedListSmall.add(i);
        }
        for(int i = 0; i < 10000; i++) {
            this.linkedListBig.add(big.get(i));
        }

    }

    public long[] sort() {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        Collections.sort(this.linkedListSmall);// is this the best way??=------------

        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        Collections.sort(this.linkedListBig);


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
        this.linkedListSmall.contains(tofind);//will this method screw up time or should i search myself?----------------
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.linkedListBig.contains(tofind); ////////////////------------
        endTimeBig = System.nanoTime();
        this.timesSearch[1] = endTimeBig - startTimeBig;
        return this.timesSearch;
    }

    public long[] insert(Integer value) {

        Integer index = 0;
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        //insert---------------
        index = this.linkedListSmall.indexOf(value);// better way?---------
        if(index != -1)
            this.linkedListSmall.add(index, value);

        /*
        */


        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------
        index = this.linkedListBig.indexOf(value);// better way?------------
        if(index != -1)
            this.linkedListBig.add(index,value);

        endTimeBig = System.nanoTime();
        this.timesInsert[1] = endTimeBig - startTimeBig;
        return this.timesInsert;
    }
    public long[] delete(Integer value) {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        //delete---------------
        this.linkedListSmall.remove(value);// check to see if its there first?--------------

        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //delete------------
        this.linkedListBig.remove(value);//--------------


        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        return this.timesDelete;
    }

    //search sort insert delete
}
