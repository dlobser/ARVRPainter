using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_PingManager : ON_FSMTrigger {

		public int whichState = -1;
		public int[] whichStates;

		public override void Ping() {

			if (val.triggerType == pubVal.triggerParams.OnPing){
				print ("OnPong");
				if(val.manualTriggers.Length<1)
					interactable.Trigger (blockOnTrigger);
				ManualTrigger ();
			}
			
			if (whichStates.Length < 1) {
				whichStates = new int[val.objectsToModify.Length];
				foreach (int i in whichStates)
					whichStates [i] = whichState;
			}

			float count = 0;

			if (whichState < 0) 
				val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().Trigger ();// Invoke ("Trigger", .1f);
			else 
				val.objectToModify.GetComponent<ON_FSM_Manager_Events> ().Trigger (whichState);// Invoke ("Trigger", .1f);
			
			for (int i = 0; i < val.objectsToModify.Length; i++) {
				if(whichStates[i] < 0)
					val.objectsToModify [i].GetComponent<ON_FSM_Manager_Events> ().Trigger ();//Invoke ("Trigger", .1f);
				else
					val.objectsToModify [i].GetComponent<ON_FSM_Manager_Events> ().Trigger (whichStates[i]);//Invoke ("Trigger", .1f);
				
			}

			if (val.triggerType == pubVal.triggerParams.OnPong || val.triggerType == pubVal.triggerParams.OnEnd) {
				print ("OnPong");
				if(val.manualTriggers.Length<1)
					interactable.Trigger (blockOnTrigger);
				ManualTrigger ();
			}


		}
	}
}
