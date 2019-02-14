using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTouch : MonoBehaviour {

	public float timeToDrop = 3f;

	private float dropTimer;
	private bool shouldDrop;
	private float yStartPosition;

	// Use this for initialization
	void Start () {
		yStartPosition = gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -100f) {//mess with to "respawn" quicker or slower
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x,yStartPosition,gameObject.transform.position.z);
			GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
			shouldDrop = false;
			dropTimer = 0;

		}
		if (shouldDrop) {
			dropTimer += Time.deltaTime;
			if (dropTimer > timeToDrop) {
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

			}
		}
			
	}
	public void OnCollisionEnter2D(Collision2D collision) {
		//float collisionAngel = Vector3.Angle(transform.position, Collision.collider, transform.position);// Collision.collider.transform.position;
		float collisiony = collision.collider.transform.position.y - collision.collider.bounds.size.y/2;
		if(transform.position.y < collisiony) {
		shouldDrop = true;
	}
}
}
