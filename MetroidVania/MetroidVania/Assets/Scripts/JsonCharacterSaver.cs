using System.IO;
using UnityEngine;
// save location: Users/<username>/Library/Application Support/<company name>/<app name>
public class JsonCharacterSaver : MonoBehaviour {

	//serializable class
	public CharacterData characterData;
	string dataPath;
	//ui variables when created

	void Start ()
	{
		dataPath = Path.Combine(Application.persistentDataPath, "CharacterData.txt");
	}

	void Update ()
	{
		//change to be used in UI
		if(Input.GetKeyDown (KeyCode.P))
			SaveCharacter (characterData, dataPath);

		if (Input.GetKeyDown (KeyCode.L))
			characterData = LoadCharacter (dataPath);
	}

	static void SaveCharacter (CharacterData data, string path)
	{
		string jsonString = JsonUtility.ToJson (data);

		using (StreamWriter streamWriter = File.CreateText (path))
		{
			streamWriter.Write (jsonString);
		}
	}

	static CharacterData LoadCharacter (string path)
	{
		using (StreamReader streamReader = File.OpenText (path))
		{
			string jsonString = streamReader.ReadToEnd ();
			return JsonUtility.FromJson<CharacterData> (jsonString);
		}
	}
}