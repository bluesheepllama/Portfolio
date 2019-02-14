using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {


	public bool isOnGround;
	public bool isOnSlopedGround;
	[HideInInspector]
	public Vector2 colliderCenter;
	[HideInInspector]
	public Vector2 collisionSize;


	public void Start() {
		
		BoxCollider2D myCollider = GetComponent<BoxCollider2D> ();//only work
		if (myCollider) {
			collisionSize = myCollider.size;
			colliderCenter = myCollider.offset;
		}

	}

	public float GetCollisionRadiusY()
	{
		return collisionSize.y / 3f; // 2f
	}


	public void OnCollisionStay2D(Collision2D collision) {//adds overrhead  
		
			
		if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "SlopedGround") {
			isOnGround = true;
			//raise gravity to stop wall slowing?
		}
		if (collision.gameObject.tag == "SlopedGround") {
			isOnSlopedGround = true;
		}
	}

	public void OnCollisionExit2D(Collision2D collision) {
		//Debug.Log ("off ground");
		isOnSlopedGround = false;
		isOnGround = false;
	}
}
