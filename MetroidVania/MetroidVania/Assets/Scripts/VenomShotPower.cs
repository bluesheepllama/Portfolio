using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomShotPower : MonoBehaviour {

	public bool isTakingDamage = false;

	private Destructable enemyDestructable;
	private SpriteRenderer sr;
	private Color alpha;
	private float timer = 0;
	private float chargeTimer;
	private float timeToCharge = 2f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		if (isTakingDamage == true) {
			Debug.Log ("istakingdamage true for venom damage");
			timer += Time.deltaTime;
			if (timer > .25f && timer < .5f) {
				Debug.Log ("timer .25-.5");
				alpha.r = 58;
				alpha.g = 158;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);

				Debug.Log ("venom .24-.5");

			}
			if (timer > .5f && timer < .75f) {
				alpha.b = 0;
				alpha.g = 0;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);
				Debug.Log ("venom .5-.75");

				//Debug.Log (alpha.b);

			}
			if (timer > .75f && timer < 1f) {
				alpha.b = 58;
				alpha.g = 158;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);
				Debug.Log ("venom ..75-1");

				//Debug.Log (alpha.b);


			}
			if (timer > 1.25f && timer < 1.5f) {
				alpha.b = 0;
				alpha.g = 0;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);
				Debug.Log ("venom 1.24-1.5");

				//Debug.Log (alpha.b);


			}
			if (timer > 1.5f && timer < 1.75f) {
				alpha.b = 58;
				alpha.g = 158;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);
				Debug.Log ("venom 1.5-1.75");

				//Debug.Log (alpha.b);


			}
			if (timer > 1.75f && timer < 2) {
				alpha.b = 0;
				alpha.g = 0;
				sr.color = alpha;
				enemyDestructable.TakeDamage (.33f);
				Debug.Log ("venom 2-3");

				//Debug.Log (alpha.b);


			}

		if (timer > 2f && timer < 3f) {
				Debug.Log ("damage");
				alpha.b = 0;//change to green
				alpha.g = 0;
				sr.color = alpha;
				//Debug.Log (alpha.b);
			isTakingDamage = false;
			timer = 0;
			}

		}


	}
	public void VemonEnemy(Collider2D collider) {
		//enemyDestructable.isVenomDamage = true;
		if (collider.gameObject.tag == "Enemy") {
			Debug.Log ("in function venom enemy");
			timer = 0;
			sr = collider.GetComponent<SpriteRenderer> ();
			isTakingDamage = true;
			enemyDestructable = collider.gameObject.GetComponent<Destructable> ();// because of collision we no have enemy values ie, can do this for venom (destructable)
			if(enemyDestructable) {
				//if(enemyDestructable.isVenomDamage) {
					//for (int i = 0; i < 10; i++) {
						//Debug.Log ("Venomshot before invoke");
						//because its not in update
						//emulat webshot better
						//Invoke ("TakeDamage", .3f);
					//}//

					//enemyDestructable.hitPoints *= .95f;// speed multiplier instead
					//enemyDestructable.maxSpeedChangeBool = false;
				//}
				enemyDestructable.isVenomDamage = true;

				//Invoke ("SetToNormalSpeed", 1);
			}


		}
	}

	void TakeDamage() {
		Debug.Log ("TakeDamage VenomShotPower Damage");
		enemyDestructable.hitPoints *= .9f;// speed multiplier instead

	}
}



/*
 * public void SlowEnemy(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy") {
			 
			enemyMover = collider.gameObject.GetComponent<Mover> ();// because of collision we no have enemy values ie, can do this for venom (destructable)

			if(enemyMover) {
				if(enemyMover.maxSpeedChangeBool) {
					enemyMover.maximumSpeed *= .2f;// speed multiplier instead

					enemyMover.maxSpeedChangeBool = false;
				}
				enemyMover.isSlow = true;

				//Invoke ("SetToNormalSpeed", 1);
			}

 * */