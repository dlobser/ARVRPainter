using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

	public Material material;
	// Use this for initialization
//	void Start () {
//		
//	}
	void Start () {
		Texture2D txr = new Texture2D (512, 512);
		DrawLine (txr, new Vector2 (0, 0), new Vector2 (512, 256), Color.red);
		txr.Apply ();

		material.SetTexture ("_MainTex", txr);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DrawLine(Texture2D tex, Vector2 p1, Vector2 p2, Color col)
	{
		Vector2 t = p1;
		float frac = 1/Mathf.Sqrt (Mathf.Pow (p2.x - p1.x, 2) + Mathf.Pow (p2.y - p1.y, 2));
		float ctr = 0;

		while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y) {
			t = Vector2.Lerp(p1, p2, ctr);
			ctr += frac;
			tex.SetPixel((int)t.x, (int)t.y, col);
		}
	}
}
