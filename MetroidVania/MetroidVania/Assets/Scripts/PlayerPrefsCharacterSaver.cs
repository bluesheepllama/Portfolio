using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsCharacterSaver : MonoBehaviour {
	public CharacterData characterData;
	//public PlayerController playerController;

	//public bool isSaveTriggered;
	public UIController uiController;
	public GameObject saveUI;
	public Button yesButton;
	public Button noButton;
	public SaveGameTrigger saveTrigger;

	void Update ()
	{

		//saves the gamez if save is triggered
		if (saveTrigger) {
			if (saveTrigger.isSaveTriggered == true) {
				uiController.PauseGame ();
				saveUI.SetActive (true);
				/*if (yesButton.onClick.AddListener ()) {
				SaveCharacter (characterData,0);//look into slot number
				//tell them it saved
				saveUI.SetActive(false);
				uiController.ResumeGame ();
			}
		} else if(noButton.onClick.AddListener ()) {
			saveUI.SetActive(false);
			uiController.ResumeGame ();
		}
		*/
				//yesButton.onClick.AddListener (YesClick);
				//noButton.onClick.AddListener (NoClick);

				/*if (Input.GetKeyDown (KeyCode.S))
				SaveCharacter (characterData, 0);

			if (Input.GetKeyDown (KeyCode.L))
				characterData = LoadCharacter (0);
				*/
			}
		}
		
	}

	/*private void YesClick() {
		SaveCharacter (characterData,0);//look into slot number
		//tell them it saved
		saveUI.SetActive(false);
		uiController.ResumeGame ();
		saveTrigger.isSaveTriggered = false;
		Debug.Log ("Yes Save button pressed");
	}*/
	private void NoClick() {
		//tell them it saved
		saveUI.SetActive(false);
		uiController.ResumeGame ();
		saveTrigger.isSaveTriggered = false;
		Debug.Log ("no Save button pressed");

	}


	static void SaveCharacter (CharacterData data, int characterSlot)
	{
		PlayerPrefs.SetString ("characterName_CharacterSlot" + characterSlot, data.characterName);//first param is name of ave, second is the name of the actual string

		PlayerPrefs.SetInt ("missileCount_CharacterSlot" + characterSlot, data.missileCount);
		PlayerPrefs.SetInt ("maxMissileCount_CharacterSlot" + characterSlot, data.maxMissileCount);
		PlayerPrefs.SetInt ("weaponIndex_CharacterSlot" + characterSlot, data.weaponindex);
		PlayerPrefs.SetFloat ("playerPositionX_CharacterSlot" + characterSlot, data.playerPosistionX);
		PlayerPrefs.SetFloat ("playerPositionY_CharacterSlot" + characterSlot, data.playerPosistionY);
		PlayerPrefs.SetFloat ("playerPositionZ_CharacterSlot" + characterSlot, data.playerPosistionZ);

		PlayerPrefs.SetFloat ("hitPoints_CharacterSlot" + characterSlot, data.hitPoints);
		PlayerPrefs.SetFloat ("maxHitPoints_CharacterSlot" + characterSlot, data.maxHitPoints);

		SetBool ("haveShrink_CharacterSlot",data.haveShrink);
		SetBool ("haveDoubleJump_CharacterSlot",data.haveDoubleJump);
		SetBool ("haveGrapple_CharacterSlot",data.haveGrapple);
		SetBool ("haveGrenade_CharacterSlot",data.haveGrenade);
		SetBool ("haveScatterShot_CharacterSlot",data.haveScatterShot);
		SetBool ("haveShootThroughWalls_CharacterSlot",data.haveShootThroughWalls);
		SetBool ("haveVenomShot_CharacterSlot",data.haveVenomShot);
		SetBool ("haveWallCliimb_CharacterSlot",data.haveWallClimb);
		SetBool ("haveWebShot_CharacterSlot",data.haveWebShot);
		SetBool ("shrinkEnabled_CharacterSlot",data.shrinkEnabled);

		PlayerPrefs.Save ();
	}

	static void SetBool(string key, bool state)
	{
		PlayerPrefs.SetInt(key, state ? 1 : 0);
	}



	static CharacterData LoadCharacter (int characterSlot)
	{
		CharacterData loadedCharacter = new CharacterData ();
		loadedCharacter.characterName = PlayerPrefs.GetString ("characterName_CharacterSlot" + characterSlot);

		loadedCharacter.missileCount = PlayerPrefs.GetInt ("missileCount_CharacterSlot" + characterSlot);
		loadedCharacter.maxMissileCount = PlayerPrefs.GetInt ("maxMissileCount_CharacterSlot" + characterSlot);
		loadedCharacter.weaponindex = PlayerPrefs.GetInt ("weaponIndex_CharacterSlot" + characterSlot);

		loadedCharacter.playerPosistionX = PlayerPrefs.GetFloat ("playerPositionX_CharacterSlot" + characterSlot);
		loadedCharacter.playerPosistionY = PlayerPrefs.GetFloat ("playerPositionY_CharacterSlot" + characterSlot);
		loadedCharacter.playerPosistionZ = PlayerPrefs.GetFloat ("playerPositionZ_CharacterSlot" + characterSlot);

		loadedCharacter.hitPoints = PlayerPrefs.GetFloat ("hitPoints_CharacterSlot" + characterSlot);
		loadedCharacter.maxHitPoints = PlayerPrefs.GetFloat ("maxHitPoints_CharacterSlot" + characterSlot);


		loadedCharacter.haveShrink = GetBool ("haveShrink_CharacterSlot");
		loadedCharacter.haveDoubleJump = GetBool ("haveDoubleJump_CharacterSlot");
		loadedCharacter.haveGrapple = GetBool ("haveGrapple_CharacterSlot");
		loadedCharacter.haveGrenade = GetBool ("haveGrenade_CharacterSlot");
		loadedCharacter.haveScatterShot = GetBool ("haveScatterShot_CharacterSlot");
		loadedCharacter.haveShootThroughWalls = GetBool ("haveShootThroughWalls_CharacterSlot");
		loadedCharacter.haveVenomShot = GetBool ("haveVenomShot_CharacterSlot");
		loadedCharacter.haveWallClimb = GetBool ("haveWallCliimb_CharacterSlot");
		loadedCharacter.haveWebShot = GetBool ("haveWebShot_CharacterSlot");
		loadedCharacter.shrinkEnabled = GetBool ("shrinkEnabled_CharacterSlot");
	
		return loadedCharacter;
	}

	public static bool GetBool(string key)
	{
		int value = PlayerPrefs.GetInt(key);

		if (value == 1)
		{
			return true;
		}

		else
		{
			return false;
		}
	}
}
