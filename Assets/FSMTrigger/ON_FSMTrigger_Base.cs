using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

	[System.Serializable]
	public class BaseVal{
		public float speed = 1;
		public Interpolate.EaseType easeType; // set using Unity's property inspector

		public bool reverse = false;
		public bool pingpong;
	}


	public class ON_FSMTrigger_Base : ON_FSMTrigger {

		public BaseVal baseSettings = new BaseVal();
		Interpolate.Function ease; 
		public float counter { get; set; }
		public ON_FSMTrigger[] same { get; set; }
//		public int maxTriggers;
//		int triggerCounter = 0;

		void Awake() {
			ease = Interpolate.Ease(baseSettings.easeType);
		}

	    public override void Ping()
	    {
			print ("Ping()");
			if (val.triggerCounter < val.maxTriggers || val.maxTriggers == 0) {
				Kill ();
				if (killOthersOnStart)
					KillSame ();
				StartCoroutine ("Animate");
				val.triggerCounter++;
				triggered = true;
				if (val.triggerType == pubVal.triggerParams.OnPing) {
					print ("OnPing");
					if(val.manualTriggers.Length<1)
						interactable.Trigger (blockOnTrigger);
					ManualTrigger ();
				}
			}
	    }


		public override void Ping(float t)
		{
			print ("Ping(t)");
			if (val.triggerCounter < val.maxTriggers || val.maxTriggers == 0) {
				
				Modify (ease (0, 1, t, 1));

				if (!triggered) {
					if (killOthersOnStart)
						KillSame ();
					if (val.triggerType == pubVal.triggerParams.OnPing) {
						print ("OnPing");
						if(val.manualTriggers.Length<1)
							interactable.Trigger (blockOnTrigger);
						ManualTrigger ();
					}
					val.triggerCounter++;
				} else if (triggered && t <= 0) {
					if (val.triggerType == pubVal.triggerParams.OnEnd) {
						print ("OnEnd");
						if(val.manualTriggers.Length<1)
							interactable.Trigger (blockOnTrigger);
						ManualTrigger ();
					}
					Reset ();
				}
				if (t >= 1) {
					if (val.triggerType == pubVal.triggerParams.OnPong) {
						print ("OnPong");
						if(val.manualTriggers.Length<1)
							interactable.Trigger (blockOnTrigger);
						ManualTrigger ();

					}
				}

				
				triggered = t > 0 ? true : false;
			}

		}

		public override void Reset(){
			base.Reset ();
		}

		public override void KillSame(){
			for (int i = 0; i < same.Length; i++) {
				if(same[i]!=this)
					same [i].Kill ();
			}
		}

		public override void Kill(){
			triggered = false;
			StopCoroutine ("Animate");
			StopCoroutine ("UnAnimate");
			counter = 0;
		}

		public virtual IEnumerator UnAnimate()
		{
			while (counter > 0)
			{
				counter -= Time.deltaTime;

				Modify ( ease(0,1, counter / baseSettings.speed,1));
				yield return new WaitForSeconds(Time.deltaTime);
			}
			Reset ();
			yield return null;
		}

	    public virtual IEnumerator Animate()
	    {
			counter = 0;

			while (counter < baseSettings.speed)
	        {
	            counter += Time.deltaTime;
				Modify (ease(0,1, counter / baseSettings.speed,1));
	            yield return new WaitForSeconds(Time.deltaTime);
	        }
			StopCoroutine ("Animate");
			if (val.triggerType == pubVal.triggerParams.OnPong) {
				print ("OnPong");
				if(val.manualTriggers.Length<1)
					interactable.Trigger (blockOnTrigger);
//				Debug.Log ("PONG");
				ManualTrigger ();
			}
			if (baseSettings.pingpong) {
				StartCoroutine ("UnAnimate");
			}
			else
				Reset();
			yield return null;
	    }

		public virtual void Modify(float t){
			
		}
	}
}
