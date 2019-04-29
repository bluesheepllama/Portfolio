using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 35f;
	public Rigidbody2D rb;
	public float damage = 1f;
	public GameObject impactEffect; // for bullet hit animation
	public GameObject impactEffect2;
	//public GameObject impactEffectPrefab;
	private PlayerController playerController;
	private float normalScaleX;
	private float normalScaleY;
	private int bounce = 0;

	private float missileDmg = 5f;
	private float GrenadeDmg = 10f;

	//public static float dmgUpgradeAmount = 1.1f;




	void Start() {
		//sets the normal scale to instantiated scale
		normalScaleX = gameObject.transform.localScale.x;
		normalScaleY = gameObject.transform.localScale.y;
		Vector2 tempTransform = new Vector2 (normalScaleX / 3, normalScaleY / 3);
		impactEffect = GameObject.Find("ImpactEffect1");
		impactEffect2 = GameObject.Find ("ImpactEffect2");
		//shrinks bullets

		damage *= playerController.bulletDmg;
		if (playerController.scatterUpgradeAmount == 2) {
			damage *= .82f; 
		} else if (playerController.scatterUpgradeAmount == 3) {
			damage *= .67f; 
		} else if (playerController.scatterUpgradeAmount == 4) {
			damage *= .55f; 
		} else if (playerController.scatterUpgradeAmount == 5) {
			damage *= .45f; 
		}

		if(playerController.isShrunk == true) {
			//Debug.Log ("shrink bullets" );
			gameObject.transform.localScale = tempTransform;
		}
		if(playerController.isShrunk == true) {
			speed = 300;
		}
		rb.velocity = transform.right * transform.localScale.x * speed;
		//accounts for shooting in all directions
		if (transform.localScale.x > 0) {
			if (Input.GetKey (KeyCode.W)) { // up
				rb.velocity = transform.up * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.E)) { //up right
				rb.velocity = (transform.up + transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.W)) { //up right
				rb.velocity = (transform.up + transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.C)) { // down right
				rb.velocity = (transform.right - transform.up) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.Q)) { // upp left
				rb.velocity = (transform.up - transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.W)) { // upp left
				rb.velocity = (transform.up - transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.Z)) { //down left
				rb.velocity = (-transform.right - transform.up) * transform.localScale.x * speed;// not sure what it is
			}
			if (Input.GetKey (KeyCode.S)) { //down
				rb.velocity = (-transform.up - transform.up) * transform.localScale.x * speed;// not sure what it is
			}

		} else if(transform.localScale.x < 0){ // facing the other way
			if (Input.GetKey (KeyCode.W)) {
				rb.velocity = -transform.up * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.E)) { // up right
				rb.velocity = -(transform.up + transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.D) && Input.GetKey (KeyCode.W)) { // up right
				rb.velocity = -(transform.up + transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.C)) { // down right
				rb.velocity = -(transform.right - transform.up) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.Q)) { // up left
				rb.velocity = -(transform.up - transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.W)) { // up left
				rb.velocity = -(transform.up - transform.right) * transform.localScale.x * speed;
			}
			if (Input.GetKey (KeyCode.Z)) { // down left
				rb.velocity = -(-transform.right - transform.up) * transform.localScale.x * speed;// not sure what it is
			}
			if (Input.GetKey (KeyCode.S)) { //down
				rb.velocity = -(-transform.up - transform.up) * transform.localScale.x * speed;// not sure what it is
			}
		}
	}



	void OnTriggerEnter2D(Collider2D collider) {
		//Collision2D collision = collider;
		//to shoot through walls dont destroy game object?
		if (playerController.weaponIndex == 6 && playerController.haveShootThroughWalls) {
			Debug.Log ("shoot through walls");
			/*if (collider.gameObject.layer == 8) {
				Physics.IgnoreCollision (GetComponent<Collider> (), GetComponent<Collider> ());// not working
			}*/


			//shooot through walls
			if (collider.gameObject.tag == "Enemy") {
				Debug.Log (collider.name); // logs name of object hit
				TakeDamage(collider);
			}

		}

		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Ground" && playerController.weaponIndex != 6 && playerController.weaponIndex != 7) {//wont work for enemies to hit player
			
			//Debug.Log (collider.name); // logs name of object hit
			WebShotPower wsp = GetComponent<WebShotPower> ();
			VenomShotPower vsp = GetComponent<VenomShotPower> ();
			if(wsp) {
				wsp.SlowEnemy (collider);
			}
			if(vsp) {
				Debug.Log ("ing bullet before venom call");
				vsp.VemonEnemy (collider);
			}
			TakeDamage (collider);//maybe extra

			//Debug.Log (impactEffect);
			if(impactEffect) {
				GameObject impact;
				Debug.Log ("impact effect");
				impact = (GameObject) Instantiate(impactEffect,transform.position,transform.rotation);
				Destroy (impact, .1f);

			}
			Destroy (gameObject);
		}
		SpiderGrenade (collider);
	}

	private void TakeDamage(Collider2D collider) {
		Destructable destructable = collider.GetComponent<Destructable> ();
		if (destructable != null) {

			//if is shrunk then do less damage based on a multiplier
			if (playerController.isShrunk == true) {
				destructable.TakeDamage (damage / 2.5f);
				Destroy (gameObject);

			} else {
				//Debug.Log("take damage in bullet");
				destructable.TakeDamage (damage);
				Destroy (gameObject);

			}
		}
	}

	private void SpiderGrenade(Collider2D collider) {
		
		if(playerController.weaponIndex == 7 && collider.gameObject.tag != "Player") {
			Destructable destructable = collider.GetComponent<Destructable> ();
			if (destructable != null || collider.gameObject.tag == "Ground") {//destroy wall condition here
				if (playerController.isShrunk == true) {
					if (destructable != null) {
						Debug.Log ("grenade shrunk");
						destructable.TakeDamage (damage / 2.5f);

					}

					Destroy (gameObject);
					GameObject impact;
					impact = (GameObject) Instantiate(impactEffect,transform.position,transform.rotation);
					Destroy (impact, .1f);


				} else {
					if (destructable != null) {
						destructable.TakeDamage (damage);
					}
					if (collider.gameObject.tag == "Enemy") {
						Debug.Log ("grenade collide with enemy");
						Destroy (gameObject);
						GameObject impact;
						impact = (GameObject) Instantiate(impactEffect2,transform.position,transform.rotation);
						Destroy (impact, .1f);
					} else {//if(!destructable){  //if( collider.gameObject.tag == "Ground"){
						Debug.Log ("grenade collide with ground");
						GameObject impact;
						Destroy (gameObject);
						impact = (GameObject) Instantiate(impactEffect2,transform.position,transform.rotation);
						Destroy (impact, .1f);
					}
				}

		}
			bounce++;//needs to be moved
	}
	}


	void PassedValue (PlayerController pc) {  //gets the playercontroller from weapon

		playerController = pc;
	}
}
/*
 *  public GameObject respawnPrefab;
    public GameObject respawn;
    void Start()
    {
        if (respawn == null)
            respawn = GameObject.FindWithTag("Respawn");

        Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
    }
 * */