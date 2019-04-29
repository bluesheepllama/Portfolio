using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameGUI : MonoBehaviour {

	public int goalCountToWin;
	//public Animator winScreenAnimator;
	public PickupGetter playerPickupGetter;


	//public List<GameObject> fullHealthIndicators;
	//public List<GameObject> scoreIndicator;
	//public Text textScore;
	public Camera mainCamera;
	//public GameObject loseScreen;
	//
	//public GameObject winScreen;
	//
	public GameObject player;
	public Destructable playerHealth;

	public Destructable playerDestructable;
	//public GameObject scoreText;
	public PickupGetter pickUpGetter;
	//private int money = 0; // not needed delete
	// Use this for initialization
	//delete start
	void Start () {

		//loseScreen.SetActive (false);
		//pickUpGetter = GetComponent<PickupGetter> ();
	}
	
	// Update is called once per frame
	void Update () {



//		if(playerPickupGetter.GetPickupcount(PickupType.Goal) >= goalCountToWin) {
			//// shows win screen with no animation, to show animation, check WinScreen and uncheck the panel,button,and text
			//winScreen.SetActive (true);
			//
			//winScreenAnimator.SetTrigger ("Show");
			//Debug.Log ("You win");
		//}
		if(playerHealth.hitPoints <= 0 || player.transform.position.y < -40f) {//change if the map goes below value
			//loseScreen.SetActive (true);
			//invoke repeating here for 3 seconds


			switch (pickUpGetter.checkPointcount) {
			case 0:
				RestartGame ();
				break;
			case 1: 
				//make function called spawn at checkpoint
				Debug.Log ("spawn at checkpoint 1");
				player.SetActive (true);
				playerHealth.hitPoints = 3;
				player.transform.position =  new Vector3( -3f, -33.05f, -0.7409099f);
				mainCamera.transform.position = new Vector3( -3f, -33.05f, -0.7409099f);//play with the x coordinate 0 maybe?
				break;
			case 2:
				Debug.Log ("spawn at checkpoint 1");
				player.SetActive (true);
				playerHealth.hitPoints = 3;
				player.transform.position =  new Vector3(-117.5f,-14.68f,-0.7409099f);
				mainCamera.transform.position = new Vector3(-117.5f,-14.68f,-0.7409099f);
				break;

			default :
				break;


			}
			//

		}
		//UpdateHealthUI ();
		//UpdateScoreUI ();
		
	}
	/*
	private void UpdateHealthUI() {
		int healthPoints = playerHealth.hitPoints; // change to getter function
		for (int i = 0; i < fullHealthIndicators.Count; i++) {
			if (i < healthPoints) {
				fullHealthIndicators [i].SetActive (true);
			} else {
				fullHealthIndicators [i].SetActive (false);

			}
		}
	}
	*/

	/*
	private void UpdateScoreUI() {
		int score = playerPickupGetter.GetPickupcount (PickupType.Money);
		textScore.text = (score).ToString() ;

	}
	*/

	public void RestartGame() { // put in new class
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene (scene.name);
	}
	//
}
