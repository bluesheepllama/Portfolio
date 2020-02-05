using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree.Types;

//namespace BayatGames.SaveGameFree.Examples {
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

	public SaveGameTrigger saveTrigger;

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

	public GameObject welcomeScreen;
	public Button welcomeScreenClose;
	public Button exitButton;

	public GameObject saveUI;
	public Button yesButton;
	public Button noButton;
	public Button loadButton;

	public GameObject loadUI;

	private SoundController soundController;
	private AudioSource songSource;
	private SaveData savedata;

	public int tempforSave = 0;

	public AudioClip songClip;
	// Use this for initialization
	void Start () {
		savedata = new SaveData ();
		soundController = GetComponent<SoundController> ();
		songSource = soundController.AddAudio (songClip,true,true,.3f);
		//aS.volume = 100F;
		songSource.Play ();
		//PauseUI = GameObject.FindGameObjectsWithTag("PauseUI");
		PauseGame();
	}
	public void CloseWelcomeScreen() {
		welcomeScreen.SetActive (false);
		ResumeGame ();
	}
	public void ExitGame() {
		Debug.Log ("quit game");
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
		welcomeScreenClose.onClick.AddListener (CloseWelcomeScreen);
		exitButton.onClick.AddListener (ExitGame);
		yesButton.onClick.AddListener (Save);
		loadButton.onClick.AddListener (Load);
		noButton.onClick.AddListener (NoPressed);
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
		saveUI.SetActive (true);

	}

	public void ResumeGame() {
		pauseUI.SetActive(false);
		Time.timeScale = 1;
		pausePressed = true;
		playerController.isPaused = false;
		saveUI.SetActive (false);

	}


	//~~~~~~~~~~~~~~~~put in saver
	public void Save() {
		saveUI.SetActive (true);
		Time.timeScale = 0;
		playerController.isPaused = true;
		if(yesButton) {
			//use setter function from save storage data
			savedata.hitpoints = destructable.hitPoints;
			saveUI.SetActive(false);
			ResumeGame ();
			saveTrigger.isSaveTriggered = false;
			Debug.Log ("Yes Save button pressed");
			//SaveGame.Save<int> ( "score", score );
			Debug.Log("Save Game: " + destructable.hitPoints);
			SaveGame.Save<SaveData> ( "hitpoints", savedata  );


		}

	}
	public void Load() {
		
		//saveUI.SetActive (true);
		Time.timeScale = 0;
		playerController.isPaused = true;
			if(loadButton) {
			//SaveGame.Save<int> ( "score", score );
			//SceneManager.LoadScene("Scene01");
			//Application.LoadLevel(Application.loadedLevel);
			//use getter function to get storage data from save

			//Load function needs to save to an object
			//make a class of all data to be saved, make class then set them all equal with getter and setter functions
			//T = SaveGame.Load<float> ( "hitpoints", destructable.hitPoints );
			/*target.position = SaveGame.Load<Vector3Save>(
			identifier,
			Vector3.zero,
			SerializerDropdown.Singleton.ActiveSerializer);*/
			float loadTemp = 0;

			savedata = SaveGame.Load<SaveData> ( "hitpoints", savedata );
			//SceneManager.LoadScene("Scene01");
			destructable.hitPoints = savedata.hitpoints;
			Debug.Log ("Load Game: " + destructable.hitPoints);
			saveTrigger.isSaveTriggered = false;
			saveUI.SetActive (false);



			}

		}
	public void NoPressed(){
	   
		saveUI.SetActive(false);
		ResumeGame ();
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
			powerUpDescription.text = "Charge Shot: Hold shoot to charge your shot, dealing more damage. Also lets you break more walls";
			break;
		default :
			break;
		}

	}


	public void DestantiatWallMsg() {
		destansiateWallTriggerMsg.SetActive (true);
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Period)) {//
			destansiateWallTriggerMsg.SetActive (false);

		}
	}

}
//}
