using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnCollide : MonoBehaviour {

	public string colliderName;

	void OnCollisionEnter(Collision c){
//		if (c.gameObject.name == colliderName)
			this.GetComponent<Rigidbody> ().isKinematic = true;
	}
}
