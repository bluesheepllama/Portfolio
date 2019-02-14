using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemyController : EnemyParent {

	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	private Animator animatorController;

	public List<Vector2> patrolPoints;

	private int moveToIndex;

	//navmesh to move around walls

	public void Start() {
		animatorController = GetComponent<Animator> ();

		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);

		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
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
	}


}
