﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public List<GameObject> powerUps;
	public Text powerUpDescription;
	public Text healthTotal;
	public Text missileTotal;
	public Destructable destructable;
	public PlayerController playerController;
	public GameObject pauseUI;
	public bool pausePressed = false;
	public GameObject destansiateWallTriggerMsg;

	public GameObject saveUI;
	public Button yesButton;
	public Button noButton;

	// Use this for initialization
	void Start () {
		//PauseUI = GameObject.FindGameObjectsWithTag("PauseUI");
	}
	
	// Update is called once per frame
	void Update () {
		healthTotal.text = destructable.hitPoints.ToString();
		missileTotal.text = playerController.missileCount.ToString ();
		if (Input.GetKeyDown (KeyCode.P)) {
			if (pausePressed == true) {
				PauseGame ();

			} else {
				ResumeGame ();

			}
		}
		if(destansiateWallTriggerMsg.active ) {
			if (Input.GetKeyDown (KeyCode.Space)) {//
				destansiateWallTriggerMsg.SetActive (false);
			}
		}
			

	}
	public void PauseGame() {
		pauseUI.SetActive(true);
		Time.timeScale = 0;
		pausePressed = false;
		playerController.isPaused = true;
	}

	public void ResumeGame() {
		pauseUI.SetActive(false);
		Time.timeScale = 1;
		pausePressed = true;
		playerController.isPaused = false;
	}


	//~~~~~~~~~~~~~~~~put in saver
	public void Save() {
		saveUI.SetActive (true);
		Time.timeScale = 0;
		playerController.isPaused = true;
		if(yesButton) {
		}

	}


	public void PowerUpAquire(int i) {
		PauseGame ();
		powerUps[i].SetActive (true);

		switch (i) {
		case 0:
			powerUpDescription.text = "High Jump: This powerup allows you to jump higher";
			break;
		case 1:
			powerUpDescription.text = "Shrink: This powerup allows you to get smaller, fitting in tighter areas. Press 's' to shrink";
			break;
		case 2:
			powerUpDescription.text = "Missile: This powerup allows you to shoot missiles, dealing more damage and allowing you to break some walls. Switch between ammo types with '1-5'";
			break;
		case 3:
			powerUpDescription.text = "Higher Jump: This powerup allows you to jump even higher";
			break;
		case 4:
			powerUpDescription.text = "";
			break;
		case 5:
			powerUpDescription.text = "";
			break;
		case 6:
			powerUpDescription.text = "";
			break;
		default :
			break;
		}

	}

	public void DestantiatWallMsg() {
		destansiateWallTriggerMsg.SetActive (true);
		if (Input.GetKeyDown (KeyCode.Space)) {//
			destansiateWallTriggerMsg.SetActive (false);

		}
	}

}
