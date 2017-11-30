using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ON{
	public class ON_FSMTrigger_ColorCanvasGroup : ON_FSMTrigger_Base {
		public float newAlpha;
		public float oldAlpha;
		public bool getOldAlphaFromMaterial = false;
		public bool getOldAlphaFromCurrent = false;
		float alpha;
		public CanvasGroup renderer;

		void Start(){
			SetupObject ();
			SetupRenderer ();
		}

		void SetupRenderer(){
			if (renderer == null)
				renderer = val.objectToModify.GetComponent<CanvasGroup> ();

			if(getOldAlphaFromMaterial)
				alpha = renderer.alpha;
		}

		public override void Ping()
		{
			SetupRenderer ();
			base.Ping ();
		}

		public override void Ping(float t)
		{
			if (!triggered || renderer==null) {
				SetupRenderer ();
			}
			base.Ping (t);
		}

		public override void Reset(){
			base.Reset ();
		}


		public override void Modify(float t){
			if (!baseSettings.reverse)
				renderer.alpha = Mathf.Lerp (oldAlpha, newAlpha, t);
			else				 
				renderer.alpha = Mathf.Lerp (newAlpha, oldAlpha, t);
			
		}
	}
}
