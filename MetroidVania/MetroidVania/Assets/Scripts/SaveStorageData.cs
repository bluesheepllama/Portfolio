using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class SaveStorageData : MonoBehaviour {

	public class SaveData
	{

		public float hitpoints;
		//public int highScore;
		//public List<Level> levels;

		public SaveData()
		{
		

			// Dummy data
			
		}
	public float GetSaveData(){
		return hitpoints;
	}
	public void SetSaveData(float s) {
		hitpoints = s;
	}

	}
//}
