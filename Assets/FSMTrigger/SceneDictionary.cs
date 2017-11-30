using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDictionary : MonoBehaviour {

	public Dictionary<string,GameObject> dictionary;

	void Start () {
		dictionary = new Dictionary<string,GameObject> ();
		dictionary ["poop"] = this.gameObject;
		makeDict (this.transform);
	}

	void makeDict(Transform go)
	{
		dictionary [go.gameObject.name.ToString()] = go.gameObject;
		for (int i = 0; i < go.childCount; i++) {
			if (!dictionary.ContainsKey (go.GetChild (i).gameObject.name.ToString ()))
				dictionary [go.GetChild (i).gameObject.name.ToString ()] = go.GetChild (i).gameObject;
			else
				Debug.LogWarning ("Duplicate Object: " + go.GetChild (i).gameObject.name + " will be excluded from dictionary");
			if(go.GetChild(i).childCount>0)
				makeDict(go.GetChild(i));
		}
	}
}
