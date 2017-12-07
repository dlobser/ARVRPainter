using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMaker : MonoBehaviour {

	public Sprite[] heads;
	public Sprite[] bodies;
	public Sprite[] legs;
	public Sprite[] eyes;

	public Vector3 neckOnBod;
	public Vector3 neckonHead;
	public Vector3 hipOnLeg;
	public Vector3 rightLeg;
	public Vector3 leftLeg;
	public Vector3 eyeOnHead;

	GameObject container;

	GameObject headCtrl;
	GameObject bodyCtrl;
	GameObject lLegCtrl;
	GameObject rLegCtrl;
	GameObject eyeCtrl ;

	public bool rebuild;
	public float id {get;set;}

	BirdControls birdControls;

	void Start () {
		container = this.gameObject;
		birdControls = FindObjectOfType<BirdControls> ();
		MakeBird ();
	}

	void Update(){
		if (rebuild) {
			MakeBird ();
			rebuild = false;
		}
	}
	
	public void Animate(Vector3 lLegRot, Vector3 rLegRot, Vector3 headRot, Vector3 bodRot){
		lLegCtrl.transform.localEulerAngles = lLegRot;
		rLegCtrl.transform.localEulerAngles = rLegRot;
		headCtrl.transform.localEulerAngles = headRot;
		bodyCtrl.transform.localEulerAngles = bodRot;
	}

	GameObject SetupSprite(Sprite s, int order){
		GameObject g = new GameObject ();
		SpriteRenderer sp = g.AddComponent<SpriteRenderer> ();
		sp.sprite = s;
		sp.sortingOrder = order;
		g.name = s.name;
		g.layer = 8;
		return g;
	}

	public void MakeBird(){

		if(birdControls==null)
			birdControls = FindObjectOfType<BirdControls> ();
		
		if (container == null)
			container = this.gameObject;
		
		if (container.transform.childCount > 0) {
			for (int i = 0; i < container.transform.childCount; i++) {
				GameObject g = container.transform.GetChild (i).gameObject;
				Destroy (g);
			}
		}

		id = Random.value;

		GameObject head = SetupSprite (heads [(int)Random.Range(0,heads.Length)],-2);
		GameObject body = SetupSprite (bodies [(int)Random.Range(0,bodies.Length)],-3);
		GameObject lLeg = SetupSprite (legs [(int)Random.Range(0,legs.Length)],-4);
		GameObject rLeg = SetupSprite (legs [(int)Random.Range(0,legs.Length)],-2);
		GameObject eye = SetupSprite (eyes [(int)Random.Range(0,eyes.Length)],-1);

		headCtrl = new GameObject ();
		headCtrl.name = "headCtrl";
		bodyCtrl = new GameObject ();
		bodyCtrl.name = "BodyCtrl";
		lLegCtrl = new GameObject ();
		lLegCtrl.name = "LLegCtrl";
		rLegCtrl = new GameObject ();
		rLegCtrl.name = "RLegCtrl";
		eyeCtrl = new GameObject ();
		eyeCtrl.name = "EyeCtrl";

		body.transform.SetParent (bodyCtrl.transform);
		head.transform.SetParent (headCtrl.transform);

		lLegCtrl.transform.position = hipOnLeg;
		lLeg.transform.SetParent (lLegCtrl.transform);
		lLegCtrl.transform.position = leftLeg;

		rLegCtrl.transform.position = hipOnLeg;
		rLeg.transform.SetParent (rLegCtrl.transform);
		rLegCtrl.transform.position = rightLeg;

		eye.transform.SetParent (eyeCtrl.transform);
		eyeCtrl.transform.position = eyeOnHead;
		eyeCtrl.transform.parent = headCtrl.transform;
		headCtrl.transform.position = neckOnBod;


		headCtrl.transform.parent = bodyCtrl.transform;
		lLegCtrl.transform.parent = bodyCtrl.transform;
		rLegCtrl.transform.parent = bodyCtrl.transform;
		eyeCtrl.transform.parent = headCtrl.transform;

		bodyCtrl.transform.parent = container.transform.parent.transform;

		bodyCtrl.transform.position = container.transform.position;
		bodyCtrl.transform.localScale = container.transform.localScale;
		bodyCtrl.transform.localEulerAngles = container.transform.localEulerAngles;

		bodyCtrl.transform.parent = container.transform;

		birdControls.birds.Add (this);

	}


}
