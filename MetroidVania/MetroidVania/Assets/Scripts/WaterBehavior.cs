using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour {

	private Mover playerMover;
	private Jumper playerJumper;
	private PlayerController pc;

	// Use this for initialization
	void Start () {
		playerMover = GameObject.FindGameObjectWithTag ("Player").GetComponent<Mover> ();
		playerJumper = GameObject.FindGameObjectWithTag ("Player").GetComponent<Jumper> ();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

	}
	

	private void OnTriggerEnter2D(Collider2D collider) {

		if (collider.tag == "Player") {
			PlayerEnterWater ();
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Player") {
			PlayerExitWater ();
		}

	}
		
	private void PlayerEnterWater() {
		//if(!haveWaterSuit) {
		playerMover.maximumSpeed *= .675f;
		playerJumper.jumpImpulse *= .8f;
		//}
	}

	private void PlayerExitWater() {
		//if(!haveWaterSuit) {
		playerMover.maximumSpeed /= .675f;
		playerJumper.jumpImpulse /= .8f;
		//}

	}

}
