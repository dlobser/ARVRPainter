using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_GameObjectSelector : MonoBehaviour {

		public GameObject[] A;
		public GameObject[] B;
		public GameObject[] C;
		public GameObject[] D;

		public GameObject Find(string name){
			for (int i = 0; i < A.Length; i++) {
				if(A[i].name ==name)
					return A[i];
			}
			for (int i = 0; i < B.Length; i++) {
				if(B[i].name ==name)
					return B[i];
			}
			for (int i = 0; i < C.Length; i++) {
				if(C[i].name ==name)
					return C[i];
			}
			for (int i = 0; i < D.Length; i++) {
				if(D[i].name ==name)
					return D[i];
			}
			return null;
		}

	}
}