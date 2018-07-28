package com.example.mitchelldennen.datastructurevisualization;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Random;
/*
compares each data structure algorithm by passing the random lists to each respective
class methods and recieving a time of how long each algorithm takes.
*/
public class CompareActivity extends AppCompatActivity {

    //private int[] randomListBig = new int[100000];// or ArrayList<Integer> ?
    private ArrayList<Integer> randomListBig = new ArrayList<Integer>(10000);
    private int bigSize = 10000;
    //private int[] randomListSmall = new int[100];
    private ArrayList<Integer> randomListSmall = new ArrayList<Integer>(100);
    private Random ran = new Random();

    private ArrayClass array;
    private LinkedListClass linkedList;
    private StackClass stack;
    private QueueClass queue;
    private HashClass hash;
    private TreeClass tree;
    private long[][] arrayTimes = new long[4][2];//change to 4 arrays
    private long[] arraySortTimes = new long[2];
    private long[] arraySearchTimes = new long[2];
    private long[] arrayInsertTimes = new long[2];
    private long[] arrayDeleteTimes = new long[2];

    private long[][] linkedListTimes = new long[4][2];//
    private long[][] stackTimes = new long[4][2];//
    private long[][] queueTimes = new long[4][2];//
    private long[][] hashTimes = new long[4][2];//
    private long[][] treeTimes = new long[4][2];//

    private TextView arraySortTV;// = (TextView) findViewById(R.id.arraySort);//doesnt like this
    private TextView arraySearchTV;// = (TextView) findViewById(R.id.arraySearch);
    private TextView arrayInsertTV;// = (TextView) findViewById(R.id.arrayInsert);
    private TextView arrayDeleteTV;// = (TextView) findViewById(R.id.arrayDelete);

    private TextView llSortTV;// = (TextView) findViewById(R.id.llSort);
    private TextView llSearchTV;// = (TextView) findViewById(R.id.llSearch);
    private TextView llInsertTV;// = (TextView) findViewById(R.id.llInsert);
    private TextView llDeleteTV;// = (TextView) findViewById(R.id.llDelete);

    private TextView stackSortTV;// = (TextView) findViewById(R.id.stackSort);
    private TextView stackSearchTV;// = (TextView) findViewById(R.id.stackSearch);
    private TextView stackInsertTV;// = (TextView) findViewById(R.id.stackInsert);
    private TextView stackDeleteTV;// = (TextView) findViewById(R.id.stackDelete);

    private TextView queueSortTV;// = (TextView) findViewById(R.id.queueSort);
    private TextView queueSearchTV;// = (TextView) findViewById(R.id.queueSearch);
    private TextView queueInsertTV;// = (TextView) findViewById(R.id.queueInsert);
    private TextView queueDeleteTV;// = (TextView) findViewById(R.id.queueDelete);

    private TextView hashSortTV;// = (TextView) findViewById(R.id.hashSort);
    private TextView hashSearchTV;// = (TextView) findViewById(R.id.hashSearch);
    private TextView hashInsertTV;// = (TextView) findViewById(R.id.hashInsert);
    private TextView hashDeleteTV;// = (TextView) findViewById(R.id.hashDelete);

    private TextView treeSortTV;// = (TextView) findViewById(R.id.treeSort);
    private TextView treeSearchTV;// = (TextView) findViewById(R.id.treeSearch);
    private TextView treeInsertTV;// = (TextView) findViewById(R.id.treeInsert);
    private TextView treeDeleteTV;// = (TextView) findViewById(R.id.treeDelete);
/*
*/

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_compare);
        randomize();

        Integer value = ran.nextInt(100);
        //Toast.makeText(this, "This is my Toast message!" + randomListSmall.get(0) + " " + randomListSmall.get(99),Toast.LENGTH_SHORT).show();

        array = new ArrayClass();
        array.populate(randomListSmall,randomListBig);//doesnt like
        arraySortTimes = array.sort();
        arraySearchTimes = array.search(value);
        arrayInsertTimes = array.insert(value);
        arrayDeleteTimes = array.delete(value);

        linkedList = new LinkedListClass();

        linkedList.populate(randomListSmall,randomListBig);
        linkedListTimes[0] = linkedList.sort();
        linkedListTimes[1] = linkedList.search(value);
        linkedListTimes[2] = linkedList.insert(value);
        linkedListTimes[3] = linkedList.delete(value);

        stack = new StackClass();
        stack.populate(randomListSmall,randomListBig);
        stackTimes[0] = stack.sort();
        stackTimes[1] = stack.search(value);
        stack.populate(randomListSmall,randomListBig);
        stackTimes[2] = stack.insert(value);
        stackTimes[3] = stack.delete(value);

        queue = new QueueClass();
        queue.populate(randomListSmall,randomListBig);
        queueTimes[0] = queue.sort();
        queueTimes[1] = queue.search(value);
        queue.populate(randomListSmall,randomListBig);
        queueTimes[2] = queue.insert(value);
        queueTimes[3] = queue.delete(value);

        hash = new HashClass();
        hash.populate(randomListSmall,randomListBig);
        hashTimes[0] = hash.sort();
        hashTimes[1] = hash.search(value);
        hashTimes[2] = hash.insert(value);
        hashTimes[3] = hash.delete(value);

        tree = new TreeClass();
        tree.populate(randomListSmall,randomListBig);
        tree.sort();
        treeTimes[0] = tree.getTimesSort();
        tree.search(value);
        treeTimes[1] = tree.getTimesSearch();
        tree.insert(value);
        treeTimes[2] = tree.getTimesInsert();
        tree.delete(value);
        treeTimes[3] = tree.getTimesDelete();
