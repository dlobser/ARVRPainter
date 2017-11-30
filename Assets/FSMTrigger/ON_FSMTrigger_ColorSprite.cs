using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	public class ON_FSMTrigger_ColorSprite : ON_FSMTrigger_Base {

		public Color newColor;
		public Color oldColor;
		public bool getOldColorFromMaterial = false;
		public bool getOldColorFromCurrent = false;
		public bool alphaOnly;
		Color col;
		public SpriteRenderer renderer;

		void Start(){
			SetupObject ();
			SetupRenderer ();
		}

		void SetupRenderer(){
			if (renderer == null )
			if(val.objectToModify!=null)
			if(val.objectToModify.GetComponent<SpriteRenderer> ()!=null)
				renderer = val.objectToModify.GetComponent<SpriteRenderer> ();

			if(getOldColorFromMaterial)
				oldColor = renderer.color;
		}

		public override void Ping()
		{
			SetupRenderer ();
			base.Ping ();
		}

		public override void Ping(float t)
		{
			if (!triggered) {
				SetupRenderer ();
			}
			base.Ping (t);

		}

		public override void Reset(){
			base.Reset ();
		}


		public override void Modify(float t){
			if (renderer != null) {
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
}
