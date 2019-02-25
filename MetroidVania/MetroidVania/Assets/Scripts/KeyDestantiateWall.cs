using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDestantiateWall : MonoBehaviour {


	private GameObject wall;

	public void DestantiateWall() {
		//Resources.FindObjectsOfTypeAll(
		//GameObject.FindGameObjectWithTag ("healthslider").GetComponent<Slider>();
		wall = GameObject.FindGameObjectWithTag ("InstantiatedPlatformKey").GetComponent<GameObject>();
		if(wall) {
			wall.SetActive (false);
		}
	}

}
