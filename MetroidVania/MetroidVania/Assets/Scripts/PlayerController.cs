using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public LayerMask rayCastLayerMask;
	public UIController uiController;

	private Mover controlledMover;
	private Jumper controlledJumper;
	private Animator controlledAnimator;
	private Rigidbody2D controlledRigidbody;
	private GroundDetector controlledGround;
	private Weapon controlledWeapon;
	private GroundDetector groundDetector;
	private GrappleHook grappleHook;
	public float normalGravity;
	//public float fallingGravity = 10f;

	//booleans for powers
	public bool haveMissle = false; //change to false
	public bool haveWebShot = true;
	public bool haveScatterShot = true;
	public bool haveWallClimb = true;
	public bool haveVenomShot = true;
	public bool haveGrapple = true;
	public bool haveShootThroughWalls = true;
	public bool haveGrenade;
	[HideInInspector]
	public bool isPaused = false;

	public bool haveDoubleJump = true;

	private float normalDrag = 0.2f;
	private float stopDrag = 20f;
	private Vector2 rbStopVel;
	private float temptimer = 0f;
	private float temptimer2 = 0f;
		


	public int maxMissileCount = 5;
	public int missileCount = 5;
	public float currentScale;
	public int weaponIndex;
	public float fireTimer;


	public void Awake() {
		controlledMover = GetComponent<Mover> ();
		controlledJumper = GetComponent<Jumper> ();
		controlledAnimator = GetComponent<Animator> ();
		controlledRigidbody = GetComponent<Rigidbody2D> ();
		controlledGround = GetComponent<GroundDetector> ();
		controlledWeapon = GetComponent<Weapon> ();
		groundDetector = GetComponent<GroundDetector> ();
		grappleHook = GetComponent<GrappleHook> ();
		shrinkEnabled = false;
		normalGravity = controlledRigidbody.gravityScale;
		rbStopVel = new Vector2 (0f, controlledRigidbody.velocity.y);//
	}



	// Update is called once per frame
	void Update () {
		controlledAnimator.SetBool ("IsWalking", false);
		controlledAnimator.SetBool ("IsIdle", true);
		controlledAnimator.SetFloat ("YVelocity", controlledRigidbody.velocity.y);

		currentScale = Mathf.Abs(transform.localScale.x);
		fireTimer += Time.deltaTime;
		/*if (!controlledAnimator ) {
			controlledAnimator.SetBool ("Walking", false);
			controlledAnimator.SetFloat ("YVelocity", controlledRigidbody.velocity.y);
			controlledAnimator.SetBool ("IsOnGround", controlledGround.isOnGround);
		}*/

		//adds gravity when falling
		if (controlledRigidbody.velocity.y < 0f) {
			//controlledRigidbody.gravityScale = fallingGravity;

		} else {
			controlledRigidbody.gravityScale = normalGravity;
		}

		//move right
		//Debug.Log((string)Input.inputString);
		//detectPressedKeyOrButton ();

		if (Input.GetKey (KeyCode.D)) {// && Input.anyKeyDown == false) {
			controlledAnimator.SetBool ("IsWalking", true);
			if (controlledRigidbody.velocity.y < 0f || controlledRigidbody.velocity.y > 0f) {//				controlledAnimator.SetFloat ("YVelocity", controlledRigidbody.velocity.y);
				controlledAnimator.SetBool ("IsWalking", false);

			}

			Vector3 newScale = transform.localScale;
			newScale.x = currentScale;
			transform.localScale = newScale;
			
			controlledRigidbody.drag = normalDrag;// here or bottom of function?

			/*if (!controlledAnimator) {
				controlledAnimator.SetBool ("Walking", true);
			}*/
			//if still moving left then turn friction way up
				if (controlledRigidbody.velocity.x < 0f && groundDetector.isOnGround) {// not acceleration, not position, velocity?
					//Debug.Log ("inrcrease drag right");
					//controlledRigidbody.drag = stopDrag;
					controlledRigidbody.velocity = rbStopVel;

				}
				if (groundDetector.isOnSlopedGround == true) { //walks up slopes
					controlledMover.AccelerateInDirection (new Vector2 (1f, 1f));

				} else {
					controlledMover.AccelerateInDirection (new Vector2 (1f, 0f));
			}
		}
		if (Input.GetKeyUp (KeyCode.D)) { // maybe ||
			if (groundDetector.isOnGround && !CanWallClimb()) {// && controlledGround.IsOnGroundRayCast()) {
				//stopDrag = 100;
				controlledRigidbody.velocity = rbStopVel;

				//controlledRigidbody.drag = stopDrag;
				Debug.Log ("getkeyup D");
			}
		}
		//move left

		if(Input.GetKey( KeyCode.A)) {
			controlledAnimator.SetBool ("IsWalking", true);
			if (controlledRigidbody.velocity.y < 0f || controlledRigidbody.velocity.y > 0f) {//
				controlledAnimator.SetFloat ("YVelocity", controlledRigidbody.velocity.y);
				controlledAnimator.SetBool ("IsWalking", false);

			}

			Vector3 newScale = transform.localScale;
			newScale.x = -currentScale;// set current scale outside in update instead 
			transform.localScale = newScale;
			controlledRigidbody.drag = normalDrag;
			/*if (!controlledAnimator) {
				controlledAnimator.SetBool ("Walking", true);
			}*/
			if (controlledRigidbody.velocity.x > 0f && groundDetector.isOnGround) {// not acceleration, not position, velocity?
				//Debug.Log ("inrcrease drag left");
				//controlledRigidbody.drag = stopDrag;
				controlledRigidbody.velocity = rbStopVel;

			}
			if (groundDetector.isOnSlopedGround == true) { //walks up slopes
				controlledMover.AccelerateInDirection (new Vector2 (-1f, 1f));

			} else {
				controlledMover.AccelerateInDirection (new Vector2 (-1f, 0f));
			}
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			
			if (groundDetector.isOnGround && !CanWallClimb()) {// && controlledGround.IsOnGroundRayCast()) {
				//stopDrag = 100;
				controlledRigidbody.velocity = rbStopVel;

				//controlledRigidbody.drag = stopDrag;
				controlledRigidbody.velocity = rbStopVel;

				Debug.Log ("getkeyup A");

			}

		}

		//wall cilimb
		if(Input.GetKey(KeyCode.W) && haveWallClimb) {
			
		//controlledRigidbody.drag = normalDrag;
		/*if (!controlledAnimator) {
				controlledAnimator.SetBool ("Walking", true);
			}*/
		if (controlledRigidbody.velocity.x > 0f) {// not acceleration, not position, velocity?
			//Debug.Log ("inrcrease drag left");
				//controlledRigidbody.drag = stopDrag;
		}
			if (CanWallClimb() == true) { //walks up slopes
				controlledRigidbody.drag = normalDrag;
				controlledRigidbody.gravityScale = 0;
				controlledMover.AccelerateInDirection (new Vector2 (1f * transform.localScale.x, 1f));
		}
	}
		//running

		if (Input.GetKeyDown (KeyCode.Tab)) {
			controlledMover.maximumSpeed = 30f;
		}


		//jumping

		if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Space)) { 
			controlledRigidbody.drag = normalDrag;
			controlledJumper.Jump ();
			Debug.Log ("after jump function");
			if (controlledJumper.doublecount > 1) {
				controlledJumper.doublecount = 0;
			}
			/*if (groundDetector.isOnGround) {// && controlledGround.IsOnGroundRayCast()) {
				stopDrag = 100;
				controlledRigidbody.drag = stopDrag;
			}*/

				
		}
		/*if (Input.GetKey (KeyCode.Space)) { 
			if (groundDetector.isOnGround && controlledGround.IsOnGroundRayCast ()) {
				stopDrag = 100;
				controlledRigidbody.drag = stopDrag;
			} else {
				controlledRigidbody.drag = normalDrag;

			}

		}*/
			


		//shrink
		if(Input.GetKeyDown(KeyCode.X)) {
			if (shrinkEnabled) {
				Shrink ();
			}
		}


		//shift for missiles, back to shot

		//Change weapons
		Color alpha100 = new Color(255,255,255);
		Color alpha20 = new Color(255,255,255);
		alpha100.a = 1f;
		alpha20.a = .2f;
		if (Input.GetKeyDown(KeyCode.Alpha1)) {//change to 1-9
			controlledWeapon.WeaponCheck (0);
		}

		//changing to and from missile
		if (Input.GetKeyDown(KeyCode.Alpha2)) {//change to 1-9
			if (haveMissle && missileCount > 0) {
				
				//if(controlledWeapon.currentWeaponIndex != 1)
					controlledWeapon.WeaponCheck (1);
			}
		}
		if (controlledWeapon.currentWeaponIndex == 1)
			uiController.missileSprite.color = alpha100;
		else 
			uiController.missileSprite.color = alpha20;
		
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			if (haveMissle && missileCount > 0) {
				
				if (controlledWeapon.currentWeaponIndex == 1)
					controlledWeapon.WeaponCheck (0);
				else
					controlledWeapon.WeaponCheck (1);
			}
		}
	
		//changing to webshot
		if (Input.GetKeyDown(KeyCode.Alpha3)) {//change to 1-9
			if (haveWebShot) {
				
				controlledWeapon.WeaponCheck (2);

			}
		}
		//changing to scatter shot
		if (Input.GetKeyDown(KeyCode.Alpha4)) {//change to 1-9
			if (haveScatterShot) {
				
				controlledWeapon.WeaponCheck (3);
			}
		}
		//change to venom shot
		if (Input.GetKeyDown(KeyCode.Alpha5)) {//change to 1-9
			if (haveVenomShot) {
				
				controlledWeapon.WeaponCheck (4);
			}
		}
		//change to grapple , venom is in slot tho??
		if (Input.GetKeyDown (KeyCode.Alpha6)) {//change to 1-9
			if (haveGrapple) {
				controlledWeapon.WeaponCheck (5);
			}
		}
		//change to shoot though walls
		if (Input.GetKeyDown (KeyCode.Alpha7)) {//change to 1-9
			if (haveGrapple) {
				controlledWeapon.WeaponCheck (6);
			}
		}
		//change to spider grenade
		if (Input.GetKeyDown (KeyCode.Alpha8)) {//change to 1-9
			if (haveGrapple) {
				controlledWeapon.WeaponCheck (7);
			}
		}
		weaponIndex = controlledWeapon.currentWeaponIndex;


		//to fix moving while a key is pressed
		if (Input.anyKey == true && !(Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.A))) {


			//controlledRigidbody.velocity.x = 0;
		}


		//~FIXES A BUG
		if (Input.anyKey == true && !(Input.GetKey (KeyCode.D)) && !(Input.GetKey (KeyCode.A)) && !(Input.GetKey (KeyCode.W))&& !(Input.GetKey (KeyCode.Space)) && !(Input.GetKey (KeyCode.M))) {
			if (groundDetector.isOnGround && !(CanWallClimb ())) { //!(controlledGround.IsOnGroundRayCast())) {
				temptimer += Time.deltaTime;
				Debug.Log (temptimer);
				if (temptimer > .01f) {
					controlledRigidbody.velocity = rbStopVel;
					temptimer = 0;
				}
				Debug.Log ("anykey == true");
				if (CanWallClimb())
					controlledRigidbody.drag = 0;
				Debug.Log ("anykey == true, apply normal drag");
	
			}
		}
		/*
		if(Input.GetKey (KeyCode.Space) || (Input.GetKey (KeyCode.M))) {
			temptimer2 += Time.deltaTime;
			if (temptimer2 > .05f) {
				Debug.Log ("trying to prevent slide jump");
				controlledRigidbody.velocity = rbStopVel;
				temptimer2 = 0;
			}

		}
		*/
		//~

		if (Input.anyKey == false) {
			//Debug.Log ("no key pressed, : velocity y: " + controlledRigidbody.velocity.y);
			controlledAnimator.SetBool ("IsWalking", false);
			controlledAnimator.SetBool ("IsIdle", true);

			controlledMover.AccelerateInDirection (new Vector2(0f,0f));
			if (controlledGround.isOnGround == true && (controlledRigidbody.velocity.y > -.25)) {//fix
				//Debug.Log ("apply stop drag");

				controlledRigidbody.velocity = rbStopVel;//stopdrag
			} else {
				controlledRigidbody.drag = normalDrag;

			}
			controlledRigidbody.gravityScale = 4.4f;
			if (groundDetector.isOnSlopedGround == true ) { //walks up slopes
				Debug.Log("stopdrag applied");
				//controlledRigidbody.drag = 100f;
				controlledRigidbody.velocity = rbStopVel;

			}
				//controlledRigidbody.gravityScale = 4.4f;
			
			if (CanWallClimb () == false) {
				controlledRigidbody.gravityScale = 4.4f;
			}

			controlledMover.maximumSpeed = 20f;


		}

		//fixes bug
		/*
		if (Input.GetKey (KeyCode.E) || Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.Z)
			|| Input.GetKey (KeyCode.C) || Input.GetKey (KeyCode.S)) {
			controlledRigidbody.drag = 100f;

		}
		*/
	}
	/*
	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "DestantiateWall") {
			uiController.DestantiatWallMsg ();
		}
	}
	*/

	// each power gets a seperate class?

	public bool shrinkEnabled = false;
	public bool isShrunk = false;
	public float shrinkValue = .5f;
	public void Shrink() {
		//if(shrinkEnabled) {
	if (isShrunk == false) {
			Vector2 temp;
				//takes more damage
			if(transform.localScale.x <0 ) {
				//shrinkValue = -.5f;
				temp = new Vector2 (-.5f, .5f);
			} else {
				//shrinkValue = .5f;
				temp = new Vector2 (.5f, .5f);

			}
			//Vector2 temp = new Vector2 (shrinkValue, shrinkValue);
			controlledRigidbody.gravityScale = 2.2f; //use a defined value
			controlledJumper.jumpImpulse -= 6;
			transform.localScale = temp;
			isShrunk = true;
		} else {
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			Vector2 temp;
			if(transform.localScale.x < 0 ) {
			 	temp = new Vector2 (-1.0f, 1.0f);
				Debug.Log ("unshrink negative");
			} else {
				 temp = new Vector2 (1.0f, 1.0f);
				Debug.Log ("unshrink positive");

			}
				//change to different function where 'w' makes bigger
			//temp = new Vector2 (1.0f, 1.0f);
			transform.localScale = temp;
			isShrunk = false;
			controlledRigidbody.gravityScale = 4.4f;
			controlledJumper.jumpImpulse += 6;


			}
		}
	//}//

	private bool CanWallClimb() {//change to ray
		float directionNormal = controlledRigidbody.velocity.x / Mathf.Abs (controlledRigidbody.velocity.x);
		//Vector2 rayCastOrigin = (Vector2)(transform.position + .5f * transform.right * directionNormal) + groundDetector.colliderCenter;
		Vector2 rayCastOrigin = (Vector2)(transform.right * directionNormal);// + groundDetector.colliderCenter;
		float rayCastDistance =  5f;
		if (transform.localScale.x < 0) 
		{
			return Physics2D.Raycast (transform.position, Vector2.left, rayCastDistance, rayCastLayerMask);
		}
			else 
		{
			return Physics2D.Raycast (transform.position, Vector2.right, rayCastDistance,rayCastLayerMask);
			}
	}




	//get rig of~~~~~~~~~~~~~
	public void detectPressedKeyOrButton()
	{
		foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKeyDown (kcode))
				Debug.Log ("KeyCode down: " + kcode);
		}
	}

	/*
	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (controlledRigidbody) {
			float directionNormal = controlledRigidbody.velocity.x / Mathf.Abs (controlledRigidbody.velocity.x);
			//Vector2 rayCastOrigin = (Vector2)(transform.position + .5f * transform.right * directionNormal) + groundDetector.colliderCenter;
			if (transform.lossyScale.x > 0) {
				Gizmos.DrawRay(transform.position, 
					(Vector2)(Vector2.right * 5f));
			} else { 
				Gizmos.DrawRay(transform.position, 
					(Vector2)(Vector2.left * 5f));
			}
		}

	}
	*/

}
