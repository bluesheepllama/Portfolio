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
	private float timer = 0f;
	private bool changeDamp = false;
	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		camFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollower> ();

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			cam.orthographicSize = cameraNewSize;
			timer = 0;
			//camFollow.damping = newDamp;
			if (newDamp == 0) {
				changeDamp = true;
				//ChangeDamp ();
			} else {
				camFollow.damping = newDamp;

			}

		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			cam.orthographicSize = cameraNewSize;
		}

	}

	private void ChangeDamp() {
		/*Debug.Log ("Changing damppening to 0;");
		for(float i = .5f; i > .01; i -= 0.001f) {
			Debug.Log (i);
			camFollow.damping = i;

		}*/
		if (timer > 0.3f && timer < .6f) {
			camFollow.damping = .4f;
		} else if (timer > .6f && timer < .9f) {
			camFollow.damping = .3f;
		} else if (timer > .9f && timer < 1.2f) {
			camFollow.damping = .2f;
		} else if (timer > 1.2f && timer < 1.5f) {
			camFollow.damping = .1f;
		} else if (timer > 1.8f && timer < 2f) {
			camFollow.damping = 0f;
		}
	}

	// Update is called once per frame
	void Update () {
		if(changeDamp == true) {
		timer += Time.deltaTime;
		if (timer > 0.3f && timer < .6f) {
			camFollow.damping = .4f;
		} else if (timer > .6f && timer < .9f) {
			camFollow.damping = .3f;
		} else if (timer > .9f && timer < 1.2f) {
			camFollow.damping = .2f;
		} else if (timer > 1.2f && timer < 1.5f) {
			camFollow.damping = .1f;
		} else if (timer > 1.8f && timer < 2.2f) {
			camFollow.damping = .02f;
		} else if (timer > 2.2f && timer < 2.5f) {
			camFollow.damping = 0f;
			changeDamp = false;
			timer = 0;
			}
		}
	}

}
