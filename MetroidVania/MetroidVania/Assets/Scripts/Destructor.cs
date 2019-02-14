using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour {

	public float damage = 1f;
	private bool wait;

	public void Update() {
		if (wait) {
			Invoke("WaitToDamage", 1);
		}
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		// if gameobject.tag != coll

		Destructable destructable = collision.gameObject.GetComponent<Destructable> ();
		//Debug.Log ("wait" + wait );
		if (destructable && !wait && gameObject.tag != collision.gameObject.tag) {//enemies cant hurt eachother based on same tag
			destructable.TakeDamage (damage);
			wait = true;
		}
			

	}
	private void WaitToDamage() {
		wait = false;
	}

		

}
