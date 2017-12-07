using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanets : MonoBehaviour {

	List<GameObject> planets;
	public float speed;
	Vector3 rot;
	// Use this for initialization
	void Start () {
		rot = new Vector3 (0, speed, 0);
		planets = new List<GameObject> ();
	}

	public void Add(GameObject g){
		planets.Add (g);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < planets.Count; i++) {
			if(planets[i]!=null)
				planets [i].transform.Rotate (rot*Time.deltaTime);
		}
	}
}
