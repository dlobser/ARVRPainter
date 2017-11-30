using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour {

	public string target;
	GameObject Target;
	Vector3 force;
	// Use this for initialization
	void Start () {
		Target = GameObject.Find (target);
	}

	// Update is called once per frame
	void Update () {
		force = Target.transform.position - this.transform.position;
		this.GetComponent<Rigidbody>().AddForce(force);
	}
}
