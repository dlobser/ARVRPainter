using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//using UnityEditor.AnimatedValues;


namespace ON{
	
	[System.Serializable]
	public class pubVal{
		public bool debug;

		public enum parameters {OnEnter,OnExit,OnHover,OnTrigger,EndHover,Manual};
		public parameters type;

		public ON_FSMTrigger[] manualTriggers;

		public enum triggerParams  {Never, OnEnd, OnPing, OnPong};
		public triggerParams triggerType;

		public bool blocking;
		public bool killOthersOnStart;
		public bool blockOnTrigger;

		public int maxTriggers;
		public int triggerCounter { get; set; }

		public bool findObjectInHierarchy;
		public string objectName;
		public GameObject objectToModify;

		public bool populateFromStrings;
		public string[] objectNames;
		public GameObject[] objectsToModify;
	}

	public class ON_FSMTrigger: MonoBehaviour {
		public string notes;
		public pubVal val = new pubVal();


		public ON_FSM interactable { get; set; } 

//		public pubVal.triggerParams triggerParams;
//		public pubVal.parameters type;
//		public enum parameters {get pubVal.parameters; set;}

//		public enum triggerParams {get{return pubVal.triggerParams;}}
//		public pubVal.triggerParams triggerType;// = val.triggerType;

		public bool debug { get; set; }
	    public bool triggered { get; set; }
		public bool hovering { get; set; }
		public bool blocking {get;set;}
		public bool killOthersOnStart {get;set;}
		public bool blockOnTrigger { get; set; }

	    public virtual void Ping() {
			triggered = true;
		}

		public virtual void Ping(float t) { }

		public virtual void ManualTrigger(){
			for (int i = 0; i < val.manualTriggers.Length; i++) {
				if (val.manualTriggers [i] != null)
					val.manualTriggers [i].Ping ();
				else
					Debug.LogWarning ("Missing Trigger: " + this.gameObject);
			}
//			Debug.Log ("TRIGGGGEEEERRRRR " + Time.timeSinceLevelLoad);
		}
		public void print(string s){
			if(val.debug)
				Debug.Log (this.gameObject.name + "  :  " + this.GetType().ToString() + "  :  " + s);
		}

//		public virtual void Animate(){}

		public virtual void Reset() {
			if (val.triggerType == pubVal.triggerParams.OnEnd) {
				if(val.manualTriggers.Length<1)
					interactable.Trigger (blockOnTrigger);
				ManualTrigger ();
			}
			triggered = false;
		}

		public virtual void KillSame(){
		}

		public virtual void Kill(){
		}

		public Transform getChildGameObject(Transform aParent, string withName)
		{
			var result = aParent.transform.Find(withName);
			if (result != null)
				return result;
			foreach(Transform child in aParent)
			{
				
				result = getChildGameObject(child, withName);
				if (result != null)
					return result;
			}
			return null;
		}

		Transform getParent(Transform fromGameObject, string name){
			if (fromGameObject.transform.parent != null) {
				if (fromGameObject.transform.parent.gameObject.name == name) {
					return fromGameObject.transform.parent;
				} else
					return getParent (fromGameObject.transform.parent, name);
			}
            else
                return fromGameObject;
		}

		public Transform FindInHierarchy(Transform fromGameObject, string name){
			Transform thing = null;
			if (GetComponent<SceneDictionary> () != null)
				thing = GetComponent<SceneDictionary> ().dictionary [name].transform;
			if(thing==null || thing.gameObject.name!=val.objectName)
				thing =  getChildGameObject (fromGameObject, name);
			if(thing==null || thing.gameObject.name!=val.objectName)
				thing = getParent(val.objectToModify.transform, name);
			if(thing==null || thing.gameObject.name!=val.objectName)
				thing = getChildGameObject (thing, name);
			return thing;
		}

		public void SetupObject(){
			if (val.objectName.Length > 0) {
				if (val.findObjectInHierarchy) {
					val.objectToModify = FindInHierarchy (val.objectToModify.transform, val.objectName).gameObject;
				} else
					val.objectToModify = GameObject.Find (val.objectName);
			}
			SetupObjects ();
		}

		public void SetupObjects(){
			if(val.objectNames.Length>0){
				val.objectsToModify = new GameObject[val.objectNames.Length];
				if(val.populateFromStrings){
					for (int i = 0; i < val.objectNames.Length; i++) {
						if(val.findObjectInHierarchy){
							val.objectsToModify[i] = FindInHierarchy(val.objectToModify.transform,val.objectNames[i]).gameObject;
						}
						else
							val.objectsToModify[i] = GameObject.Find(val.objectNames[i]);
					}
				}
			}
		}

		void Start(){
			SetupObject ();
		}

		public void OnEnable(){
			
			if (val.objectToModify == null)
				val.objectToModify = this.gameObject;
			
			interactable = GetComponent<ON_FSM> ();
//			triggerParams = pubVal.triggerParams;
//			type = val.type;
//			triggerType = val.triggerType;
			debug = val.debug;
			blocking = val.blocking;
			killOthersOnStart = val.killOthersOnStart;
			blockOnTrigger = val.blockOnTrigger;
			switch (val.type) {
				case pubVal.parameters.OnEnter:
					interactable.OnEnter += Ping;
					break;
			case pubVal.parameters.OnExit:
					interactable.OnExit += Ping;
					break;
			case pubVal.parameters.OnHover:
					interactable.OnHover += Ping;
					break;
			case pubVal.parameters.OnTrigger:
					interactable.OnTrigger += Ping;
					break;
			case pubVal.parameters.EndHover:
					interactable.EndHover += Ping;
					break;
				default:
					break;
			}
			interactable.KillSame += KillSame;
			interactable.OnIdle += Reset;
		}

		public void OnDisable()
		{
			switch (val.type) {
			case pubVal.parameters.OnEnter:
					interactable.OnEnter -= Ping;
					break;
			case pubVal.parameters.OnExit:
					interactable.OnExit -= Ping;
					break;
			case pubVal.parameters.OnHover:
					interactable.OnHover -= Ping;
					break;
			case pubVal.parameters.OnTrigger:
					interactable.OnTrigger -= Ping;
					break;
			case pubVal.parameters.EndHover:
					interactable.EndHover -= Ping;
					break;
				default:
					break;
			}	
			interactable.KillSame -= KillSame;
			interactable.OnIdle -= Reset ;
		}
	}
}


