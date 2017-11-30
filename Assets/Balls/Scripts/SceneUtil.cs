using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneUtil : MonoBehaviour {

	public GameObject balls;
	public GameObject[] planes;
	public Texture[] tex;
	public Texture GridTex;
	// Use this for initialization
	void Start () {
		tex = new Texture[planes.Length];
		for (int i = 0; i < tex.Length; i++) {
			tex [i] = planes [i].GetComponent<MeshRenderer> ().material.mainTexture;
			planes [i].GetComponent<MeshRenderer> ().material.mainTexture = GridTex;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			balls.SetActive (true);
			for (int i = 0; i < tex.Length; i++) {
				planes [i].GetComponent<MeshRenderer> ().material.mainTexture = tex[i];
			}
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
	}
}
