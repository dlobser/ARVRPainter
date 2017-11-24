using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blitter1 : MonoBehaviour {

	public CustomRenderTexture A;
	public RenderTexture B;
	public RenderTexture C;
	public Material mat;
	public Material lineMat;
	public Vector4 pos;
	bool f;
	ON_MouseInteraction mouse;
	Vector2 prevCoord;
	Vector2 prevCoord2;
	public int steps = 1;
	// Use this for initialization
	void Start () {
		mouse = FindObjectOfType<ON_MouseInteraction> ();
		A.DiscardContents (true,true);
		B.DiscardContents (true,true);
		UnityEngine.RenderTexture.active = B;
		GL.Clear (true, true, Color.black);
		UnityEngine.RenderTexture.active = C;
		GL.Clear (true, true, Color.black);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.X)) {
			mat.SetFloat ("_Mult", 1);
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			mat.SetFloat ("_Mult", 0);
		}
		if (mouse.beenHit && mouse.hitCoord!=prevCoord) {
//			A.Update ();
			Vector2 anchor1 = (prevCoord2-prevCoord) + prevCoord;
			Vector2 lerped = Vector2.Lerp (prevCoord, mouse.hitCoord, .5f);
			Vector2 lerped2 = anchor1;// Vector2.Lerp (anchor1, lerped, .5f);

			for (int i = 0; i < steps; i++) {

				Vector2 aa = bezier (prevCoord, lerped2, mouse.hitCoord, (float)i / (float)(steps+1));
				Vector2 bb = bezier (prevCoord, lerped2, mouse.hitCoord, (float)(i+1) / (float)(1+steps));
				pos = new Vector4(aa.x,aa.y,bb.x,bb.y);
				lineMat.SetVector ("_Pos", pos);

				Graphics.Blit (C, B, lineMat);
				Graphics.Blit (B, C, mat);
			}



			prevCoord = mouse.hitCoord;
			prevCoord2 = prevCoord;
		}
	
	}

	Vector2 bezier(Vector2 A, Vector2 B, Vector2 C, float t){
		Vector2 a = Vector2.Lerp (A, B, t);
		Vector2 b = Vector2.Lerp (B, C, t);
		return Vector2.Lerp (a, b, t);

	}
}
