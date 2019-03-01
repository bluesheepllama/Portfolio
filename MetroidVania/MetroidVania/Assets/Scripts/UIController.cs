using System.Collections;
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

	//bullet icons
	public GameObject missileUI;
	public GameObject webUI;
	public GameObject venomUI;
	public GameObject grenadeUI;
	public GameObject scatterUI;
	public GameObject shootThrouWallsUI;
	public GameObject grappleUI;

	//Equiped
	public Image missileSprite;

	//turn UI on/off
	public GameObject miniMap;



	public GameObject saveUI;
	public Button yesButton;
	public Button noButton;

	private SoundController soundController;
	private AudioSource songSource;


	public AudioClip songClip;
	// Use this for initialization
	void Start () {
		soundController = GetComponent<SoundController> ();
		songSource = soundController.AddAudio (songClip,true,true,.3f);
		//aS.volume = 100F;
		songSource.Play ();
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
		if (Input.GetKeyDown (KeyCode.O)) {
			if (miniMap.activeSelf)
				miniMap.SetActive (false);
			else
				miniMap.SetActive (true);
		}


		if(playerController.haveMissle)
			missileUI.SetActive (true);
		else
			missileUI.SetActive (false);
		

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
			powerUpDescription.text = "Shrink: This powerup allows you to get smaller, fitting in tighter areas. Press 'x' to shrink";
			break;
		case 2:
			powerUpDescription.text = "Missile: This powerup allows you to shoot missiles, dealing more damage and allowing you to break some walls. Switch to missile with '2' or 'Left Shift";
			break;
		case 3:
			powerUpDescription.text = "Double Jump: This powerup allows you to Double Jump";
			break;
		case 4:
			powerUpDescription.text = "Missile Upgrade: This Upgrade lets you hold 10 more missiles";
			break;
		case 5:
			powerUpDescription.text = "Wellness Upgrade: This Upgrade Gives you 50 extra wellness";
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
