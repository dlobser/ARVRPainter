
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_SwitchState : ON_FSMTrigger{

		public int switchToState;
		public bool increment;
		int currentState;

		public override void Ping() {
			currentState = val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().which;
			if (increment) {
				if(debug)
					Debug.Log ("Object To Modify A: " + val.objectToModify + " , " + currentState);
				currentState+=1;
				if (currentState > val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().states.Length - 1)
					currentState = 0;
				
			} else
				currentState = switchToState;
			if(debug)
				Debug.Log ("Object To Modify: " + val.objectToModify + " , " + currentState);
			val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().ChangeState (currentState);	
		}

	}
}