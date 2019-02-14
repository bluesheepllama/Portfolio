using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour {

	public int heal = 1;

	public void OnCollisionEnter2D(Collision2D collision) {

		Destructable destructable = collision.gameObject.GetComponent<Destructable> ();

		if (destructable) {
			destructable.Heal (heal);
			//Debug.Log ("in healer" + heal);
			Destroy(gameObject);
		}

	}
}
