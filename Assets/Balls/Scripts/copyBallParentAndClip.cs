using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyBallParentAndClip : MonoBehaviour {

	public GameObject side;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = side.transform.position;
		this.transform.eulerAngles = side.transform.eulerAngles;
		this.GetComponent<CirclePacker> ().clip = new Vector2 (side.transform.localScale.x*.5f, side.transform.localScale.y*.5f);
	}
}
