using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Transform[] waypoints;
	//public Vector2[] waypointPositions;
	public float speed = 2;
	public float moveOffsetx = 0;
	public float moveOffsety = 0;


	public int CurrentPoint = 0;


	public void Awake(){
		//waypoints[0].position = waypoitPositions [0];
		//waypoints[1].position = waypoitPositions [1];
		//waypoints[2].position = waypoitPositions [2];
		//waypointPositions[0] = new Vector2 (transform.position.x + moveOffsetx ,transform.position.y + moveOffsety);
		//waypointPositions[1] = new Vector2 (transform.position.x - moveOffsetx ,transform.position.y - moveOffsety);

		//waypoints [0].transform = waypointPositions [0]; 
		//waypoints[1].transform.position = waypointPositions [1];

		//for loop instead to loop through length
		/*for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i].transform.position = waypointPositions [i];
		}*/

	}

	void Update () 
	{
		if(transform.position.y != waypoints[CurrentPoint].transform.position.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
		}

		if(transform.position.y == waypoints[CurrentPoint].transform.position.y)
		{
			CurrentPoint +=1;
		}
		if( CurrentPoint >= waypoints.Length)
		{
			CurrentPoint = 0; 
		}
	}

	/*public Mover controlledMover;
	public float patrolOffsetx = 20f;
	public float patrolOffsety = 20f;


	public List<Vector2> patrolPoints;

	private int moveToIndex;

	//navmesh to move around walls

	public void Start() {
		controlledMover = GetComponent<Mover> ();
		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x - patrolOffsetx,transform.position.y-patrolOffsety);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffsetx,transform.position.y + patrolOffsety);

	}

	public override void Update() {
		if (IsWithinDistance (stopDistance)) {
			IncrementMoveToPoint ();
		}
		base.Update ();

	}

	private void IncrementMoveToPoint() {

		if (patrolPoints.Count == 0) {
			return;
		}

		moveToIndex++;

		if (moveToIndex >= patrolPoints.Count) {
			moveToIndex = 0;
		}
		SetMoveToPoint (patrolPoints [moveToIndex]);
	}*/


}
