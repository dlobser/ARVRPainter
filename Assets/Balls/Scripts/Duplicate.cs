using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour {

	public GameObject thing;
	public int amount;
	public float spread;

	int count = 0;
	// Use this for initialization
	void Start () {
		StartCoroutine (Dup ());
//		for (int i = 0; i < amount; i++) {
//			Instantiate(thing, Random.onUnitSphere * spread, Quaternion.identity,this.transform);
//		}
	}

	IEnumerator Dup(){
		while (count < amount) {
			count++;
			Instantiate(thing, Random.onUnitSphere * spread, Quaternion.identity,this.transform);
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
