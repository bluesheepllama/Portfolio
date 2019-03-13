using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyCameraSize : MonoBehaviour {

	private Camera cam;
	private CameraFollower camFollow;
	public float cameraNewSize;
	private float cameraDefaultSize = 27.94702f;
	public float newDamp;
	private float defaultDamp = 0.5f;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		camFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollower> ();

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			cam.orthographicSize = cameraNewSize;
			camFollow.damping = newDamp;

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
