using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControls : MonoBehaviour {

	public List<BirdMaker> birds;
	public float speed;
	float counter;

	Vector3[] ctrls;
	// Use this for initialization
	void Start () {
		Setup ();
	}

	public void Setup(){
		birds = new List<BirdMaker> ();//FindObjectsOfType<BirdMaker> ();
		ctrls = new Vector3[4];
		for (int i = 0; i < ctrls.Length; i++) {
			ctrls [i] = Vector3.zero;
		}
	}

	
	// Update is called once per frame
	void Update () {
//		birds = FindObjectsOfType<BirdMaker> ();

		Vector3 z = Vector3.zero;
		counter += Time.deltaTime * speed;
		for (int i = 0; i < birds.Count; i++) {
			float id = birds [i].id;
			float rLeg = ns (counter + id, 60, 120);
			float lLeg = ns (counter + id * 1.5f, 45, 100);
			float head = ns (counter + id * 2, -30, 10);
			float bod = ns ((counter + id)*.2f, -10, 10);
			ctrls [0].Set (0, 0, rLeg);
			ctrls [1].Set (0, 0, lLeg);
			ctrls [2].Set (0, 0, head);
			ctrls [3].Set (0, 0, bod);
			if(birds[i]!=null)
				birds [i].Animate (ctrls [0], ctrls [1], ctrls [2], ctrls [3]);
		}
	}

	float ns(float t, float low, float high){
		return (Mathf.PerlinNoise (t, t)*(high-low))+low;
	}
}
