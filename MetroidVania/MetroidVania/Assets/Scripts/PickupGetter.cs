using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGetter : MonoBehaviour {

	List<PickupType> pickups;
	public int checkPointcount;
	private PlayerController pc;
	public UIController uiController;

	public void Awake () {//gets run before any other function(before start)
		pickups = new List<PickupType>();
		pc = GetComponent<PlayerController> ();

	}

	public void PickUp(PickupType pickupType) {
		pickups.Add (pickupType);

		//
		//New Powers
		if (pickupType == PickupType.ShrinkPower ) {
			if (pc && gameObject.tag == "Player") {
				Debug.Log("shrink enabled: ");

				pc.shrinkEnabled = true;
				uiController.PowerUpAquire (1);

				//change shrink bool
			}
		}


		if (pickupType == PickupType.DoubleJump) {
			Jumper jumper = GetComponent<Jumper> ();
			if (jumper && gameObject.tag == "Player") {
				pc.haveDoubleJump = true;
				Debug.Log ("Jump Upgrade" );
				uiController.PowerUpAquire (0); // change to correct Ui------------------------------

			}
		}

		//new weapons

		//missle aquire
		if (pickupType == PickupType.MisslePower) {
			PlayerController PC = GetComponent<PlayerController> ();
			if (PC) {
				Debug.Log("missle enabled: ");

				PC.haveMissle = true;
				uiController.PowerUpAquire (2);
				//change shrink bool
			}
		}
		//webshot aquire
		if (pickupType == PickupType.WebShotPower) {
			PlayerController PC = GetComponent<PlayerController> ();
			if (PC) {
				Debug.Log("webshot enabled: ");

				PC.haveWebShot = true;
				uiController.PowerUpAquire (4);

				//change shrink bool
			}
		}
		//sccattershot aquire
		if (pickupType == PickupType.ScattershotPower) {
			PlayerController PC = GetComponent<PlayerController> ();
			if (PC) {
				Debug.Log("scattershot enabled: ");

				PC.haveScatterShot = true;
				uiController.PowerUpAquire (6);

				//change shrink bool
			}
		}
		if (pickupType == PickupType.VenomPower) {
			PlayerController PC = GetComponent<PlayerController> ();
			if (PC) {
				Debug.Log("scattershot enabled: ");

				PC.haveVenomShot = true;
				uiController.PowerUpAquire (5);

				//change shrink bool
			}
		}

		//Upgrades
		if (pickupType == PickupType.HealthUpgrade) {
			Destructable destructable = GetComponent<Destructable> ();
			if (destructable && gameObject.tag == "Player") {
				destructable.maximumHitpoints+= 10;//change to heal variable
				Debug.Log ("Health Upgrade" );
				//Destroy(gameObject);//maybe change 
			}
		}
		if (pickupType == PickupType.JumpUpgrade) {
			Jumper jumper = GetComponent<Jumper> ();
			if (jumper && gameObject.tag == "Player") {
				jumper.jumpImpulse += jumper.jumpUpgradeAmount;
				Debug.Log ("Jump Upgrade" );
				uiController.PowerUpAquire (0);

				//Destroy(gameObject);//maybe change 
			}
		}
		if (pickupType == PickupType.MissleUpgrade) {
			pc.maxMissileCount += 5;
			Debug.Log ("Missile Upgrade" );
		}


		//Inventory
		if (pickupType == PickupType.Health) {
			Destructable destructable = GetComponent<Destructable> ();

			if (destructable) {
				int healAmount = Random.Range (2,7);
				destructable.Heal (healAmount);//change to heal variable
				Debug.Log ("in healer" );
				//Destroy(gameObject);//maybe change 
			}
		}
		if (pickupType == PickupType.Missile) {
			if(pc.missileCount < pc.maxMissileCount)
			pc.missileCount += 1;
		}
			
		/*
		if (pickupType == PickupType.GrappleHook) {
			GrappleHook grappleHook = GetComponent<GrappleHook> ();
			if (grappleHook) {
				grappleHook.isEnabled = true;
				grappleHook.pickUpSound.Play();
			}
		}*/

		if (pickupType == PickupType.CheckPoint) {
			checkPointcount++;
			Debug.Log ("checkPoint count: " + checkPointcount);

		}

	}

	public int GetPickupCount(PickupType pickupType) {

		int count = 0;
		for (int i = 0; i < pickups.Count; i++) {
			if (pickups[i] == pickupType) {
				count++;
			}

		}
		return count;
	}
		
}
