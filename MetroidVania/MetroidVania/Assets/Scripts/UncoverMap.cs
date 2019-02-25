using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UncoverMap : MonoBehaviour {
	public GameObject panelToUncover;


	private void OnTriggerEnter2D(Collider2D collider) {

		if (collider.tag == "Player") {
			Debug.Log ("uncover map portion");
			panelToUncover.SetActive (false);
		}

		}


	}

