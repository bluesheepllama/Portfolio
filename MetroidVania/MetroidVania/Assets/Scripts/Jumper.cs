using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {

	//get rid of jump multiplier
	public float jumpImpulse = 21f; // jump height

	public float jumpDelay = .075f;

	public float jumpMultiplier = 1f;
	public List<AudioClip> jumpSounds;

	private float lastTimeJumped;


	private float jumpBonusMultiplier = 2.5f;

	private float jumpBonusLength = 2f;

	private float jumpBonusCounter;

	public float jumpUpgradeAmount = 8;
	//private bool jumpBonusIsActive;

	private Rigidbody2D rb;
	private GroundDetector groundDetector;
	private AudioRandomizer audioRandomizer;
	private PlayerController playercontroller;

	public void Start() {
		groundDetector = GetComponent<GroundDetector> ();
		audioRandomizer = GetComponent<AudioRandomizer> ();
		rb = GetComponent<Rigidbody2D> ();
		playercontroller = GetComponent<PlayerController> ();

	}

	public void Update() {
		/*if (jumpBonusIsActive) {
			jumpBonusCounter += Time.deltaTime;
			if (jumpBonusCounter >= jumpBonusLength) {
				EndBonusMultiplier ();
			}
		} */
	}

	public int doublecount = 0;
	public void Jump() {
		float timeSinceJumped = Time.time - lastTimeJumped;

		Debug.Log ("Jump attepted" + groundDetector.isOnGround);

		if(((timeSinceJumped >= jumpDelay && IsAtRest())   && groundDetector.isOnGround )|| doublecount == 1) {// add jump delay for wall jump
			Debug.Log ("Jump executed");

			//Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			rb.velocity = new Vector2 (rb.velocity.x, jumpImpulse * jumpMultiplier);//kills momentum in x
			lastTimeJumped = Time.time;

			/*if (doublecount >= 1) {
				doublecount = 0;
			}*/
			/*if(doublecount == 1) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpImpulse * jumpMultiplier);//kills momentum in x
				doublecount = 0;//only when touched the ground
			}*/
			if (audioRandomizer) {
				audioRandomizer.playRandomSound (jumpSounds);
			}
			if (playercontroller != null) {
				if (playercontroller.haveDoubleJump == true) {
					doublecount++;
				}
			}

		}
	}

	public bool IsAtRest()
	{
		if (Mathf.Abs (rb.velocity.y) < 0.25f) {
			doublecount = 0;
			return true;
		}

		return false;
	}




}
