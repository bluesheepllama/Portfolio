using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestantiateWallOnDeath : MonoBehaviour {

	//Doesnt work Go To KeyDestantiate
	public GameObject wallToDestantiate;
	public GameObject enemy;

	/*public void Update() {
		if (enemy) {
			if (enemy.activeInHierarchy == false) {
				Debug.Log ("deactivate boss wall");
				wallToDestantiate.SetActive (false);

			}
		}
	}*/

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			//GameObject go = GameObject.FindWithTag ("Boss1");
			//GameObject gO = GetComponent<GameObject> ();
			if (enemy) {
				//if (enemy.activeInHierarchy) {
				if (!enemy.active) {
					Debug.Log (" destantiate boss wall");
					wallToDestantiate.SetActive (false);

				}
			}
		}
	}
}
