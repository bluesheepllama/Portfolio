using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoEnemyController : EnemyParent {


	private Mover controlledMover;
	protected Transform target;
	private Animator animator;
	public float maxSeeDistance = 33f;
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
		controlledMover = GetComponent<Mover> ();

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
			controlledMover.maximumSpeed = 30;
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

}