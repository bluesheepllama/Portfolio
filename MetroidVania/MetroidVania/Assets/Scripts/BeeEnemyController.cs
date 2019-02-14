using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemyController : EnemyParent {

	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	//public Transform target;
	public GameObject target;
	public Animator animator;
	public float maxSeeDistance = 10f;
	public float offset;

	private Animator animatorController;

	public List<Vector2> patrolPoints;

	private int moveToIndex;


	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,maxSeeDistance);
	}

	public void Start() {
		isFlying = true;
		animatorController = GetComponent<Animator> ();
		//gun = player.transform.Find("Gun").gameObject;
		target = GameObject.Find ("Player");

		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		rigidBody = GetComponent<Rigidbody2D> ();

		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
	}

	public override void Update() {
		//target = gameObject.transform.Find ("Player");
		//target = gameObject.Find ("Player").gameObject;

		if (target) {

			SetMoveToPoint (target.transform.position);
		}
		if (IsWithinDistance (maxSeeDistance)) { //get rid of this to always follow
			//Debug.Log ("Scorpion following");

			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}
			patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
			patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
			patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);

			isFlying = true;
			ContinueMoving ();

			base.Update ();
		} else if (IsWithinDistance (stopDistance)) {
			//Debug.Log ("Scorpion increment patolling");

			SetMoveToPoint (patrolPoints [0]);
			if (animator) {
				//animatorController.SetBool ("IsMoving", false);
			}
			IncrementMoveToPoint ();


			//StopMoving ();

			//TODO-followingline, fixes a wierd bug

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);


		} else {
			//Debug.Log ("Scorpion  patolling");
			SetMoveToPoint (patrolPoints [1]);

			base.Update ();



		}
		//}

		//base.Update ();

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
