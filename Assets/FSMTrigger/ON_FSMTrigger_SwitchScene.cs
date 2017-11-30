using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ON{
	public class ON_FSMTrigger_SwitchScene : ON_FSMTrigger{

		public int whichScene;

		public override void Ping() {
			SceneManager.LoadScene (whichScene);
		}

	}
}