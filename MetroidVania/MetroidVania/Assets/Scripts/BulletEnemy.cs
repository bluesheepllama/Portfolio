using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

	public float speed = 35f;
	public Rigidbody2D rb;
	public float damage = 5f;
	public GameObject impactEffect; // for bullet hit animation
	//public GameObject impactEffectPrefab;
	private PlayerController playerController;
	private float normalScaleX;
	private float normalScaleY;
	private int bounce = 0;

	private bool followPlayer = false;

	public Transform player;

	private SpriteVelocityFlipper spriteFlipper;

	void Start() {
		//sets the normal scale to instantiated scale

		impactEffect = GameObject.Find("ImpactEffect1");

		//shrinks bullets

		//if(transform.localScale.x > 0) {

		if(followPlayer == false) {
		if(spriteFlipper.flip) {//will only work on instatiated enemies because of prefab
			Debug.Log ("shootleft");

			rb.velocity = transform.right * transform.localScale.x * speed;

		} else if(!(spriteFlipper.flip)) {
			Debug.Log ("shootright");
			rb.velocity = -(transform.right) * transform.localScale.x * speed;
		}
		Debug.Log ("enemy transform" + transform.localScale.x + "flip" + spriteFlipper.flip);
		//accounts for shooting in all directions
		}
	}



	void OnTriggerEnter2D(Collider2D collider) {
		//Collision2D collision = collider;
		//to shoot through walls dont destroy game object?

		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Ground") {//wont work for enemies to hit player
			
			//Debug.Log (collider.name); // logs name of object hit

			TakeDamage (collider);//maybe extra

			//Debug.Log (impactEffect);
			if(impactEffect) {
				GameObject impact;
				//Debug.Log ("impact effect");
				impact = (GameObject) Instantiate(impactEffect,transform.position,transform.rotation);
				Destroy (impact, .1f);

			}
			Destroy (gameObject);
		}
	}

	private void TakeDamage(Collider2D collider) {
		Destructable destructable = collider.GetComponent<Destructable> ();
		if (destructable != null) {

			//if is shrunk then do less damage based on a multiplier

				destructable.TakeDamage (damage);
				Destroy (gameObject);


		}
	}
	void PassedValue (SpriteVelocityFlipper svf) {  //gets the playercontroller from weapon

		spriteFlipper = svf;
	}
	void PassedValue2 (bool follow) {  //gets the playercontroller from weapon

		followPlayer = follow;
	}
}



	/*void PassedValue (PlayerController pc) {  //gets the playercontroller from weapon

		playerController = pc;
	}*/
