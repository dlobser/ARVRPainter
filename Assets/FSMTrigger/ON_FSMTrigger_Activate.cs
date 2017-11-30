
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Activate : ON_FSMTrigger{

		public bool findInHierarchy;
		public string[] Activate;
		public string[] Deactivate;
		public GameObject[] activate;
		public GameObject[] deactivate;
		public bool swapActiveDeactive;
		public bool populateFromStrings;

		void Start(){
			SetupObject ();
			if(populateFromStrings)
				FillGOArray ();
		}

		void FillGOArray(){
			activate = new GameObject[Activate.Length];
			deactivate = new GameObject[Deactivate.Length];
//			Debug.Log (val.objectToModify);
			if (findInHierarchy) {
				for (int i = 0; i < activate.Length; i++) {
					activate [i] = FindInHierarchy (val.objectToModify.transform, Activate [i]).gameObject;// val.objectToModify.GetComponent<ON_GameObjectSelector> ().Find (Activate [i]);
				}
				for (int i = 0; i < deactivate.Length; i++) {
					deactivate [i] = FindInHierarchy (val.objectToModify.transform, Deactivate [i]).gameObject;//val.objectToModify.GetComponent<ON_GameObjectSelector> ().Find (Deactivate [i]);
				}
			} else {
				for (int i = 0; i < activate.Length; i++) {
					activate [i] = GameObject.Find (Activate [i]);
				}
				for (int i = 0; i < deactivate.Length; i++) {
					deactivate [i] =  GameObject.Find (Deactivate [i]);
				}
			}
		}

		public override void Ping() {
//			if (useGameObjectSelector) {
//				activate = val.objectToModify.GetComponent<ON_GameObjectSelector> ().A;
//				deactivate = val.objectToModify.GetComponent<ON_GameObjectSelector> ().B;
//			}
			if (activate.Length>0 && activate [0] == null || deactivate.Length>0 && deactivate [0] == null)
				if(populateFromStrings)
						FillGOArray ();
			for (int i = 0; i < activate.Length; i++) {
				activate [i].SetActive (!swapActiveDeactive);
			}
			for (int i = 0; i < deactivate.Length; i++) {
				deactivate [i].SetActive (swapActiveDeactive);
			}
		}

	}
}