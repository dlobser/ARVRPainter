using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ON{
	public class ON_FSM: MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler{ 

		enum STATE {IDLE, ENTER, HOVER, EXIT, ENDHOVER};
		STATE state;
		
		public delegate void EnterAction();
		public event EnterAction OnEnter;

		public delegate void ExitAction();
		public event ExitAction OnExit;

		public delegate void HoverAction(float t);
		public event HoverAction OnHover;

		public delegate void IdleAction();
		public event IdleAction OnIdle;

		public delegate void TriggerAction();
		public event TriggerAction OnTrigger;

		public delegate void EndHoverAction();
		public event EndHoverAction EndHover;
//
		public delegate void KillSameAction();
		public event KillSameAction KillSame;

		public bool debug;

		public float counter { get; set; }
		public float timeToTrigger = 1;

		[Tooltip("Speed that OnHover will return to zero")]
		public float returnMultiplier = 1;

		public bool pinged;// { get; set; }
		public bool over { get; set; }
		public bool triggered;// { get; set; }

		public int triggerCounter { get; set; }

		[Tooltip("How many times will it trigger")]
		public int maxTriggers;

		public bool pingable { get; set; }
		public int blockCounter { get; set; }

		public bool neverTrigger;
		public bool neverBlockExit;
		public bool neverBlockEnter;
		public bool neverBlockHover;
		public bool dontRetriggerUntilExit;
		bool triggerable = true;
		public bool triggerOnEnable;

		int exitCounter =0;

		ON_FSMTrigger[] triggers;

		string blockingComponent;
		[Tooltip("Use Unity's event system directly instead of via ON_FSM_Manager_Events")]
		public bool localManagement = true;
		bool pinging;

		public void OnPointerEnter(PointerEventData p){
			if(localManagement)
				pinging = true;
		}
		public void OnPointerExit(PointerEventData p){
			if(localManagement)
				pinging = false;
		}
		public void OnPointerDown(PointerEventData p){
			if(localManagement)
				Trigger ();
		}
		public void OnPointerUp(PointerEventData p){
			if(localManagement)
				UnTrigger ();
		}

		public void Ping(){
			if(pingable)
				pinged = true;
			over = true;
		}

		public void Trigger(){
			if(triggerable)
				triggered = true;
			if (dontRetriggerUntilExit)
				triggerable = false;
		}

		public void Trigger(bool block){
			triggered = true;
			pingable = !block;
		}

		void OnEnable(){
			pingable = true;
			state = STATE.IDLE;
			triggers = GetComponents<ON_FSMTrigger> ();

		}
		void Start(){
			if (triggerOnEnable)
				Trigger ();
		}

		public void UnTrigger(){
			triggered = false;
			triggerable = true;
		}
			
		void Update () {
			if (pinging && localManagement)
				Ping ();
			
			if (debug) {
				Debug.Log ("blocked:  " + CheckBlocked () + " , " + blockingComponent);
				Debug.Log ("pinged:  " + pinged);
				Debug.Log ("pingable:  " + pingable);
			}
			switch (state) 
			{
			case STATE.IDLE:
				{
					if (pinged) {
						state = STATE.ENTER;
						HandleEnter ();
					}
					if (!CheckBlocked () && !over) {
						pingable = true;
					}
					break;
				}
			case STATE.ENTER:
				{
					if (pinged) {
						state = STATE.HOVER;
					} 
					else if (!pinged && !over) {
						state = STATE.EXIT;
						HandleExit ();
					}
					break;
				}
			case STATE.HOVER:
				{
					if (!pinged && !over) {
						state = STATE.EXIT;
						HandleExit ();
					} 
					break;
				}
			case STATE.ENDHOVER:
				{
					state = STATE.IDLE;
					if (EndHover != null) {
						EndHover ();
					}
					break;
				}
			case STATE.EXIT:
				{
					if (dontRetriggerUntilExit)
						triggerable = true;
					HandleReset ();
					state = STATE.IDLE;
					if (!pingable) {
						pingable = true;
					}
					break;
				}
			default:
				{
					break;
				}
			}

			if(debug)
				Debug.Log ("STATE:   " + state);
			
			if (triggered) {
				if (debug)
					Debug.Log ("TRIGGERED!");
				HandleTrigger ();
				triggered = false;
			}

			if (pinged) {
				if(!CheckBlocked() || neverBlockHover)
					HandleHover ();
			}
//			Debug.Log (counter);

			if (
				//state != STATE.HOVER && state!= STATE.ENDHOVER && 
				!pinged) {
				if (counter > 0 && OnHover != null) {
					if (!CheckBlocked () || neverBlockHover) {
						counter -= Time.deltaTime * returnMultiplier;
						counter = counter < 0 ? 0 : counter;
						OnHover (counter / timeToTrigger);
						if (counter == 0) {
							state = STATE.ENDHOVER;
						}
					}
				} 
			}

			pinged = false;
			over = false;
		}

		public bool CheckBlocked(){
			bool blocking = false;
			for (int i = 0; i < triggers.Length; i++) {
				if (triggers [i].triggered && triggers [i].blocking) {
					blocking = true;
				}
			}
			return blocking;
		}

		public void HandleEnter(){
			if (OnEnter != null ) {
				if (neverBlockEnter && CheckBlocked()) {
					if (KillSame != null)
						KillSame ();
					OnEnter ();
				}
				else if(!CheckBlocked())
					OnEnter ();
			}
		}

		public void HandleExit(){
			if (OnExit != null ) {
				if (neverBlockExit && CheckBlocked()) {
					if (KillSame != null)
						KillSame ();
					OnExit ();
				}
				else if(!CheckBlocked())
					OnExit ();

			}
		}

		public void HandleHover(){
			if (counter <= timeToTrigger) {
				if (OnHover != null) {
					counter += Time.deltaTime;
					OnHover (counter / timeToTrigger);
					if (counter >= timeToTrigger) {
						if(!neverTrigger) {
							Trigger ();
						}
					}
				}
			} 
		}

		public void HandleTrigger(){
			if (OnTrigger != null && triggerCounter < maxTriggers || OnTrigger != null  && maxTriggers==0) {
				if (!CheckBlocked ()) {
					OnTrigger ();
					triggerCounter++;
				}
			}
		}

		public void HandleReset(){

		}

	}
}
