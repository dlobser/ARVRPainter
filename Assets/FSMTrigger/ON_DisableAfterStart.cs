using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_DisableAfterStart : MonoBehaviour {
		
		// Update is called once per frame
		void Update () {
			this.gameObject.SetActive (false);
			this.enabled = false;
		}
	}
}
