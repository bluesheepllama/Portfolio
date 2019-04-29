using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
	Goal, // change
	DmgUpgrade,//JumpBonus,
	DoubleJump,
	JumpUpgrade, // and a temp jump bonus?
	Money,
	Health,
	Missile,
	SpeedBonus,
	GrappleHook,
	GrenadePower,
	ShrinkPower,
	HealthUpgrade,
	WebShotPower,
	MisslePower,
	MissleUpgrade,
	ScattershotPower,
	VenomPower,
	DamageUpgrade,
	Key,
	ChargeShot,
	GrenadeUpgrade,
	GrenadeAmmo,
	WallClimbPower

}
public class Pickup : MonoBehaviour {


	public PickupType pickupType;
	private float elapstTime;

	public List<AudioClip> pickupSounds;
	//private AudioRandomizer audioRandomizer;

	private void OnTriggerEnter2D(Collider2D collision) { // similar to collider
		//audioRandomizer = GetComponent<AudioRandomizer> ();
		PickupGetter touchedPickupGetter = collision.GetComponent<PickupGetter>();
		if(touchedPickupGetter) {
			
			/*if (audioRandomizer) {
				Debug.Log ("before pickup sound");//not going in if 
				audioRandomizer.playRandomSound (pickupSounds);
			}*/

			PickUp (touchedPickupGetter);

		}
	}

	private void PickUp(PickupGetter pickupGetter) {
		
		pickupGetter.PickUp(pickupType);
		gameObject.SetActive (false);
		//if (pickupType == PickupType.JumpBonus) {
			//Invoke ("EnableObject", 5);
		//}
		//Destroy(gameObject);

	}

	void EnableObject() {
		//Debug.Log ("jumpbonus enabled");
		gameObject.SetActive(true);
	}


}
