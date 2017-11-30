using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ON{
		public class ON_FSM_Manager_Events : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler{

		public GameObject[] states;
		public GameObject thisState { get; set; }
		public int which { get; set; }
		public bool pingableOnStart;
		bool pinging = false;
		public GameObject stateContainer;

		void Start () {
			if (thisState != null)
				Destroy (thisState);
			ChangeState (0);
		}

		public void OnPointerEnter(PointerEventData p){
			pinging = true;
		}
		public void OnPointerExit(PointerEventData p){
			pinging = false;
		}
		public void OnPointerDown(PointerEventData p){
			Trigger ();
		}
		public void OnPointerUp(PointerEventData p){
			UnTrigger ();
		}

		public void Ping(){
			thisState.GetComponent<ON_FSM>().Ping();
		}

		public void Trigger(){
			thisState.GetComponent<ON_FSM>().Trigger();
		}

		public void UnTrigger(){
			thisState.GetComponent<ON_FSM>().UnTrigger();
		}

		public void Ping(int w){
			ChangeState (w);
			thisState.GetComponent<ON_FSM>().Ping();
		}

		public void Trigger(int w){
			ChangeState (w);
			thisState.GetComponent<ON_FSM>().Trigger();
		}

		public void UnTrigger(int w){
			ChangeState (w);
			thisState.GetComponent<ON_FSM>().UnTrigger();
		}


		void Update(){
			if (pinging) {
				Ping ();
			}
		}

		public void ChangeState(int w){
			which = w;

			if(thisState!=null)
				Destroy(thisState);
			
			thisState = Instantiate(states[w]);

			if (stateContainer != null)
				thisState.transform.parent = stateContainer.transform;
			
			thisState.GetComponent<ON_FSM> ().pingable = pingableOnStart;
			thisState.name = this.gameObject.name + "_" + thisState.gameObject.name;
			ON_FSMTrigger[] triggers = thisState.GetComponents<ON_FSMTrigger> ();

//			Debug.Log (w + " , " + triggers.Length);

			if (w < states.Length) {
				for (int i = 0; i < triggers.Length; i++) {
					if (triggers [i].val.objectToModify == null || triggers [i].val.objectToModify == triggers [i].gameObject) {
						triggers [i].val.objectToModify = this.gameObject;

					}
				}
			} else
				Debug.LogWarning ("trigger on manager is null");

		}

	}
}