using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGetter : MonoBehaviour {

	List<PickupType> pickups;
	public int checkPointcount;
	private PlayerController pc;
	public UIController uiController;
	private SoundController soundController;

	//public float healthPickupMin = 2;
	//public float healthPickupMax = 7;


	public void Awake () {//gets run before any other function(before start)
		pickups = new List<PickupType>();
		pc = GetComponent<PlayerController> ();
		soundController = GetComponent<SoundController> ();

	}

	public void PickUp(PickupType pickupType) {
		pickups.Add (pickupType);

		//soundController.audio1.PlayOneShot(pickupSound,vol);

		//
		//New Powers
		if (pickupType == PickupType.ShrinkPower ) {
			if (pc && gameObject.tag == "Player") {
				Debug.Log("shrink enabled: ");

				pc.shrinkEnabled = true;
				//uiController.PowerUpAquire (1);
				Debug.Log("PlayAudio");
				//soundController.audio1.Play();

				//change shrink bool
			}
		}


		if (pickupType == PickupType.DoubleJump) {
			Jumper jumper = GetComponent<Jumper> ();
			if (jumper && gameObject.tag == "Player") {
				pc.haveDoubleJump = true;
				Debug.Log ("Jump Upgrade" );
				//uiController.PowerUpAquire (3); // change to correct Ui------------------------------
				Debug.Log("PlayAudio");
				//soundController.audio1.Play();
			}
		}
		if (pickupType == PickupType.WallClimbPower) {
			if (pc && gameObject.tag == "Player") {
				pc.haveWallClimb = true;
				Debug.Log ("wallClimb Upgrade" );
				//uiController.PowerUpAquire (3); // change to correct Ui------------------------------
				Debug.Log("PlayAudio");
				//soundController.audio1.Play();
			}
		}



		//new weapons

		//missle aquire
		if (pickupType == PickupType.MisslePower) {
			//PlayerController pc = GetComponent<PlayerController> ();
			if (pc) {
				Debug.Log("missle enabled: ");

				pc.haveMissle = true;
				uiController.PowerUpAquire (2);
				Debug.Log("PlayAudio");
				soundController.audio1.Play();
				//change shrink bool
			}
		}
		//charge shot
		if (pickupType == PickupType.ChargeShot) {
			//PlayerController pc = GetComponent<PlayerController> ();
			if (pc) {
				Debug.Log("charge shot enabled: ");

				pc.haveChargeShot = true;
				uiController.PowerUpAquire (6);
				Debug.Log("PlayAudio");
				soundController.audio1.Play();
				//change shrink bool
			}
		}
		//webshot aquire
		if (pickupType == PickupType.WebShotPower) {
			//PlayerController pc = GetComponent<PlayerController> ();
			if (pc) {
				Debug.Log("webshot enabled: ");

				pc.haveWebShot = true;
				uiController.PowerUpAquire (4);
				Debug.Log("PlayAudio");
				soundController.audio1.Play();
				//change shrink bool
			}
		}
		//sccattershot aquire
		if (pickupType == PickupType.ScattershotPower) {
			//PlayerController pc = GetComponent<PlayerController> ();
			if (pc) {
				Debug.Log("scattershot enabled: ");

				//pc.haveScatterShot = true;
				pc.scatterUpgradeAmount ++;
				//
				//uiController.PowerUpAquire (6);
				Debug.Log("PlayAudio");
				//soundController.audio1.Play();
				//change shrink bool
			}
		}
		if (pickupType == PickupType.VenomPower) {
			//PlayerController pc = GetComponent<PlayerController> ();
			if (pc) {
				Debug.Log("scattershot enabled: ");

				pc.haveVenomShot = true;
				uiController.PowerUpAquire (5);
				Debug.Log("PlayAudio");
				soundController.audio1.Play();
				//change shrink bool
			}
		}

		//Upgrades
		if (pickupType == PickupType.HealthUpgrade) {
			Destructable destructable = GetComponent<Destructable> ();
			if (destructable && gameObject.tag == "Player") {
				//uiController.PowerUpAquire (5);

				destructable.maximumHitpoints+= 25;//change to heal variable
				Debug.Log ("Health Upgrade" );
				//Destroy(gameObject);//maybe change 
				Debug.Log("PlayAudio");
				soundController.audio1.Play();
				uiController.PauseGame ();
				uiController.powerUpDescription.text = "Maximum wellness has Increased by 25";

			}
		}
		if (pickupType == PickupType.JumpUpgrade) {
			Jumper jumper = GetComponent<Jumper> ();
			if (jumper && gameObject.tag == "Player") {
				jumper.jumpImpulse += jumper.jumpUpgradeAmount;
				//Debug.Log ("Jump Upgrade" );
				//uiController.PowerUpAquire (0);
				Debug.Log("PlayAudio");
				//soundController.audio1.Play();
				//Destroy(gameObject);//maybe change 
			}
		}
		if (pickupType == PickupType.MissleUpgrade) {
			pc.maxMissileCount += 4;
			//uiController.PowerUpAquire (4);

			Debug.Log ("Missile Upgrade" );
			Debug.Log("PlayAudio");
			soundController.audio1.Play();
			uiController.PauseGame ();
			uiController.powerUpDescription.text = "Missile Capacity Increased by 5";
		}
		if (pickupType == PickupType.GrenadeUpgrade) {
			pc.maxMissileCount += 2;
			//uiController.PowerUpAquire (4);

			Debug.Log ("Grenade Upgrade" );
			Debug.Log("PlayAudio");
			soundController.audio1.Play();
			uiController.PauseGame ();
			uiController.powerUpDescription.text = "Grenade Capacity Increased by 2";
		}

		if (pickupType == PickupType.DamageUpgrade) {
			pc.bulletDmg *= 1.1f;
			//uiController.PowerUpAquire (4);

			Debug.Log ("Damge Upgrade" );
			Debug.Log("PlayAudio");
			soundController.audio1.Play();
			uiController.PauseGame ();
			uiController.powerUpDescription.text = "Bullet Damage Increased";
		}

		//Inventory
		if (pickupType == PickupType.Health) {
			Destructable destructable = GetComponent<Destructable> ();

			if (destructable) {
				int healAmount = Random.Range (2,7);
				destructable.Heal (healAmount);//change to heal variable
				//Debug.Log ("in healer" );
				//Destroy(gameObject);//maybe change 
			}
		}
		if (pickupType == PickupType.Missile) {
			if(pc.missileCount < pc.maxMissileCount)
			pc.missileCount += 1;
		}
		if (pickupType == PickupType.GrenadeAmmo) {
			if(pc.missileCount < pc.maxMissileCount)
				//pc.grenadeCount += 1;,
				Debug.Log("Grenade ammo pickup");
		}
			
		/*
		if (pickupType == PickupType.GrappleHook) {
			GrappleHook grappleHook = GetComponent<GrappleHook> ();
			if (grappleHook) {
				grappleHook.isEnabled = true;
				grappleHook.pickUpSound.Play();
			}
		}*/

		/*if (pickupType == PickupType.CheckPoint) {
			checkPointcount++;
			Debug.Log ("checkPoint count: " + checkPointcount);

		}*/

		if (pickupType == PickupType.Key) {
			/*KeyDestantiateWall kDW = GetComponent<KeyDestantiateWall>();
			kDW.DestantiateWall ();*/
			GameObject wall = GameObject.Find ("InstantiatedPlatformKey");//.GetComponent<GameObject>();
			if(wall) {
				wall.SetActive (false);
			}
		}
	}
		

	public int GetPickupcount(PickupType pickupType) {

		int count = 0;
		for (int i = 0; i < pickups.Count; i++) {
			if (pickups[i] == pickupType) {
				count++;
			}

		}
		return count;
	}
		
}
