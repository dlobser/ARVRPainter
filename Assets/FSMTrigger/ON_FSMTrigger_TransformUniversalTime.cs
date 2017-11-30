using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_TransformUniversalTime : ON_FSMTrigger_Base {


		public TransformUniversal xform;

		public float timeLow=1;
		public float timeHigh=1;


		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_TransformUniversalTime> ();
			base.KillSame ();
		}

		public override void Modify(float t){
			if (xform==null && val.objectToModify.GetComponent<TransformUniversal> ()!=null)
				xform = val.objectToModify.GetComponent<TransformUniversal> ();
			if(xform!=null)
				xform.globalTimeScale = Mathf.Lerp (timeLow, timeHigh, t);
			
		}
	}
}
