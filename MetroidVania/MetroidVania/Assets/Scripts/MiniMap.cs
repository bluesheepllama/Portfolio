using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//map equal to 10* dimentions of whole level iimage
//blip size relative to player size

public class MiniMap : MonoBehaviour {

	public Transform Target;

	public float zoomLevel = 10f;

	public Vector2 TransformPosition (Vector3 position) {
		if (position != null) {
			Vector3 offset = position - Target.position;
			Vector2 newPosition = new Vector2 (offset.x, offset.y);//.z?
			newPosition *= zoomLevel;
			return newPosition;
		} else {
			return new Vector2(0,0);
		}
	}

	public Vector3 TransformRotation(Vector3 rotation) {
		return new Vector3 (0, 0, Target.eulerAngles.y - rotation.y);

}

	public Vector2 MoveInside(Vector2 point) {
		Rect mapRect = GetComponent<RectTransform> ().rect;
		point = Vector2.Max (point, mapRect.min);
		point = Vector2.Min (point, mapRect.max);
		return point;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
