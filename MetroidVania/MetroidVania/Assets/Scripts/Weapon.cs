using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public Transform firePoint; //356 to hook up
	public List<GameObject> bulletPrefab;//make a list for more bullet types
	private PlayerController pc;
	private GrappleHook grappleHook;
	public int currentWeaponIndex = 0;//chang to private once works, change to zero if decide not to use the number keys
	//public GameObject bulletPrefab;
	public bool isShrunk = false;


	public SpriteRenderer chargingSprite;
	//public Transform firePointObject;

	private Color alpha;
	private Rigidbody2D rb;

	float chargeDamage = 0;
	float chargeRate = 1;
	bool charging = false;

	private float fireRate = .25f;

	private float fireTimer;
	private float timer;
	private float chargeTimer;
	private float timeToCharge = 2f;
	private float chargeShotTimer;
	private bool charge;
	void Awake() {
		//bulletPrefab = new List<GameObject> ();
		pc = GetComponent<PlayerController> ();
		grappleHook = GetComponent<GrappleHook> ();
		alpha = chargingSprite.GetComponent<SpriteRenderer>().color;
		alpha.a = 0f;
		chargingSprite .GetComponent<SpriteRenderer>().color = alpha;
		rb = GetComponent<Rigidbody2D> ();
	}

	/*
	 * Color tmp = Thugger.GetComponent<SpriteRenderer>().color;
 tmp.a = 0f;
 Thugger.GetComponent<SpriteRenderer>().color = tmp;
	 * 
	 * */

	void Update() {
		//fireTimer += Time.deltaTime;//~~~~~~~~~~~~~

		if ((Input.GetKeyDown (KeyCode.RightAlt) || Input.GetKeyDown (KeyCode.Slash) )&& pc.isPaused == false) {//getkey for charge
			charging = true;
			timer = 0;
			if(pc.fireTimer >= fireRate) {
				Shoot ();
				pc.fireTimer = 0;
			}
			alpha.a = 255f;
			chargingSprite .GetComponent<SpriteRenderer>().color = alpha;

		}

		if (charging == true && pc.haveChargeShot && pc.weaponIndex != 5) {
			Transform temp;
			//chargingSprite.color.
			timer += Time.deltaTime;
			Transform tempScale = firePoint;
			if (charging == true) {
				if (timer > .25f && timer < .5f) {
					//rb.drag = 4.4f;

					//Debug.Log ("timer .25-.5");
					alpha.b = 213;
					alpha.g = 213;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.1f, .1f, .1f);
					firePoint = tempScale;

				}
				if (timer > .5f && timer < .75f) {
					//rb.drag = 4.4f;

					alpha.b = 171;
					alpha.g = 171;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.17f, .17f, .17f);
					firePoint = tempScale;

				}
				if (timer > .75f && timer < 1f) {
					//rb.drag = 4.4f;

					alpha.b = 129;
					alpha.g = 129;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.24f, .24f, .24f);
					firePoint = tempScale;


				}
				if (timer > 1.25f && timer < 1.5f) {
					//rb.drag = 4.4f;

					alpha.b = 87;
					alpha.g = 87;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.32f, .32f, .32f);
					firePoint = tempScale;


				}
				if (timer > 1.5f && timer < 1.75f) {
					//rb.drag = 4.4f;

					alpha.b = 45;
					alpha.g = 45;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.43f, .43f, .43f);
					firePoint = tempScale;


				}
				if (timer > 2f) {
					alpha.b = 0;
					alpha.g = 0;
					chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
					//Debug.Log (alpha.b);
					tempScale.localScale = new Vector3 (.5f, .5f, .5f);
					firePoint = tempScale;

					/*Vector2 tempVel = new Vector2 (0f,rb.velocity.y);
					rb.velocity = tempVel;//~~~~~~~
					Vector2 tempVel2 = new Vector2 (1f,rb.velocity.y);
					rb.velocity = tempVel;//~~~~~~~
					*/
					//rb.drag = 100f;
					//rb.drag = 4.4f;
					//rb.drag = 4.4f;

				}
			}
			//timer = Time.time%60;
			//add charge rate to charge damage every sec
			chargeDamage = (chargeDamage + (chargeRate * Time.deltaTime));
		}
			


		//Debug.Log (timer);

		if ((Input.GetKeyUp (KeyCode.RightAlt) || Input.GetKeyUp (KeyCode.Slash) )&& pc.isPaused == false) {
			
			//Debug.Log ("timer: " + timer );

			//Debug.Log (fireTimer);
			if (timer >= timeToCharge && charging == true) {
				currentWeaponIndex = 8;
				//fireTimer += Time.deltaTime;
				//if (fireTimer >= fireRate) || charging == true) {
					Shoot ();
					fireTimer = 0;
				//}
				chargeDamage = 0;
				charging = false;
				currentWeaponIndex = 0;
				alpha.a = 0f;
				alpha.b = 255;
				alpha.g = 255;
				chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;

			} 
			charging = false;
			Transform tempScale = firePoint;
			tempScale.localScale = new Vector3 (0, 0, 0);
			firePoint = tempScale;
			timer = 0;


		}
	}

	void Shoot() {
		//Debug.Log (timer);
		fireTimer = 0f;
		if (chargeShotTimer >= timeToCharge) {
			Debug.Log ("key Pressed for charge");
		}
		if (currentWeaponIndex == 5) {
			Debug.Log ("in shoot right before grapple");
			grappleHook.grappleIsEnabled = true;// set to false
			grappleHook.StartGrapple ();
			return;
		}

		//check to see which ammo is equipped, List of bullet prefabs with diff textures and damages
		//Debug.Log("currentWeapon index: " + currentWeaponIndex);
		//Debug.Log("shooting");
		Vector2 scatter2 = new Vector2 (firePoint.position.x, firePoint.position.y+1f);
		Vector2 scatter3 = new Vector2 (firePoint.position.x, firePoint.position.y-1f);
		Vector2 scatter4 = new Vector2 (firePoint.position.x, firePoint.position.y+.5f);
		Vector2 scatter5 = new Vector2 (firePoint.position.x, firePoint.position.y-.5f);
		if (pc.weaponIndex == 0) {
			if (pc.scatterUpgradeAmount > 1) {
				ScatterShot (-1);
			}
			if (pc.scatterUpgradeAmount > 2) {
				ScatterShot (1);
			}
			if (pc.scatterUpgradeAmount > 3) {
				ScatterShot (.5f);

			}
			if (pc.scatterUpgradeAmount > 4) {
				ScatterShot (-.5f);
			}
		}
		GameObject bulletObject = Instantiate (bulletPrefab[currentWeaponIndex], firePoint.position, firePoint.rotation);
		bulletObject.SendMessage("PassedValue", pc); // passes the player controller to bullet
		//scatter shot
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			Destroy (bulletObject, 2.4f);
		} else {
			Destroy (bulletObject, 1.22f);
		}
		
		//spider grenade
		Vector3 bulletScale = bulletObject.transform.localScale;
		bulletScale.x *= (transform.localScale.x >= 0) ? 1 : -1;
		bulletObject.transform.localScale = bulletScale;

		if (currentWeaponIndex == 1) {
			pc.missileCount -= 1;
		}
		if (pc.missileCount <= 0) {
			currentWeaponIndex = 0;
		}
	}

	public void WeaponCheck(int weaponChoice) {
		

		//Debug.Log("weopon choice: " + weaponChoice);
			
		currentWeaponIndex = weaponChoice;
			
	}

	//Scattershot, shoots extra bullets
	private void ScatterShot(float offset) {
		Vector2 scatter1 = new Vector2 (firePoint.position.x, firePoint.position.y+offset);
		if(Input.GetKey(KeyCode.W)) {
			scatter1 = new Vector2 (firePoint.position.x+offset, firePoint.position.y);
		}
		if(Input.GetKey(KeyCode.S)) {
			scatter1 = new Vector2 (firePoint.position.x+offset, firePoint.position.y);
		}
		GameObject bulletObjectScatter = Instantiate (bulletPrefab[currentWeaponIndex], scatter1, firePoint.rotation);
		bulletObjectScatter.SendMessage("PassedValue", pc); // passes the player controller to bullet
		Vector3 bulletScale = bulletObjectScatter.transform.localScale;
		bulletScale.x *= (transform.localScale.x >= 0) ? 1 : -1;
		bulletObjectScatter.transform.localScale = bulletScale;
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			Destroy (bulletObjectScatter, 2.4f);
		} else {
			Destroy (bulletObjectScatter, 1.22f);
		}
	}

	private void ChargeColorChange() { // why????????
		if (timer > .25f && timer < .5f) {
			alpha.b = 213;
			alpha.g = 213;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
		}
		if (timer > .5f && timer < .75f) {
			alpha.b = 171;
			alpha.g = 171;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;

		}
		if (timer > .75f && timer < 1f) {
			alpha.b = 129;
			alpha.g = 129;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
		}
		if (timer > 1.25f && timer < 1.5f) {
			alpha.b = 87;
			alpha.g = 87;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
		}
		if (timer > 1.5f && timer < 1.75f) {
			alpha.b = 45;
			alpha.g = 45;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
		}
		if (timer > 2f) {
			alpha.b = 0;
			alpha.g = 0;
			chargingSprite.GetComponent<SpriteRenderer> ().color = alpha;
		}
	}
		
}
