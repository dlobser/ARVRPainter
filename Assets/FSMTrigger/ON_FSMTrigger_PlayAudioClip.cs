using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_PlayAudioClip : ON_FSMTrigger {

//		public bool getAudioFromObject;

		public AudioClip[] audi;
		public AudioSource audioSource;
		public bool disableAfterPlay = false;
		public bool randomizePitch = false;
		public Vector2 pitchLowHigh = Vector2.one;
		public float volume;
		bool playedOnce = false;
		public bool dontInterrupt;

		public override void Ping()
		{
			if (val.triggerCounter < val.maxTriggers || val.maxTriggers == 0) {
				if (audioSource==null) {
					audioSource = val.objectToModify.GetComponent<AudioSource> ();
				}
				base.Ping ();
				val.triggerCounter++;
				if (!playedOnce && audi.Length > 0) {
					if(!audioSource.isPlaying || audioSource.isPlaying && !dontInterrupt){
						int index = (int)Mathf.Floor (Random.value * audi.Length);
						audioSource.clip = audi [index];
						if (randomizePitch)
							audioSource.pitch = Random.Range (pitchLowHigh.x, pitchLowHigh.y);
						if (audioSource != null) {
							audioSource.volume = volume;
							audioSource.Play ();
						}
	//					if (val.triggerType == pubVal.triggerParams.OnEnd) {
	//						print ("On Audio End");
						StartCoroutine ("CheckPlaying", audioSource);
					}

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
