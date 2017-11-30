using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_PlayAudio : ON_FSMTrigger {

		public bool getAudioFromObject;

		public AudioSource[] audi;
		public bool disableAfterPlay = false;
		public bool randomizePitch = false;
		public Vector2 pitchLowHigh = Vector2.one;
		public float volume;
		bool playedOnce = false;

		public override void Ping()
		{
			if (val.triggerCounter < val.maxTriggers || val.maxTriggers == 0) {
				if (getAudioFromObject) {
					audi = val.objectToModify.GetComponents<AudioSource> ();
				}
				base.Ping ();
				val.triggerCounter++;
				if (!playedOnce && audi.Length > 0) {
					int index = (int)Mathf.Floor (Random.value * audi.Length);
					if (randomizePitch)
						audi [index].pitch = Random.Range (pitchLowHigh.x, pitchLowHigh.y);
					if (audi [index] != null) {
						audi [index].volume = volume;
						audi [index].Play ();
					}
//					if (val.triggerType == pubVal.triggerParams.OnEnd) {
//						print ("On Audio End");
						StartCoroutine ("CheckPlaying", audi [index]);
//					}
				}
				if (disableAfterPlay) {
					this.enabled = false;
					playedOnce = true;
				}
			}
		}

		IEnumerator	CheckPlaying(AudioSource audi){
			
			while (audi.isPlaying) {
				yield return null;
			}
//			while (interactable.CheckBlocked ())
//				yield return null;

			if (val.triggerType == pubVal.triggerParams.OnEnd) {
				if (val.manualTriggers.Length < 1)
					interactable.Trigger (blockOnTrigger);
				ManualTrigger ();
			}
			triggered = false;
			yield return null;
		}


	}
}
