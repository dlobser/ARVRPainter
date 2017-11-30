
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ON{
	public class ON_FSMTrigger_UIImageRaycastable : ON_FSMTrigger{

		public bool raycast;

		public override void Ping() {
			val.objectToModify.GetComponent<Image> ().raycastTarget = raycast;
			for (int i = 0; i < val.objectsToModify.Length; i++) {
				val.objectsToModify[i].GetComponent<Image> ().raycastTarget = raycast;
			}
		}

	}
}