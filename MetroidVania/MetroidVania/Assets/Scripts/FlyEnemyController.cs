using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyController : EnemyParent {


	public Mover controlledMover;
	public Transform target;
	public Animator animator;
	public float maxSeeDistance = 10f;
	public float count;
	public float offset;

	private Animator animatorController;
	private Rigidbody2D rigidBody;

	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,maxSeeDistance);
	}

	void Start() {
		isFlying = true;
		animatorController = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		//target = player.transform;
	}

	public override void Update() {
		if (target) {

			SetMoveToPoint (target.position);
		}
		if (IsWithinDistance (maxSeeDistance)) //get rid of this to always follow
		{
			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}

			ContinueMoving ();

			base.Update ();
		} 
		else 
		{
			if (animator) 
			{
				//animatorController.SetBool ("IsMoving", false);
			}

			StopMoving ();

			//TODO-followingline, fixes a wierd bug

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);


			}
	}

	/*
	void Update () {

		if(gameObject.tag == "Player")

		count = Time.time + offset;
		if (count % 3 <= 1.3) {
			controlledMover.AccelerateInDirection (new Vector2 (0f, 1f));
			//Debug.Log ("EnemyFlyer up");

		}

		if (count % 3 > 1.7) {
			controlledMover.AccelerateInDirection (new Vector2(0f,-1.3f));
			//Debug.Log ("EnemyFlyer down");
		}


	}
	*/
}
