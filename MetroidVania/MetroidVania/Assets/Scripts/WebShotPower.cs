using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShotPower : MonoBehaviour {
	private Mover enemyMover;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SlowEnemy(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy") {
			Debug.Log ("slow shot hits enemy");
			enemyMover = collider.gameObject.GetComponent<Mover> ();// because of collision we no have enemy values ie, can do this for venom (destructable)
			enemyMover.maxSpeedChangeBool = true;
			if(enemyMover) {
				if(enemyMover.maxSpeedChangeBool) {
					enemyMover.maximumSpeed *= .2f;// speed multiplier instead
					Debug.Log("enemy slow: " + enemyMover.maximumSpeed);
					enemyMover.maxSpeedChangeBool = false;
				}
				enemyMover.isSlow = true;

				//Invoke ("SetToNormalSpeed", 1);
			}


		}
	}
	/*
	private void SetToNormalSpeed() {
		Debug.Log ("In WebshotPower, invoke function " + enemyMover.maximumSpeed);

		enemyMover.maximumSpeed *= .8f;

	}
	*/
}
