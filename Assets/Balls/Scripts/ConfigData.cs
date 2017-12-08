using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

[System.Serializable]
public class DataHolder
{
	// Fields that are needed for SetCubeSize
	public float parentPosX;
	public float parentPosY;
	public float parentPosZ;
	public float parentRotY;

	public float camPosY;
	public float camFOV;
	public float camRotX;
	public float camRotY;


	public float scaleX;
	public float scaleY;
	public float scaleZ;

	public string timestamp;
}

/*
 * ConfigData
 *   Writes data from PlayerPrefs to the StreamingAssets/settings.json file.
 *   And reads data from the StreamingAssets/settings.json file and saves it in PlayerPrefs.
 *  Note: other code, elsewhere, needs to move the data between PlayerPrefs and the actual game objects the settings are supposed to affect.
 */
public class ConfigData : MonoBehaviour {

	public DataHolder data;

	public string dataFileName = "settings.json";
	public string filePath;
	// Use this for initialization
	void Start () {

		filePath = Path.Combine(Application.streamingAssetsPath, dataFileName);
		data = new DataHolder ();

		//		LoadConfig();   // Uncomment this if you want to load from data file when game starts

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Reads data from the StreamingAssets/settings.json file and saves it in local data field, then calls UpdatePlayerPrefs
	public void LoadConfig()
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		Debug.Log("In LoadConfig.");
		if(File.Exists(filePath))
		{
			Debug.Log ("Opening file: " + filePath);
			// Read the json from the file into a string
			string dataAsJson = File.ReadAllText(filePath); 
			Debug.Log ("Read: " + dataAsJson);
			// Pass the json to JsonUtility, and tell it to fill in the fields in the data object

			JsonUtility.FromJsonOverwrite (dataAsJson, this.data);
			Debug.Log (data.timestamp);
			UpdatePlayerPrefs ();
		}
		else
		{
			Debug.LogError("Cannot load game data!");
		}

	}

	// Copies data from the local data field to the PlayerPrefs
	private void UpdatePlayerPrefs()
	{
		PlayerPrefs.SetFloat ("parentPosX", data.parentPosX);
		PlayerPrefs.SetFloat ("parentPosY", data.parentPosY);
		PlayerPrefs.SetFloat ("parentPosZ", data.parentPosZ);
		PlayerPrefs.SetFloat ("parentRotY", data.parentRotY);

		PlayerPrefs.SetFloat ("camPosY", data.camPosY);
		PlayerPrefs.SetFloat ("camFOV", data.camFOV);
		PlayerPrefs.SetFloat ("camRotX", data.camRotX);
		PlayerPrefs.SetFloat ("camRotY", data.camRotY);


		PlayerPrefs.SetFloat ("scaleX", data.scaleX);
		PlayerPrefs.SetFloat ("scaleY", data.scaleY);
		PlayerPrefs.SetFloat ("scaleZ", data.scaleZ);
		PlayerPrefs.Save ();
	}

	// Copies data from PlayerPrefs to the local data field, then serializes it to Json and writes it to the settings file.
	public void SaveConfig()
	{
		data.parentPosX = PlayerPrefs.GetFloat ("parentPosX");
		data.parentPosY = PlayerPrefs.GetFloat ("parentPosY");
		data.parentPosZ = PlayerPrefs.GetFloat ("parentPosZ");
		data.parentRotY = PlayerPrefs.GetFloat ("parentRotY");

		data.camPosY= PlayerPrefs.GetFloat ("camPosY");
		data.camFOV = PlayerPrefs.GetFloat ("camFOV");

		data.camRotX = PlayerPrefs.GetFloat ("camRotX");
		data.camRotY = PlayerPrefs.GetFloat ("camRotY");
		data.scaleX = PlayerPrefs.GetFloat ("scaleX");
		data.scaleY = PlayerPrefs.GetFloat ("scaleY");
		data.scaleZ = PlayerPrefs.GetFloat ("scaleZ");

		data.timestamp = System.DateTime.Now.ToString ();

		string outJson = JsonUtility.ToJson (data, true);
		Debug.Log ("Saving to "+filePath+"; data:" + outJson);
		File.WriteAllText (filePath, outJson);
	}

}
