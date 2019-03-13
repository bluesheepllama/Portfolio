using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemyBullet : MonoBehaviour {

	public Mover controlledMover;
	public Transform target;
	//private Rigidbody2D rigidBody;


	void Start() {
		//isFlying = false;
		//rigidBody = GetComponent<Rigidbody2D> ();
		controlledMover = GetComponent<Mover>();
		target = GameObject.Find("Player+Camera").GetComponent<Transform>();
	}

	void Update() {
		if (target) {
			//Vector2 temp = currentMoveToPoint - (Vector2)transform.position;
			Debug.Log("target position" + target.transform.position.x + "," + target.transform.position.y);
		    Vector2 temp = new Vector2 (target.transform.position.x, target.transform.position.y);
			controlledMover.AccelerateInDirection (temp);

			//return temp;
			//SetMoveToPoint (target.position);
		}

			

			//ContinueMoving ();

			//base.Update ();
	
			
	}
	/*public override void SetMoveToPoint( Vector2 moveTo )
	{
		
		Vector2 temp = moveTo;
		temp = new Vector2 (target.transform.position.x, target.transform.position.y);
		currentMoveToPoint = temp;
		//navMeshAgent.destination = moveTo;

	}*/

	/*public override*/ void OnCollisionEnter2D(Collision2D collider) {
		
		Destroy(gameObject,2f);

	}
}
