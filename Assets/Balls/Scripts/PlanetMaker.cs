using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMaker : MonoBehaviour {

	public Sprite[] bodies;

	GameObject container;

	public bool rebuild;

	public int amount;

	void Start () {
		container = this.gameObject;
		MakeBird ();
	}

	void Update(){
		if (rebuild) {
			MakeBird ();
//			rebuild = false;
		}
	}


	GameObject SetupSprite(Sprite s){
		GameObject g = new GameObject ();
		GameObject p = new GameObject ();
		g.transform.parent = p.transform;
		g.transform.localPosition = Vector3.forward;
		SpriteRenderer sp = g.AddComponent<SpriteRenderer> ();
		sp.sprite = s;
		g.name = s.name;
		p.transform.localEulerAngles = Random.insideUnitSphere * 360;
		p.transform.parent = container.transform;
		return p;
	}

	void MakeBird(){

		if (container.transform.childCount > 0) {
			for (int i = 0; i < container.transform.childCount; i++) {
				GameObject g = container.transform.GetChild (i).gameObject;
				Destroy (g);
			}
		}

		for (int i = 0; i < amount; i++) {
			GameObject g = SetupSprite (bodies [(int)Random.Range (0, bodies.Length)]);

		}


	}


}
