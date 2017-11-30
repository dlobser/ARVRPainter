using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	public class ON_FSMTrigger_Color : ON_FSMTrigger_Base {

	    public Color newColor;
	    public Color oldColor;
	    public bool getOldColorFromMaterial = false;
		public bool getOldColorFromCurrent = false;
	    public string channel;
		public bool alphaOnly;
		public bool returnToSharedMaterial;
	    Material mat;
		Material sharedMaterial;
		Renderer rend;
		public Renderer renderer;
		Renderer oldRend;


		void Start(){
			SetupObject ();
			if (renderer == null) {
				renderer = val.objectToModify.GetComponent<Renderer> ();
			}
			if (renderer != null) {
				sharedMaterial = renderer.sharedMaterial;
				if (getOldColorFromMaterial)
					oldColor = sharedMaterial.GetColor (channel);
			}
//			GetMat ();
		}

		void GetMat(){
			if (renderer == null) {
				renderer = val.objectToModify.GetComponent<Renderer> ();
			
				sharedMaterial = renderer.sharedMaterial;
				if (getOldColorFromMaterial)
					oldColor = sharedMaterial.GetColor (channel);
			}
			mat = Instantiate (renderer.sharedMaterial);
			renderer.material = mat;


		}

	    public override void Ping()
	    {
//			Debug.Log("TIME "+ Time.timeSinceLevelLoad);
			GetMat ();
			if(getOldColorFromCurrent)
				oldColor = mat.GetColor(channel);
			base.Ping ();
	    }

		public override void Ping(float t){
			
			if (!triggered) {
				GetMat ();
				if (getOldColorFromCurrent)
					oldColor = mat.GetColor (channel);
				
			}
			base.Ping (t);
		}

		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_Color> ();
			base.KillSame ();
		}

		public override void Reset(){
			base.Reset ();
			if(returnToSharedMaterial)
				renderer.material = sharedMaterial;
		}

		public override void Modify(float t){
			
			if (alphaOnly) {
				Color col = mat.GetColor (channel);
				if (!baseSettings.reverse)
					col.a = t;
				else
					col.a = 1 - t;
				
				mat.SetColor (channel, col);
			} else {
				if (!baseSettings.reverse)
					mat.SetColor(channel, Color.Lerp (oldColor, newColor, t));
				else
					mat.SetColor(channel, Color.Lerp (newColor, oldColor, t));
			}
		}
	}
}
//
//using UnityEngine;
//
//public class TriggerPrewarmColor : TriggerPrewarm {
//
//	public Color newColor;
//	Color oldColor;
//	public string channel;
//	public MeshRenderer rend;
//	Material mat;
//	Material sharedMat;
//
//	public float power = 1f;
//
//	public bool getRendererFromObject;
//
//	public override void Animate(float t) {
//		mat = rend.material;
//		mat.SetColor(channel, Color.Lerp(oldColor, newColor, Mathf.Pow(t,power)));
//	}
//
//	public override void Reset()
//	{
//		mat = sharedMat;
//	}
//
//	void Start()
//	{
//		if (getRendererFromObject)
//			rend = GetComponent<MeshRenderer>();
//		sharedMat = rend.sharedMaterial;
//		oldColor = sharedMat.GetColor(channel);
//	}
//
//
//}
