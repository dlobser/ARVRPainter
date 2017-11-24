using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ON_MouseInteraction : MonoBehaviour {

    public bool UseMouse;
    public bool useObject;
    public string objectName = "Controller (left)";
    public static GameObject rayObject { get; set; }
    public Vector3 hitPosition;
    public static Vector3 theHitPosition;
    public Vector3 hitNormal;
	public GameObject hitObject;
    public static GameObject theHitObject;
    public bool beenHit;
	GameObject objPos;
	public Vector2 hitCoord;


    public delegate void MouseHasHit();
    public static event MouseHasHit mouseHasHit;

    private void Start() {
        rayObject = GameObject.Find(objectName);
        if (rayObject == null && useObject) {
            Debug.LogWarning("ray object not found");
        }
		if (useObject) {
			UseMouse = false;
			Debug.LogWarning ("Can't use object and mouse at the same time");
		}
    }

    void Update() {
		
        RaycastHit hitInfo = new RaycastHit();
		bool hit;

		hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
//		hit = Physics.Raycast(new Ray(objPos.transform.position, objPos.transform.forward), out hitInfo, 1e6f);
        beenHit = hit;

        if (hit) {
			hitCoord = hitInfo.textureCoord;
            hitPosition = hitInfo.point;
            hitNormal = hitInfo.normal;
			hitObject = hitInfo.collider.gameObject;
        
        }
        else {
			hitCoord = Vector2.zero;
            hitPosition = Vector3.zero;
            hitNormal = Vector3.zero;
			hitObject = null;
        }

    }
}

