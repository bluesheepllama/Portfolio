using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	public GameObject player;
	public float maximumHitpoints = 3f;
	public float hitPoints;
	public GameObject healthPrefab;
	public GameObject missileAmmoPrefab;
	public GameObject keyPrefab;

	public int healAmountMin = 2;
	public int healAmountMax = 7;

	private SpriteRenderer spriteRenderer;
	private PlayerController playerController;
	private float colorChangLength = 8f;
	private float colorChangeCounter;
	private bool colorchangebool = false;

	public bool isVenomDamage;
	private float venomCounter;
	private float venomLength = 2f;


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		hitPoints = maximumHitpoints;

	}

	void Update () {
		if (colorchangebool == true) {
			colorChangeCounter++;
		}
		//
		if (gameObject.tag == "Enemy") {// get rid of and move to venomClass
			if (isVenomDamage) {
				venomCounter += Time.deltaTime;
				if (venomCounter >= venomLength) {
					isVenomDamage = false;
					hitPoints = hitPoints;


				}
			}
		}
				//
		if (colorChangeCounter > colorChangLength) {
			

			if (spriteRenderer) {
				spriteRenderer.color = Color.white;
				colorChangeCounter = 0;
				colorchangebool = false;
			}
		}
	}

	public void TakeDamage(float damageAmount) {

		ModifyHitPoints (-damageAmount);
		if (spriteRenderer) {
			spriteRenderer.color = Color.red;
			colorChangeCounter += Time.deltaTime;
			colorchangebool = true;
		}

	}

	public void Heal(float healAmount) {

		healAmount = Random.Range (healAmountMin,healAmountMax);
		ModifyHitPoints (healAmount);

	}
	//changed void to int
	private void ModifyHitPoints(float modAmount) {

		hitPoints += modAmount;
		hitPoints = Mathf.Min (maximumHitpoints, hitPoints);
		//Debug.Log (tag +hitPoints);
		if (hitPoints <= 0f) {
			Die ();
		}
	}

	private void Die() {
		//player.SetActive (false);

		if (player.tag != "Player") {//wont work for enemies to hit player
			float ranNum = Random.Range (-1f, 1f);
			//Debug.Log("right before spawn health");
			Vector3 spawnPosition = new Vector3(player.transform.position.x+ranNum,player.transform.position.y+ranNum,player.transform.position.z);
			if (healthPrefab) {
				GameObject healthpre = Instantiate (healthPrefab, player.transform.position, player.transform.rotation) as GameObject;
				Destroy (healthpre,10);
			}
			if(missileAmmoPrefab && playerController.haveMissle) {
				//Debug.Log ("spawn missile");
				GameObject missilepre = Instantiate(missileAmmoPrefab, spawnPosition,player.transform.rotation) as GameObject;
				Destroy (missilepre,10);
			}
			if(keyPrefab) {
				//Debug.Log ("spawn missile");
				GameObject keyPre = Instantiate(keyPrefab, spawnPosition,player.transform.rotation) as GameObject;
			}
			//Invoke ("Respawn", 50);// time to respawn
		}
			
		Destroy (gameObject);

	}
	private void Respawn() {
		//GameObject bulletObject = Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		player.SetActive(true);
		ModifyHitPoints (maximumHitpoints);
	}
}
