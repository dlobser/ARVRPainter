using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Scale : ON_FSMTrigger_Base {

		public Vector3 scale;
		private Vector3 initialScale;
		private Vector3 privateScale;
		private Vector3 init;
		public bool randomize = false;
		public bool relativeScale;

//		public GameObject objectToModify;


		void OnEnable(){
			base.OnEnable ();
			if (val.objectToModify == null)
				val.objectToModify = this.gameObject;
			initialScale = scale;
			privateScale = scale;
			init = val.objectToModify.transform.localScale;
		}

		void OnDisable(){
			base.OnDisable ();
		}

		public override void Ping() {
			counter = 0;
			GetInitialScale ();
			base.Ping ();

		}
		
		public override void Ping(float t){
			if (!triggered)
				GetInitialScale ();
			base.Ping (t);
		}

		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_Scale> ();
			base.KillSame ();
		}

		public override void Reset(){
			base.Reset ();
			Idle ();
			counter = 0;
		}

		void Idle(){
			GetInitialScale ();
		}

		void GetInitialScale(){
			init = relativeScale ? val.objectToModify.transform.localScale : val.objectToModify.transform.localScale;
			privateScale = relativeScale ? val.objectToModify.transform.localScale + initialScale : initialScale;

			if (randomize) {
				float lerp = Random.value;
				privateScale = Vector3.Lerp (init, privateScale, lerp);
			}
		}

		public override void Modify(float t){
			val.objectToModify.transform.localScale = Vector3.Lerp (init,  privateScale, t);
		}
	}
}
