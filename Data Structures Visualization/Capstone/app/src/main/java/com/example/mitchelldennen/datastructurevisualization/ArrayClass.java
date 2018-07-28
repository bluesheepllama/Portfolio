package com.example.mitchelldennen.datastructurevisualization;

import android.util.Log;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collections;

/**
 * Created by mitchelldennen on 2/10/18.
 */

public class ArrayClass {

    private ArrayList<Integer> smallList;// = new ArrayList<>();
    private ArrayList<Integer> bigList;// = new ArrayList<>();
    private long[] timesSearch;
    private long[] timesSort;
    private long[] timesInsert;
    private long[] timesDelete;



      ArrayClass() { //

        this.smallList = new ArrayList<Integer>(100);
        this.bigList = new ArrayList<Integer>(100000);
        this.timesSort = new long[2];
        this.timesSearch = new long[2];
        this.timesInsert = new long[2];
        this.timesDelete = new long[2];
    }

    public void test() {// get rid of
        Log.d("test","test");
    }

    public void populate(ArrayList<Integer> small, ArrayList<Integer> big) {//only pass small and big into this function----------
        //should the activity call populate once or should each method call populate?---------------
        //for(int i = 0; i < 100-1; i++) {
        for(Integer i:small) {
        //Log.d("test this to be loggedd ", "" + small.get(0));//get rid of
            this.smallList.add(i);
        }
        for(int i = 0; i < 10000; i++) {
            this.bigList.add(big.get(i));
        }
    }

    public long[] search(Integer tofind) {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();
        this.smallList.contains(tofind);//will this method screw up time or should i search myself?----------------
        endTimeSmall = System.nanoTime();
        this.timesSearch[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();
        this.bigList.contains(tofind); ////////////////------------
        endTimeBig = System.nanoTime();
        this.timesSearch[1] = endTimeBig - startTimeBig;
        return this.timesSearch;
    }

    public long[] sort() {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        //this.smallList.sort();//--------------
        //shellSort(this.smallList,this.smallList.size());
        Collections.sort(this.smallList);
        endTimeSmall = System.nanoTime();
        this.timesSort[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //this.bigList.sort(); ////////////////------------
        Collections.sort(this.bigList);
        //shellSort(this.bigList,this.bigList.size());//----------------

        endTimeBig = System.nanoTime();
        this.timesSort[1] = endTimeBig - startTimeBig;
        //display array for testing purposes
        Log.d("start small","" + startTimeBig);
        Log.d("end small","" + endTimeBig);
        Log.d("final","" + this.timesSort[1]);

        return this.timesSort;
    }
    public long[] insert(Integer value) {
        long startTimeSmall;
        long startTimeBig;
        long endTimeSmall;
        long endTimeBig;
        startTimeSmall = System.nanoTime();

        //insert--------------- test this

        for(int i = this.smallList.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (this.smallList.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            this.smallList.add(i+1,this.smallList.get(i));      // shift elements forward
            if (this.smallList.get(i) <= value) {       // if we found the right spot
                this.smallList.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
        }

        endTimeSmall = System.nanoTime();
        this.timesInsert[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        //insert------------

        for(int i = this.bigList.size() - 1; i > 0; i--) {//does add overright elements when inserted?--------------
            if (this.bigList.get(i) == 0) continue; // skip last elements to avoid array index out of bound
            this.bigList.add(i+1,this.bigList.get(i));      // shift elements forward
            if (this.bigList.get(i) <= value) {       // if we found the right spot
                this.bigList.add(i,value);        // place the new element and
                break;                 // break out the loop
            }
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
        startTimeSmall = System.nanoTime();

        this.smallList.remove(value);

        endTimeSmall = System.nanoTime();
        this.timesDelete[0] = endTimeSmall - startTimeSmall;
        startTimeBig = System.nanoTime();

        this.smallList.remove(value);


        endTimeBig = System.nanoTime();
        this.timesDelete[1] = endTimeBig - startTimeBig;
        return this.timesDelete;
    }
    /* function to sort arr using shellSort */

    int shellSort(ArrayList<Integer> arr, int n) { //get rid of both variables--------------
        // Start with a big gap, then reduce the gap
        for (int gap = n/2; gap > 0; gap /= 2)
        {
            // Do a gapped insertion sort for this gap size.
            // The first gap elements a[0..gap-1] are already in gapped order
            // keep adding one more element until the entire array is
            // gap sorted
            for (int i = gap; i < n; i += 1)
            {
                // add a[i] to the elements that have been gap sorted
                // save a[i] in temp and make a hole at position i
                int temp = arr.get(i); //change to class variables--------------------------

                // shift earlier gap-sorted elements up until the correct
                // location for a[i] is found
                int j;
                for (j = i; j >= gap && arr.get(j - gap) > temp; j -= gap)
                    arr.add(j,j-gap); //arr.indexOf(j) = arr.get(j - gap);

                //  put temp (the original a[i]) in its correct location
                arr.add(j,temp);//arr.get(j) = temp;// arr.
            }
        }
        return 0;
    }


}
