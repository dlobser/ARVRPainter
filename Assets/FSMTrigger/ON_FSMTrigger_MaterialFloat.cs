using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	public class ON_FSMTrigger_MaterialFloat : ON_FSMTrigger_Base {

	    public float newValue;
	    public float oldValue;
	    public bool getOldValueFromMaterial = false;
		public bool getOldValueFromCurrent = false;
	    public string channel;
	    Material mat;
		Material sharedMaterial;
		public bool returnToSharedMaterial;
		public Renderer renderer;
		public bool useSharedMaterial;

		void Start(){
			SetupObject ();
			if (renderer == null) {
				renderer = val.objectToModify.GetComponent<Renderer> ();
			}
			if (renderer != null) {
				sharedMaterial = renderer.sharedMaterial;
				if (getOldValueFromMaterial)
					oldValue = sharedMaterial.GetFloat (channel);
			}
			//			GetMat ();
		}

		void GetMat(){
			if (renderer == null) {
				renderer = val.objectToModify.GetComponent<Renderer> ();

				sharedMaterial = renderer.sharedMaterial;
				if (getOldValueFromMaterial)
					oldValue = sharedMaterial.GetFloat (channel);
			}
			if (!useSharedMaterial) {
				mat = Instantiate (renderer.sharedMaterial);
				renderer.material = mat;
			}
			else
				mat = renderer.sharedMaterial;
		}

	    public override void Ping()
	    {
			GetMat ();
			if(getOldValueFromMaterial)
				oldValue = mat.GetFloat(channel);
			base.Ping ();
	    }

		public override void Ping(float t){
			
			if (!triggered) {
				GetMat ();
				if (getOldValueFromCurrent)
					oldValue = mat.GetFloat (channel);
			}
			base.Ping (t);
		}

		public override void Reset(){
			base.Reset ();
			if(returnToSharedMaterial)
				renderer.material = sharedMaterial;
		}

		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_Color> ();
			base.KillSame ();
		}

		public override void Modify(float t){
			if(!baseSettings.reverse)
				mat.SetFloat(channel,Mathf.Lerp(oldValue, newValue, t));
			else
				mat.SetFloat(channel, Mathf.Lerp(oldValue, newValue, t));
		}
	}
}

