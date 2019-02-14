using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public Transform target;
	public float damping = .005f;

	public Vector3 velocity;
	public Vector3 offset;

	// Use this for initialization
	void Start () {

		offset = transform.position - target.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			
			return;

		}
		Vector3 targetPosition = target.position + offset;
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, damping );


	}
}
