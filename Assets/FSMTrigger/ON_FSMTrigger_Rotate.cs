using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Rotate : ON_FSMTrigger_Base {

	    public Vector3 rotate;
		private Vector3 initialRot;
		private Vector3 privateRot;
		private Vector3 init;
		public bool randomize = false;
//		public bool smoothMove = false;
		public bool localRotation = false;
		public bool relativeRotation = false;


//		float counter = 0;
//		bool triggered = false;

		void OnEnable(){
			base.OnEnable ();
			if (val.objectToModify == null)
				val.objectToModify = this.gameObject;
//			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;
			initialRot = rotate;
			privateRot = rotate;

		}

		void OnDisable(){
			base.OnDisable ();
//			GetComponent<ON_InteractableEvents> ().OnIdle -= Idle;
		}

	    public override void Ping() {
			
			init = localRotation ?  val.objectToModify.transform.localEulerAngles : val.objectToModify.transform.eulerAngles;
			initialRot = rotate;
			privateRot = rotate;
            GetPrivateRot ();
			counter = 0;
			GetInitialRotation ();
			base.Ping ();

	    }

		public override void Ping(float t){
			if (!triggered)
				GetInitialRotation ();
			base.Ping (t);
		}

		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_Rotate> ();
			base.KillSame ();
		}

		public override void Reset(){
			base.Reset ();
			Idle ();
			counter = 0;
		}

		void GetPrivateRot(){
			if(localRotation)
				privateRot = relativeRotation ? val.objectToModify.transform.localEulerAngles + initialRot : privateRot;
			else
				privateRot = relativeRotation ? val.objectToModify.transform.eulerAngles + initialRot : privateRot;	
		}

		void Idle(){
			GetPrivateRot ();
			GetInitialRotation ();
		}

		void GetInitialRotation(){
			if (localRotation)
				init = relativeRotation ? val.objectToModify.transform.localEulerAngles : init;
			else
				init = relativeRotation ? val.objectToModify.transform.eulerAngles : init;

			if (randomize) {

				privateRot = rotate;
				privateRot.Scale (Random.insideUnitSphere);

				if(localRotation)
					privateRot+=val.objectToModify.transform.localPosition ;
				else
					privateRot+=val.objectToModify.transform.position ;

			}
		}

		public override void Modify(float t){
			if (localRotation)
				val.objectToModify.transform.localEulerAngles = new Vector3 (Mathf.LerpAngle (init.x, privateRot.x, t), Mathf.LerpAngle (init.y, privateRot.y, t), Mathf.LerpAngle (init.z, privateRot.z, t));
			else
				val.objectToModify.transform.eulerAngles = new Vector3 (Mathf.LerpAngle (init.x, privateRot.x, t), Mathf.LerpAngle (init.y, privateRot.y, t), Mathf.LerpAngle (init.z, privateRot.z, t));


		}
	}
}
