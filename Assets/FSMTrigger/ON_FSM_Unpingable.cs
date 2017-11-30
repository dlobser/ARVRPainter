using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSM_Unpingable : ON_FSMTrigger {

		public override void Ping(){
			val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().thisState.gameObject.GetComponent<ON_FSM>().pingable = false;
		}
		
	}
}