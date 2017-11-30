using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_AudioVolume : ON_FSMTrigger_Base {

		public bool getAudioFromObject;
		public bool chooseAudio;
		public int chosenAudio;
		public AudioSource[] audios;
		public float volumeLow = 1;
		public float volumeHigh =1;
		public float pitchLow=1;
		public float pitchHigh=1;


		public override void KillSame(){
			same = GetComponents<ON_FSMTrigger_AudioVolume> ();
			base.KillSame ();
		}

		public override void Modify(float t){
			if (audios.Length < 1) {
				if (getAudioFromObject) {
					audios = val.objectToModify.GetComponents<AudioSource> ();
				}
			}
			if (audios.Length > 0) {
				
				if (!chooseAudio) {
					for (int i = 0; i < audios.Length; i++) {
						audios [i].volume = Mathf.Lerp (volumeLow, volumeHigh, t);
						audios [i].pitch = Mathf.Lerp (pitchLow, pitchHigh, t);
					}
				} else {
					audios [chosenAudio].volume = Mathf.Lerp (volumeLow, volumeHigh, t);
					audios [chosenAudio].pitch = Mathf.Lerp (pitchLow, pitchHigh, t);
				}
			}
		}
	}
}
