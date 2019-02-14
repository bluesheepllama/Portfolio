using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInstantiateEnemyTrigger : MonoBehaviour {

	public GameObject instantiatEnemiesTrigger;

	private float timer;
	
	// Update is called once per frame
	void Update () {
		if (timer % 300 == 10) {
			instantiatEnemiesTrigger.SetActive (false);

		}

	}
	private void OnTriggerEnter2D(Collider2D collider) {


		if (collider.tag == "Player") {
			timer = Time.time;
			instantiatEnemiesTrigger.SetActive (true);
		}
	}
}
