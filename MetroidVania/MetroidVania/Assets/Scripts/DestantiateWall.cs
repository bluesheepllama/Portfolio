using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestantiateWall : MonoBehaviour {

	public GameObject wallToInstantiate;
	public UIController uiController;



	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			PlayerController playerController = collider.GetComponent<PlayerController> ();
			if (playerController) {
				
				//set so if it has a certain power than it sets active to false
				//add a private int to count powers aquired
				//if counter int >= another public in set on object, then it sets to false
				wallToInstantiate.SetActive (false);
				uiController.DestantiatWallMsg ();

			}
		}
	}

}
