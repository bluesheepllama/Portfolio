using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	public LayerMask rayCastLayerMask;
	public bool isOnGround;
	public bool isOnSlopedGround;
	public bool isOnFallingPlatform = false;
	[HideInInspector]
	public Vector2 colliderCenter;
	[HideInInspector]
	public Vector2 collisionSize;

	private PlayerController pc;
	private Jumper jumper;
	private Collision2D collision2D;
	private Rigidbody2D rb;

	public void Start() {
		pc = GetComponent<PlayerController> ();
		jumper = GetComponent<Jumper> ();
		rb = GetComponent<Rigidbody2D> ();
		BoxCollider2D myCollider = GetComponent<BoxCollider2D> ();//only work
		if (myCollider) {
			collisionSize = myCollider.size;
			colliderCenter = myCollider.offset;
		}

	}

	public float GetCollisionRadiusY()
	{
		return collisionSize.y / 3f; // 2f
	}


	public void OnCollisionStay2D(Collision2D collision) {//adds overrhead  
		collision2D = collision;



		if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "SlopedGround") {
			isOnGround = true;
			isOnFallingPlatform = false;

			//raise gravity to stop wall slowing?
		}
		if (collision.gameObject.tag == "SlopedGround") {
			isOnSlopedGround = true;
			isOnFallingPlatform = false;

		}
		if (collision.gameObject.tag == "FallingPlatform") {
			isOnFallingPlatform = true;
		}

		if (IsOnGroundRay ()) {
			isOnGround = true;
		}else {
				isOnGround = false;
			}
			
	}

	public void OnCollisionExit2D(Collision2D collision) {
		//Debug.Log ("off ground");
		isOnSlopedGround = false;
		isOnGround = false;
	}

	public bool IsOnGroundRayCast() {
		/*Rigidbody2D controlledRigidbody = GetComponent<Rigidbody2D> ();
		float directionNormal = controlledRigidbody.velocity.x / Mathf.Abs (controlledRigidbody.velocity.x);
		Vector2 rayCastOrigin = (Vector2)(-(transform.up) * directionNormal);
		float rayCastDistance =  5f;
		Debug.Log ();
		//if (transform.localScale.x < 0) //get rid of if
		//{
		Physics2D ray = Physics2D.Raycast (transform.position, Vector2.left, rayCastDistance, rayCastLayerMask);
			Debug.Log ("onground?");
		if (ray) {
			pc.inAir = false;
		} else {
			pc.inAir = true;

		}
		return ray;
			//return Physics2D.Raycast (transform.position, Vector2.left, rayCastDistance, rayCastLayerMask);
		//}
		//else 
		//{
			//pc.inAir = true;
			//return Physics2D.Raycast (transform.position, Vector2.right, rayCastDistance,rayCastLayerMask);
		//
*/

		/*RaycastHit hit = collision2D.collider;

		//BoxCollider box = hit.collider as BoxCollider;
		Collider col = hit.collider;
		if (col == null) Debug.LogWarning("Collider is not a BoxCollider!");

		Vector3 localPoint = hit.transform.InverseTransformPoint(hit.point);
		Vector3 localDir = localPoint.normalized;


		/*
		 * 
		 *  If upDot is positive, we're above the box; if negative, we're below the box.

			If fwdDot is positive, we're in front of the box; if negative, we're behind the box.

			If rightDot is positive, we're to the box's right; if negative, we're to its left.
		 * 

		float upDot = Vector3.Dot(localDir, Vector3.up);
		float fwdDot = Vector3.Dot(localDir, Vector3.forward);
		float rightDot = Vector3.Dot(localDir, Vector3.right);

		if(upDot > 0) {
			return true;
		} else {
			return false;
		}
		*/
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, rayCastLayerMask))
		{
			//jumper.doublecount = 0;
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
			Debug.Log("Did Hit");
			return true;

		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
			Debug.Log("Did not Hit");
			return false;

		}
	}
	private bool IsOnGroundRay() {
		float directionNormal = rb.velocity.x / Mathf.Abs (rb.velocity.x);
		//Vector2 rayCastOrigin = (Vector2)(transform.position + .5f * transform.right * directionNormal) + groundDetector.colliderCenter;
		Vector2 rayCastOrigin = (Vector2)(transform.right * directionNormal);// + groundDetector.colliderCenter;
		float rayCastDistance =  2f;
		if (transform.localScale.x < 0) 
		{
			return Physics2D.Raycast (transform.position, Vector2.down, rayCastDistance, rayCastLayerMask);
		}
		else 
		{
			return Physics2D.Raycast (transform.position, Vector2.down, rayCastDistance,rayCastLayerMask);
		}
	}

}
	
