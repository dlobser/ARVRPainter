using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_MoveRect : ON_FSMTrigger_Base {

	    public Vector3 move;
		private Vector3 initialMove;
		private Vector3 privateMove;
		private Vector3 init;
//		public bool randomize = false;
//		public bool localPosition = false;
		public bool relativePosition = false;


//		float counter = 0;
//		bool triggered = false;

		void OnEnable(){
			base.OnEnable ();
//			GetComponent<ON_InteractableEvents> ().OnIdle += Idle;

			if (val.objectToModify == null && this.gameObject.GetComponent<RectTransform>()!=null)
				val.objectToModify = this.gameObject;
			
		}

		void OnDisable(){
			base.OnDisable ();
//			GetComponent<ON_InteractableEvents> ().OnIdle -= Idle;
		}

	    public override void Ping() {
			
			init = val.objectToModify.GetComponent<RectTransform> ().anchoredPosition3D;
			initialMove = move;
            privateMove = move;
//            GetPrivateMove ();
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
			privateMove = val.objectToModify.GetComponent<RectTransform>().anchoredPosition3D;

		}

		void Idle(){
			GetPrivateMove ();
			GetInitialPosition ();
		}

		void GetInitialPosition(){
			init = val.objectToModify.GetComponent<RectTransform>().anchoredPosition3D;

		}

		public override void Modify(float t){
			val.objectToModify.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp (init,  privateMove, t);
		}
	}
}
