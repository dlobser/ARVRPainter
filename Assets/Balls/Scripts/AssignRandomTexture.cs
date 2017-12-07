using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRandomTexture : MonoBehaviour {

	public Texture[] textures;
	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer> ().material.mainTexture = textures [(int)Random.Range (0, textures.Length-1)];
	}
	

}
