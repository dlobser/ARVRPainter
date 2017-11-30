using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Vector3 : ON_FSMTrigger_Base {

//	    public Vector3 to;
//		public Vector3 from;
//		private Vector3 initialVec;
//		private Vector3 vecToLerpToward;
//		private Vector3 init;
//		public bool randomize = false;
//		public bool local = false;
//		public bool relative = false;
//		public bool useFrom;
//
//
//	    public override void Ping() {
//			
//			init = localPosition ?  val.objectToModify.transform.localPosition : val.objectToModify.transform.position;
//			initialVec = vector;
//			vecToLerpToward = vector;
//            GetPrivateMove ();
//			counter = 0;
//			GetInitialPosition ();
//			base.Ping ();
//
//	    }
//
//		public override void Ping(float t){
//			if (!triggered)
//				GetInitialPosition ();
//			base.Ping (t);
//		}
//
//		public override void KillSame(){
//			same = GetComponents<ON_FSMTrigger_Move> ();
//			base.KillSame ();
//		}
//
//		public override void Reset(){
//			base.Reset ();
//			Idle ();
//			counter = 0;
//		}
//
//		void GetPrivateMove(){
//			if(localPosition)
//				privateMove = relativePosition ? val.objectToModify.transform.localPosition + initialMove : privateMove;
//			else
//				privateMove = relativePosition ? val.objectToModify.transform.position + initialMove : privateMove;
//		}
//
//		void Idle(){
//			GetPrivateMove ();
//			GetInitialPosition ();
//		}
//
//		public Vector3 GetInitialVector(Vector3 vec){
//
//			init = relative ? vec : init;
//
//			if (randomize) {
//
//				vecToLerpToward = to;
//				vecToLerpToward.Scale (Random.insideUnitSphere);
//
//				if(local)
//					vecToLerpToward+=val.objectToModify.transform.localPosition ;
//				else
//					privateMove+=val.objectToModify.transform.position ;
//
//			}
//		}
//
//		public override void Modify(float t){
////			if (smoothMove)
////				t = Mathf.SmoothStep (0, 1, t);
//			if (localPosition) 
//				val.objectToModify.transform.localPosition = Vector3.Lerp (init, privateMove , t);
//			else
//				val.objectToModify.transform.position = Vector3.Lerp (init,  privateMove, t);
//			
//		}
	}
}
