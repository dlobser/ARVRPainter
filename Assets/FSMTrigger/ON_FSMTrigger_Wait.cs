
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_FSMTrigger_Wait : ON_FSMTrigger_Base{

		public override void Modify(float t){
			if (debug)
				Debug.Log (t);
		}

	}
}