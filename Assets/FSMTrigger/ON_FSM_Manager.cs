using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSM_Manager : MonoBehaviour {

		public GameObject[] states;
		public GameObject thisState { get; set; }
		int which;
		public bool pingableOnStart;

		// Use this for initialization
		void Start () {
			ChangeState (0);
		}

		public void Ping(){
			for (int i = 0; i < states.Length; i++) {
				thisState.GetComponent<ON_FSM>().Ping();
			}
		}

		public void Trigger(){
			for (int i = 0; i < states.Length; i++) {
				thisState.GetComponent<ON_FSM>().Trigger();
			}
		}


		void Update(){
			if (Input.GetKeyUp (KeyCode.A)) {
				which++;
				if (which > states.Length - 1)
					which = 0;
				ChangeState (which);

			}
		}

		public void ChangeState(int w){
			if(thisState!=null)
				Destroy(thisState);
			thisState = Instantiate(states[w]);
			thisState.GetComponent<ON_FSM> ().pingable = pingableOnStart;
			ON_FSMTrigger[] triggers = thisState.GetComponents<ON_FSMTrigger> ();
			for (int i = 0; i < triggers.Length; i++) {
				if (triggers [i].val.objectToModify == null || triggers [i].val.objectToModify == triggers [i].gameObject) {
					triggers [i].val.objectToModify = this.gameObject;

				}
			}
		}

	}
}