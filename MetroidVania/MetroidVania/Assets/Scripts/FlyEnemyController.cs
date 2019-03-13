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
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

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
	//teleports enemies could be useful
	/*void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Ground") {//wont work for enemies to hit player
			//Debug.Log("Enemy Pareent, in collinoenter");
			Vector2 direction =   ( transform.position + collider.transform.position);
			rigidBody.AddForce (direction * 500);
		}
	}*/
}
