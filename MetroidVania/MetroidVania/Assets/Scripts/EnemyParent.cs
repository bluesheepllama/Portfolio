using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour {

	public float stopDistance = 3.5f;
	public Vector2 currentMoveToPoint;
	public float hitForce;
	private float hitForceGround = 100f;
	public bool isFlying = false;

	protected Mover mover;
	protected Rigidbody2D rigidBody;
	protected bool canMove = true;
	protected bool isoffset = false;
	public int flyingOffset = 10;

	public virtual void Awake()
	{
		mover = GetComponent<Mover>();
		rigidBody = GetComponent<Rigidbody2D> ();

	SetMoveToPoint(transform.position);
		

	}

	public virtual void Update()
	{
		if (IsWithinDistance (stopDistance) == false && mover && canMove) 
		{
			mover.AccelerateInDirection (GetDirection ());
		} 

	}

	public virtual void SetMoveToPoint( Vector2 moveTo )
	{
		if (isFlying == false) {
			currentMoveToPoint = moveTo;
		} else {
			Vector2 temp = moveTo;
			temp = new Vector2 (moveTo.x, moveTo.y + flyingOffset);
				currentMoveToPoint = temp;
		}
			//navMeshAgent.destination = moveTo;

	}

	public virtual bool IsWithinDistance( float distance )
	{
		bool isWithinDistance = (GetDirection ().magnitude < distance);

	//Debug.Log(isWithinDistance + " " + GetDirection ().magnitude + " " + distance);

		return isWithinDistance;
	}

	public virtual Vector2 GetDirection()
	{
	//Debug.Log (transform.position);
		//try to fix flying here~~~~~~~~~~~~~~~~~~~~
		if (isFlying == true) {
			return currentMoveToPoint - (Vector2)transform.position;
		} else {
			Vector2 temp = currentMoveToPoint - (Vector2)transform.position;
			temp = new Vector2 (temp.x, 0);
			return temp;
		}
	}
	public virtual void StopMoving() {
		canMove = false;
		//mover.maximumSpeed = 0;
		//transform.position = transform.position;
			//navMeshAgent.destination = transform.position;

	}
	public virtual void ContinueMoving() {
		//mover.maximumSpeed = mover.normalSpeed;
		canMove = true;

			//navMeshAgent.destination = currentMoveToPoint;

	}
	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Player") {//wont work for enemies to hit player
		//Debug.Log("Enemy Pareent, in collinoenter");
		Vector2 direction =  (transform.position - collider.transform.position);
			rigidBody.AddForce (direction * hitForce);
		}
		if (isFlying && collider.gameObject.tag == "Ground") {//SOMETIMES WEIRD BEHAVIOR
			Vector2 direction =  (transform.position - collider.transform.position);
			rigidBody.AddForce (direction * (hitForceGround/2));
		}
	}
}
