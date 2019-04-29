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
			//if doesnt have water suit
			PlayerEnterWater ();
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Player") {
			//if doesnt have water suit
			PlayerExitWater ();
		}

	}
		
	private void PlayerEnterWater() {
		//if(!haveWaterSuit) {
		playerMover.maximumSpeed *= .675f;
		playerJumper.jumpImpulse *= .8f;
		Debug.Log("Enter water: speed: " + playerMover.maximumSpeed);
		//}
	}

	private void PlayerExitWater() {
		//if(!haveWaterSuit) {
		playerMover.maximumSpeed /= .675f;
		playerJumper.jumpImpulse /= .8f;
		Debug.Log ("Exit water: speed: " + playerMover.maximumSpeed);

		//}

	}

}
