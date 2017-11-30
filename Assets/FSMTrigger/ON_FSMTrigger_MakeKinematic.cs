using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_MakeKinematic : ON_FSMTrigger{
		
//		public float force;
//		public ON_MouseInteraction mouse;
		public bool makeAllSiblingsKinematic;
		public bool isKinematic;
//		public GameObject objectToModify;
		Rigidbody rigid;

		void Start(){
			SetupObject ();
			if (val.objectToModify == null)
				val.objectToModify = this.gameObject;
			rigid = val.objectToModify.GetComponent<Rigidbody> ();
		}

		public override void Ping() {
			base.Ping ();
//			if (makeAllSiblingsKinematic) { 
//				for (int i = 0; i < val.objectToModify.transform.parent.transform.childCount; i++) {
//					val.objectToModify.transform.parent.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
//				}
//			}
//			else {
			val.objectToModify.GetComponent<Rigidbody> ().isKinematic = isKinematic;

//			}
//			if (mouse != null) {
//				if (!mouse.hitPosition.Equals (Vector3.zero)) {
//					Vector3 direction = this.transform.position - mouse.hitPosition;
//					rigid.AddForce (direction * force);
//				} else
//					Debug.Log ("NOT");
//			}
//			else
//				rigid.AddForce (Vector3.up * force);
		}

	}
}