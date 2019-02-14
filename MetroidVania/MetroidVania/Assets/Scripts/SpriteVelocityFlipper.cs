using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVelocityFlipper : MonoBehaviour {

	public bool defaultRightFacing = true;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D controlledrigidBody;
	private float currentScale;

	public bool flip;

	public void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		controlledrigidBody = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentScale = Mathf.Abs(transform.localScale.x);
		flip = controlledrigidBody.velocity.x < 0f;

		if (controlledrigidBody.velocity.x < 0.1f && controlledrigidBody.velocity.x > -0.1f) {
			return;
		}

		if (!defaultRightFacing) {
			flip = !flip;
		}
		//transform.Rotate(0f,180f,0f);
		if (flip) {
			Vector3 newScale = transform.localScale;
			newScale.x = -currentScale;
			transform.localScale = newScale;
		} else {
			Vector3 newScale = transform.localScale;
			newScale.x = currentScale;
			transform.localScale = newScale;
		}
		
	}
}
