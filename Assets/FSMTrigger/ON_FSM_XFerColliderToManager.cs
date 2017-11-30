using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ON{
	public class ON_FSM_XFerColliderToManager : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler{

		public ON_FSM_Manager_Events manager;
		bool pinging = false;


		public void OnPointerEnter(PointerEventData p){
			pinging = true;
		}
		public void OnPointerExit(PointerEventData p){
			pinging = false;
		}
		public void OnPointerDown(PointerEventData p){
			manager.Trigger ();
		}
		public void OnPointerUp(PointerEventData p){
			manager.UnTrigger ();
		}

		void Update(){
			if (pinging) {
				manager.Ping ();
			}
		}

	}
}