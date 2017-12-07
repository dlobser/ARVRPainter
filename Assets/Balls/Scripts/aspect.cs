using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aspect : MonoBehaviour {

    public Camera cam;
    public float ass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cam.aspect = ass;
	}
}
