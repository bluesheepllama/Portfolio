using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour {

	// Use this for initialization
	public float maxGrappleDistance = 5f;
	public LayerMask layerMask;
	public bool grappleIsEnabled = false;
	//public AudioSource pickUpSound;
	//public AudioClip pickupClip;

	private Rigidbody2D controlledRigidBody;
	private LineRenderer lineRenderer;
	private DistanceJoint2D distanceJoint;
	private Vector3 target;
	private Transform grapplePoint;

	public void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		//Gizmos.DrawWireSphere (transform.position, maxGrappleDistance);
	}


	void Start () {

		//pickUpSound
		lineRenderer = GetComponent<LineRenderer> ();
		distanceJoint = GetComponent<DistanceJoint2D> ();
		controlledRigidBody = GetComponent<Rigidbody2D> ();
		grapplePoint = GetComponent<Transform> ();
		distanceJoint.enabled = false;
		lineRenderer.enabled = false;
		if (distanceJoint == null) {
			distanceJoint = new DistanceJoint2D ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightAlt) && grappleIsEnabled ) {
			Debug.Log("Start Grapple");
			StartGrapple ();
		}
		if(Input.GetKey(KeyCode.RightAlt)&& grappleIsEnabled) {
			ContinueGrapple ();
		}

		if (Input.GetKeyUp (KeyCode.RightAlt)&& grappleIsEnabled) {
			EndGrapple ();
			Debug.Log ("end grapple");
			grappleIsEnabled = false;
		}

		
	}
	public void StartGrapple () {
		RaycastHit2D hit = Physics2D.Raycast (grapplePoint.position,grapplePoint.up);

		if (hit) {
			Debug.Log ("grapple target" + hit.transform.name);
		}
		//Vector3 point = new Vector3 (grapplePoint.right, grapplePoint.transform.position.y, grapplePoint.transform.position.z);
		//target = Camera.main.ScreenToWorldPoint (grapplePoint.up);//(Input.mousePosition);// this is why
		target = Camera.main.ScreenToWorldPoint (hit.transform.up);//(Input.mousePosition);// this is why

		target.z = transform.position.z;
		//RaycastHit2D rayCastHit = Physics2D.Raycast (transform.position, target - transform.position, maxGrappleDistance, layerMask);
		RaycastHit2D rayCastHit = Physics2D.Raycast (transform.position, transform.up, maxGrappleDistance, layerMask); // changes grapple position

		if (rayCastHit.collider) {

			distanceJoint.enabled = true;
			distanceJoint.distance = Vector2.Distance (transform.position, rayCastHit.point);
			distanceJoint.connectedAnchor = rayCastHit.point;
			lineRenderer.enabled = true;
			lineRenderer.SetColors (Color.gray, Color.grey);
			lineRenderer.SetWidth (.2f, .2f);
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, rayCastHit.point);
			//}
		}

	}

	public void ContinueGrapple () {
		//if (isEnabled = true) {
			lineRenderer.SetPosition (0, transform.position);
		//if(Input.GetKey(KeyCode.W)) {
			//gameObject.transform.position = new Vector3(gameObject.transform.position.x+.3f,gameObject.transform.position.y+.3f,gameObject.transform.position.z);
			//find angle between mike and connectedanchor and go up that way?
		//}
			

		//}
	}

	public void EndGrapple () {
		//if (isEnabled = true) {
			distanceJoint.enabled = false;
			lineRenderer.enabled = false;
		//}
	}


	/*
 * float directionNormal = controlledRigidbody.velocity.x / Mathf.Abs (controlledRigidbody.velocity.x);
		//Vector2 rayCastOrigin = (Vector2)(transform.position + .5f * transform.right * directionNormal) + groundDetector.colliderCenter;
		Vector2 rayCastOrigin = (Vector2)(transform.right * directionNormal);// + groundDetector.colliderCenter;
		float rayCastDistance =  5f;
		if (transform.localScale.x < 0) 
		{
			return Physics2D.Raycast (transform.position, Vector2.left, rayCastDistance, rayCastLayerMask);
		}
			else 
		{
			return Physics2D.Raycast (transform.position, Vector2.right, rayCastDistance,rayCastLayerMask);
			}
 */
}
