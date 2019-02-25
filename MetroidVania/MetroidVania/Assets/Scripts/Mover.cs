using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float acceleration;// = 5f;
	public float maximumSpeed;// = 20f;
	public float minimumSpeed = .2f;//
	public float normalSpeed = 10;
	public bool isSlow = false;
	public bool maxSpeedChangeBool = true;
	//public bool speedBonusIsActive = false;
	public float slowCounter;
	public float slowLength = 8f;
	public float speedMultiplier = 1f;
	//public float speedBonusMultiplier = 1.5f;
	//private float speedBonusLength = 8f;
	//private float speedBonusCounter;
	private SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		normalSpeed = maximumSpeed;
	}

	public void Update() {
		if (gameObject.tag == "Enemy") {// slows only the enemy?
			if (isSlow) {
				if (spriteRenderer) {
					Color32 blueish = new Color32(0x9C, 0xCE, 0xFF, 0xFF);
					spriteRenderer.color = blueish;

				}
				slowCounter += Time.deltaTime;
				if (slowCounter >= slowLength) {
					SetNormalSpeed ();
				}
			}
		}
		} 

	public void AccelerateInDirection (Vector2 direction) {
		
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		Vector3 newVelocity = rb.velocity +( direction * acceleration * Time.deltaTime * speedMultiplier);//*speed multiplier?
		//Debug.Log ("velocity " + newVelocity);
		//if (speedBonusIsActive == false) {
			newVelocity.x = Mathf.Clamp (newVelocity.x, -maximumSpeed, maximumSpeed);//(newVelocity.x, -maximumSpeed, maximumSpeed);
		//} else {
			//newVelocity.x = Mathf.Clamp (newVelocity.x, -13f, 13f);

		//}
		rb.velocity = newVelocity;
		//Debug.Log (spBoost.isActive + " ");

	}


	private void SetNormalSpeed() {
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		maximumSpeed = normalSpeed;
		isSlow = false;
		slowCounter = 0;
		maxSpeedChangeBool = true;
		if (sr) {
			sr.color = Color.white;
		}
	}
	public bool IsWalking() {
		return Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) >= minimumSpeed;
	}


}
