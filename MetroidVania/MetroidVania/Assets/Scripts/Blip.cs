using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour {

	public Transform target;
	public bool keepInBounds = true;
	public bool lockScale = false;
	public bool lockRotation = false;
	public float minimumScale = 1;

	MiniMap map;
	RectTransform myRectTransform;


	// Use this for initialization
	void Start () {
		map = GetComponentInParent<MiniMap> ();//inparent
		myRectTransform = GetComponent<RectTransform> ();
	}

	void LateUpdate() {
		if (target) {
			Vector2 newPosition = map.TransformPosition (target.position);
			if (keepInBounds) {
				newPosition = map.MoveInside (newPosition);
			}
			if (!lockScale) {
				float scale = Mathf.Max (minimumScale, map.zoomLevel);
				myRectTransform.localScale = new Vector3 (scale, scale, 1);
			}
			if (!lockRotation) {
				myRectTransform.localEulerAngles = map.TransformRotation (target.eulerAngles);
			}

			myRectTransform.localPosition = newPosition;
		}
	}
	

}
