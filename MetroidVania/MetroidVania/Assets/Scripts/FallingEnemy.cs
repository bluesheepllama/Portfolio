using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : MonoBehaviour {

	public float timeToDisappear = 2;
	public float timer;
	public void Start() {
		//timer = Time.deltaTime;
	}

	public void Update() {
		//Debug.Log (timer);
		if (timer >= timer + timeToDisappear) {
			//Debug.Log ("Disapper now");
			Destroy (gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
		timer = Time.time;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.gravityScale = 2;
		
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {

			//Debug.Log("OOn collision falling");
			Destroy (gameObject);
		}
	}
}
