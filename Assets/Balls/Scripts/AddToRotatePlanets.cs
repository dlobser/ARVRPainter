using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToRotatePlanets : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FindObjectOfType<RotatePlanets> ().Add (this.gameObject);
	}

}
