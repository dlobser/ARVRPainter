using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ON{

	public class ON_FSMTrigger_ColorUIText : ON_FSMTrigger_Base {

		public Color newColor;
		public Color oldColor;
		public bool getOldColorFromMaterial = false;
		public bool getOldColorFromCurrent = false;
		public bool alphaOnly;
		Color col;
		public Text renderer;
		public bool affectChildren;

		void Start(){
			SetupObject ();
			SetupRenderer ();
		}

		void SetupRenderer(){
			if (renderer == null)
				renderer = val.objectToModify.GetComponent<Text> ();

			if(getOldColorFromMaterial)
				oldColor = renderer.color;
		}

		public override void Ping()
		{
			print ("Ping()");
			SetupRenderer ();
			base.Ping ();
		}

		public override void Ping(float t)
		{
			Debug.Log (renderer);
			if (!triggered || renderer==null) {
				SetupRenderer ();
			}
			base.Ping (t);
		}

		public override void Reset(){
			base.Reset ();
		}


		public override void Modify(float t){
			if (alphaOnly) {
				Color col = renderer.color;
				if (!baseSettings.reverse)
					col.a = t;
				else
					col.a = 1 - t;
				renderer.color = col;
			} else {
				if (!baseSettings.reverse)
					renderer.color = Color.Lerp (oldColor, newColor, t);
				else
					renderer.color = Color.Lerp (newColor, oldColor, t);
			}
		}
	}
}
