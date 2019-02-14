using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : MonoBehaviour {

	//get rid of jump multiplier
	public float jumpImpulse = 21f; // jump height

	public float jumpDelay = .075f;

	public float jumpMultiplier = 1f;

	private float lastTimeJumped;
	private Rigidbody2D rb;
	private GroundDetector groundDetector;
	private PlayerController playercontroller;

	public void Start() {
		groundDetector = GetComponent<GroundDetector> ();
		rb = GetComponent<Rigidbody2D> ();
		playercontroller = GetComponent<PlayerController> ();

	}

	public void Jump() {
		float timeSinceJumped = Time.time - lastTimeJumped;
		if((timeSinceJumped >= jumpDelay && IsAtRest()) ) {// add jump delay for wall jump

			rb.velocity = new Vector2 (rb.velocity.x, jumpImpulse * jumpMultiplier);//kills momentum in x
			lastTimeJumped = Time.time;

	}
}

public bool IsAtRest()
{
	if (Mathf.Abs (rb.velocity.y) < 0.25f) {
		return true;
	}

	return false;
}




}
