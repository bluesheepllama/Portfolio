using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBugEnemyController : EnemyParent {
	//public float timeToDisappear = 2;

	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	public Transform target;
	public Animator animator;
	public float maxSeeDistance = 40f;
	public float offset;

	private Animator animatorController;

	public List<Vector2> patrolPoints;

	private int moveToIndex;
	public float timer;

	private Transform ladyTransform;

	public void Start() {
		animatorController = GetComponent<Animator> ();
		controlledMover = GetComponent<Mover> ();
		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		rigidBody = GetComponent<Rigidbody2D> ();
		ladyTransform = GetComponent<Transform> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
	}
	public override void Update() {

		if (target) {

			SetMoveToPoint (target.position);
		}
		if (IsWithinDistance (maxSeeDistance)) { //get rid of this to always follow
			//Debug.Log ("Scorpion following");
			//controlledMover.maximumSpeed = 25;

			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}
			patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
			patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
			patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);


			ContinueMoving ();

			base.Update ();
		} else if (IsWithinDistance (stopDistance)) {
			//Debug.Log ("Scorpion increment patolling");
			//controlledMover.maximumSpeed = 10;

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


	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			//Vector3 temp = new Vector3 (0f, 0f, 0f);
			Quaternion temp = new Quaternion(0f,0f,0f,0f);
			ladyTransform.rotation = temp;
			timer = Time.time;
			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			rb.gravityScale = 3f;

		}
	}
	/*private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {

			//Debug.Log("OOn collision falling");
			//Destroy (gameObject);
		}
	}*/
}

