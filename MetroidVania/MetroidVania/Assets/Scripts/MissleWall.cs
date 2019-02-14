using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collider) {
		//to shoot through walls dont destroy game object?
		if (collider.gameObject.tag == "Missle" ) {//wont work for enemies to hit player
			gameObject.SetActive(false);
			//Destroy (gameObject);
		}
	}


}
