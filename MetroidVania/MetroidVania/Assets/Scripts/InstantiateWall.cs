using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWall : MonoBehaviour {

	public GameObject wallToInstantiate;



	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			wallToInstantiate.SetActive (true);
		}
	}

}