/*
*/
        arraySortTV = (TextView) findViewById(R.id.arraySort);
        arraySortTV.setText("" + arraySortTimes[0] + " , " + arraySortTimes[1]);
        Log.d("arraytimes", "" + arraySortTimes[1]);
        arraySearchTV = (TextView) findViewById(R.id.arraySearch);
        arraySearchTV.setText("" + arraySearchTimes[0] + " , " + arraySearchTimes[1]);
        arrayInsertTV = (TextView) findViewById(R.id.arrayInsert);
        arrayInsertTV.setText("" + arrayInsertTimes[0] + " , " + arrayInsertTimes[1]);
        arrayDeleteTV = (TextView) findViewById(R.id.arrayDelete);
        arrayDeleteTV.setText("" + arrayDeleteTimes[0] + " , " + arrayDeleteTimes[1]);

        llSortTV = (TextView) findViewById(R.id.llSort);
        llSortTV.setText("" + linkedListTimes[0][0] + " , " + linkedListTimes[0][1]);
        llSearchTV = (TextView) findViewById(R.id.llSearch);
        llSearchTV.setText("" + linkedListTimes[1][0] + " , " + linkedListTimes[1][1]);
        llInsertTV = (TextView) findViewById(R.id.llInsert);
        llInsertTV.setText("" + linkedListTimes[2][0] + " , " + linkedListTimes[2][1]);
        llDeleteTV = (TextView) findViewById(R.id.llDelete);
        llDeleteTV.setText("" + linkedListTimes[3][0] + " , " + linkedListTimes[3][1]);

        stackSortTV = (TextView) findViewById(R.id.stackSort);
        stackSortTV.setText("" + stackTimes[0][0] + " , " + stackTimes[0][1]);
        stackSearchTV = (TextView) findViewById(R.id.stackSearch);
        stackSearchTV.setText("" + stackTimes[1][0] + " , " + stackTimes[1][1]);
        stackInsertTV = (TextView) findViewById(R.id.stackInsert);
        stackInsertTV.setText("" + stackTimes[2][0] + " , " + stackTimes[2][1]);
        stackDeleteTV = (TextView) findViewById(R.id.stackDelete);
        stackDeleteTV.setText("" + stackTimes[3][0] + " , " + stackTimes[3][1]);

        queueSortTV = (TextView) findViewById(R.id.queueSort);
        queueSortTV.setText("" + queueTimes[0][0] + " , " + queueTimes[0][1]);
        queueSearchTV = (TextView) findViewById(R.id.queueSearch);
        queueSearchTV.setText("" + queueTimes[1][0] + " , " + queueTimes[1][1]);
        queueInsertTV = (TextView) findViewById(R.id.queueInsert);
        queueInsertTV.setText("" + queueTimes[2][0] + " , " + queueTimes[2][1]);
        queueDeleteTV = (TextView) findViewById(R.id.queueDelete);
        queueDeleteTV.setText("" + queueTimes[3][0] + " , " + queueTimes[3][1]);

        hashSortTV = (TextView) findViewById(R.id.hashSort);
        hashSortTV.setText("" + hashTimes[0][0] + " , " + hashTimes[0][1]);
        hashSearchTV = (TextView) findViewById(R.id.hashSearch);
        hashSearchTV.setText("" + hashTimes[1][0] + " , " + hashTimes[1][1]);
        hashInsertTV = (TextView) findViewById(R.id.hashInsert);
        hashInsertTV.setText("" + hashTimes[2][0] + " , " + hashTimes[2][1]);
        hashDeleteTV = (TextView) findViewById(R.id.hashDelete);
        hashDeleteTV.setText("" + hashTimes[3][0] + " , " + hashTimes[3][1]);

        treeSortTV = (TextView) findViewById(R.id.treeSort);
        treeSortTV.setText("" + treeTimes[0][0] + " , " + treeTimes[0][1]);
        treeSearchTV = (TextView) findViewById(R.id.treeSearch);
        treeSearchTV.setText("" + treeTimes[1][0] + " , " + treeTimes[1][1]);
        treeInsertTV = (TextView) findViewById(R.id.treeInsert);
        treeInsertTV.setText("" + treeTimes[2][0] + " , " + treeTimes[2][1]);
        treeDeleteTV = (TextView) findViewById(R.id.treeDelete);
        treeDeleteTV.setText("" + treeTimes[3][0] + " , " + treeTimes[3][1]);
        /*
*/

    }

//pass random array to each class so it can do the calculations
    public void randomize() {

        for(int i = 0; i < 100; i++) {
            //randomListSmall[i] = ran.nextInt(100);
            randomListSmall.add(ran.nextInt(100));
        }
        for(int i = 0; i < bigSize; i++) {
            //randomListBig[i] = ran.nextInt(100000);
            randomListBig.add(ran.nextInt(bigSize));

        }

    }




}

