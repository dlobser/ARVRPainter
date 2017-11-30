using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Move : ON_FSMTrigger_Base {

	    public Vector3 move;
		private Vector3 initialMove;
		private Vector3 privateMove;
		private Vector3 init;
		public bool randomize = false;
		public bool localPosition = false;
		public bool relativePosition = false;


//		float counter = 0;
//		bool triggered = false;

		void OnEnable(){
			base.OnEnable ();
//			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;

			if (val.objectToModify == null)
				val.objectToModify = this.gameObject;
			

		}

		void OnDisable(){
			base.OnDisable ();
//			GetComponent<ON_InteractableEvents> ().OnIdle -= Idle;
		}

	    public override void Ping() {
			
			init = localPosition ?  val.objectToModify.transform.localPosition : val.objectToModify.transform.position;
			initialMove = move;
            privateMove = move;
            GetPrivateMove ();
			counter = 0;
			GetInitialPosition ();
			base.Ping ();

	    }

		public override void Ping(float t){
			if (!triggered)
				GetInitialPosition ();
			base.Ping (t);
		}

		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_Move> ();
			base.KillSame ();
		}

		public override void Reset(){
			base.Reset ();
			Idle ();
			counter = 0;
		}

		void GetPrivateMove(){
			if(localPosition)
				privateMove = relativePosition ? val.objectToModify.transform.localPosition + initialMove : privateMove;
			else
				privateMove = relativePosition ? val.objectToModify.transform.position + initialMove : privateMove;
		}

		void Idle(){
			GetPrivateMove ();
			GetInitialPosition ();
		}

		void GetInitialPosition(){
			if (localPosition)
				init = relativePosition ? val.objectToModify.transform.localPosition : init;
			else
				init = relativePosition ? val.objectToModify.transform.position : init;

			if (randomize) {

				privateMove = move;
				privateMove.Scale (Random.insideUnitSphere);

				if(localPosition)
					privateMove+=val.objectToModify.transform.localPosition ;
				else
					privateMove+=val.objectToModify.transform.position ;

			}
		}

		public override void Modify(float t){
//			if (smoothMove)
//				t = Mathf.SmoothStep (0, 1, t);
			if (localPosition) 
				val.objectToModify.transform.localPosition = Vector3.Lerp (init, privateMove , t);
			else
				val.objectToModify.transform.position = Vector3.Lerp (init,  privateMove, t);
			
		}
	}
}
