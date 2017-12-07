using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCubeSize : MonoBehaviour {

	public Vector3 scale;
	public Vector3 rotate;
	public Vector3 position;

	public Transform[] sides;
	public Transform[] cameras;

	public int state;

	float mouseRotateY;
	Vector3 mouseScale;
	Vector3 mousePos;

	public float rotateSpeed = 1;
	public float translateSpeed = 1;
	public float scaleSpeed = 1;
	// Use this for initialization
	void Start () {
		mouseScale = scale;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Alpha0)) {
			state=0;
		}
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			state=1;
		}
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			state=2;
		}
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			state=3;
		}

		if (Input.GetKeyUp (KeyCode.Alpha4)) {
			state=4;
		}
		/*
		 * if (Input.GetKey (KeyCode.Alpha7)) {
			Transform g = Camera.main.transform;
			g.localEulerAngles = new Vector3 (g.localEulerAngles.x + Input.GetAxis ("Mouse Y") * speed, g.localEulerAngles.y + Input.GetAxis ("Mouse X") * speed, g.localEulerAngles.z);
		}
		if (Input.GetKey (KeyCode.Alpha8)) {
			Camera.main.fieldOfView = Camera.main.fieldOfView + Input.GetAxis ("Mouse Y") * speed;
		}
		if (Input.GetKey (KeyCode.Alpha9)) {
			Transform g = Camera.main.transform;
			g.localPosition = new Vector3 (g.localPosition.x, g.localPosition.y + Input.GetAxis ("Mouse Y") * speed*.1f, g.localPosition.z);
		}
		*/

		if (Input.GetKeyUp (KeyCode.S)) {
			PlayerPrefs.SetFloat ("parentPosX", this.transform.parent.localPosition.x);
			PlayerPrefs.SetFloat ("parentPosY", this.transform.parent.localPosition.y);
			PlayerPrefs.SetFloat ("parentPosZ", this.transform.parent.localPosition.z);
			PlayerPrefs.SetFloat ("parentRotY", this.transform.parent.localEulerAngles.y);

			PlayerPrefs.SetFloat ("camPosY", Camera.main.transform.localPosition.y);
			PlayerPrefs.SetFloat ("camFOV", Camera.main.fieldOfView);
			PlayerPrefs.SetFloat ("camRotX", Camera.main.transform.localEulerAngles.x);
			PlayerPrefs.SetFloat ("camRotY", Camera.main.transform.localEulerAngles.y);


			PlayerPrefs.SetFloat ("scaleX", scale.x);
			PlayerPrefs.SetFloat ("scaleY", scale.y);
			PlayerPrefs.SetFloat ("scaleZ", scale.z);
			PlayerPrefs.Save ();
		}
		if (Input.GetKeyUp (KeyCode.L)) {
			this.transform.parent.localPosition = new Vector3 (
				PlayerPrefs.GetFloat ("parentPosX"),
				PlayerPrefs.GetFloat ("parentPosY"),
				PlayerPrefs.GetFloat ("parentPosZ"));
			this.transform.parent.localEulerAngles = new Vector3 (0, PlayerPrefs.GetFloat ("parentRotY"), 0);

			Camera.main.transform.localPosition = new Vector3 (
				Camera.main.transform.localPosition.x,
				PlayerPrefs.GetFloat ("camPosY"),
				Camera.main.transform.localPosition.z
			);

			Camera.main.fieldOfView = PlayerPrefs.GetFloat ("camFOV");

			Camera.main.transform.localEulerAngles = new Vector3 (
				PlayerPrefs.GetFloat ("camRotX"),
				PlayerPrefs.GetFloat ("camPosY"),
				Camera.main.transform.localEulerAngles.z
			);


			scale.Set (PlayerPrefs.GetFloat ("scaleX"), PlayerPrefs.GetFloat ("scaleY"), PlayerPrefs.GetFloat ("scaleZ"));
			ApplyVectors();
		}

		if (state == 3) {
			Vector3 pos = this.transform.parent.localPosition;
			if (Input.GetKey (KeyCode.LeftShift))
				mousePos.Set (pos.x, pos.y + Input.GetAxis ("Mouse Y") * translateSpeed, pos.z);
			else
				mousePos.Set (pos.x + Input.GetAxis ("Mouse X") * translateSpeed, pos.y, pos.z + Input.GetAxis ("Mouse Y") * translateSpeed);
			
			this.transform.parent.localPosition = mousePos;
		}
		if (state == 1) {
			mouseRotateY += Input.GetAxis("Mouse X") * rotateSpeed;
//			float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
			this.transform.parent.localEulerAngles = new Vector3 (0,mouseRotateY,0);

		}
		if (state == 2) {
			if (Input.GetKey (KeyCode.LeftShift))
				mouseScale.Set (mouseScale.x , mouseScale.y + Input.GetAxis ("Mouse Y") * scaleSpeed, mouseScale.z);
			else
				mouseScale.Set (mouseScale.x + Input.GetAxis ("Mouse X") * scaleSpeed, mouseScale.y, mouseScale.z + Input.GetAxis ("Mouse Y") * scaleSpeed);
			scale = mouseScale;
			ApplyVectors();
			SetPosition (false);
		}


		if (state == 0) {
			ApplyVectors ();
			SetPosition (true);
		}

	}

	void ApplyVectors(){
		sides [0].transform.localPosition = new Vector3 (0, scale.y * .5f, scale.z * .5f);
		sides [0].transform.localScale = new Vector3 (scale.x, scale.y, 1);

		sides [1].transform.localPosition = new Vector3 (scale.x * .5f, scale.y * .5f, 0);
		sides [1].transform.localEulerAngles = new Vector3 (0, 90, 0);
		sides [1].transform.localScale = new Vector3 (scale.z, scale.y, 1);

		sides [2].transform.localPosition = new Vector3 (0, scale.y * .5f, -scale.z * .5f);
		sides [2].transform.localEulerAngles = new Vector3 (0, 180, 0);
		sides [2].transform.localScale = new Vector3 (scale.x, scale.y, 1);

		sides [3].transform.localPosition = new Vector3 (-scale.x * .5f, scale.y * .5f, 0);
		sides [3].transform.localEulerAngles = new Vector3 (0, -90, 0);
		sides [3].transform.localScale = new Vector3 (scale.z, scale.y, 1);


		sides [4].transform.localPosition = new Vector3 (0, scale.y, 0);
		sides [4].transform.localEulerAngles = new Vector3 (-90, 0, 0);
		sides [4].transform.localScale = new Vector3 (scale.x, scale.z, 1);


		sides [5].transform.localEulerAngles = new Vector3 (90, 0, 0);
		sides [5].transform.localScale = new Vector3 (scale.x, scale.z, 1);

		for (int i = 0; i < cameras.Length; i++) {
			cameras [i].localPosition = new Vector3 (0, scale.y * .5f, 0);

		}
		cameras [0].GetComponent<Camera> ().orthographicSize = scale.y * .5f;
		cameras [1].GetComponent<Camera> ().orthographicSize = scale.y * .5f;
		cameras [2].GetComponent<Camera> ().orthographicSize = scale.y * .5f;
		cameras [3].GetComponent<Camera> ().orthographicSize = scale.y * .5f;
		cameras [4].GetComponent<Camera> ().orthographicSize = scale.z * .5f;
		cameras [5].GetComponent<Camera> ().orthographicSize = scale.z * .5f;

		//cameras [0].GetComponent<Camera> ().rect = new Rect (0, (scale.x - scale.y) / scale.x, 1, 1);
		//cameras [1].GetComponent<Camera> ().rect = new Rect (0, (scale.z - scale.y) / scale.z, 1, 1);
		//cameras [2].GetComponent<Camera> ().rect = new Rect (0, (scale.x - scale.y) / scale.x, 1, 1);
		//cameras [3].GetComponent<Camera> ().rect = new Rect (0, (scale.z - scale.y) / scale.z, 1, 1);
		//cameras [4].GetComponent<Camera> ().rect = new Rect (0, (scale.x - scale.z) / scale.x, 1, 1);
		//cameras [5].GetComponent<Camera> ().rect = new Rect (0, (scale.x - scale.z) / scale.x, 1, 1);

        cameras[0].GetComponent<Camera>().aspect = (scale.x / scale.y);
        cameras[1].GetComponent<Camera>().aspect = (scale.z / scale.y);
        cameras[2].GetComponent<Camera>().aspect = (scale.x / scale.y);
        cameras[3].GetComponent<Camera>().aspect = (scale.z / scale.y);
        cameras[4].GetComponent<Camera>().aspect = (scale.x / scale.z);
        cameras[5].GetComponent<Camera>().aspect = (scale.x / scale.z);
                                                                                

        cameras [1].localEulerAngles = new Vector3 (0, 90, 0);
		cameras [2].localEulerAngles = new Vector3 (0, 180, 0);
		cameras [3].localEulerAngles = new Vector3 (0, -90, 0);
		cameras [4].localEulerAngles = new Vector3 (-90, 0, 0);
		cameras [5].localEulerAngles = new Vector3 (90, 0, 0);


	}

	void SetPosition(bool doParent){
		this.transform.localPosition = new Vector3 (-scale.x * .5f, 0, -scale.z * .5f);
		if(doParent)
			this.transform.parent.localPosition = new Vector3 (scale.x * .5f, 0, scale.z * .5f);
	}
}
