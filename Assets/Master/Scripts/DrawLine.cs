//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//namespace ON{
//	public class DrawLine : MonoBehaviour {
//
//		public List<GameObject> ctrls;
//		public Transform[] controlPointsList;
//		public LineRenderer lineRend;
//		public TubeRenderer tubeRend;
//		public bool isLooping = false;
//		List<Vector3> points;
//		public float width;
//		Vector3[] vecs;
//		public float resolution = .2f;
//		BezierArm arm;
//		public GameObject boneParent;
//
//		void Start(){
//			arm = GetComponent<BezierArm> ();
//			points = new List<Vector3> ();
//			controlPointsList = new Transform[ctrls.Count];
//			for (int i = 0; i < ctrls.Count; i++) {
//				controlPointsList [i] = ctrls [i].transform;
//			}
//
//		}
//
//		void Update(){
//			for (int i = 0; i < controlPointsList.Length; i++) {
//				DisplayCatmullRomSpline (i);
//			}
//			if (boneParent != null) {
//				UpdateBones ();
//			}
//			if ( points.Count>0) {
//				if (vecs==null||vecs.Length==0)
//					vecs = new Vector3[points.Count];
//				for (int i = 0; i < points.Count; i++) {
//					if(i<vecs.Length)
//						vecs [i] = points [i];
//
//				}
//				points.Clear ();
//				if (lineRend != null) {
//					lineRend.positionCount = vecs.Length;
//					lineRend.SetPositions (vecs);
//					lineRend.widthMultiplier = width;
//				}
//				if (tubeRend != null) {
//					tubeRend.SetPoints(vecs,width,Color.white);
//				}
//			}
//
//		}
//
//		void UpdateBones(){
//			for (int i = 0; i < boneParent.transform.childCount; i++) {
//				float a = (float)boneParent.transform.childCount - 1;
//				if (i == 0) {
//					boneParent.transform.GetChild (i).transform.position = arm.root.position;
//				} else
//					boneParent.transform.GetChild (i).transform.position = GetSplinePos ((float)i / a);
//			}
//			for (int i = 0; i < boneParent.transform.childCount; i++) {
//
//				if (i < boneParent.transform.childCount - 1) {
//					boneParent.transform.GetChild (i).transform.LookAt (boneParent.transform.GetChild (i + 1).transform.position, Vector3.Lerp (arm.root.up, arm.target.up, (float)i / (float)boneParent.transform.childCount));
//				} else {
//					Vector3 endLook = boneParent.transform.GetChild (i ).transform.position - boneParent.transform.GetChild (i-1).transform.position;
//					endLook += boneParent.transform.GetChild (i).transform.position;
//					boneParent.transform.GetChild (i).transform.LookAt (endLook, Vector3.Lerp (arm.root.up, arm.target.up, (float)i / (float)boneParent.transform.childCount));
//				}
//			}
//
//		}
//
//		//Display a spline between 2 points derived with the Catmull-Rom spline algorithm
//		void DisplayCatmullRomSpline(int pos)
//		{
//			//The 4 points we need to form a spline between p1 and p2
//			Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
//			Vector3 p1 = controlPointsList[pos].position;
//			Vector3 p2 = controlPointsList[ClampListPos(pos + 1)].position;
//			Vector3 p3 = controlPointsList[ClampListPos(pos + 2)].position;
//
//			//The start position of the line
//			Vector3 lastPos = p1;
//
//			//The spline's resolution
//			//Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
////			float resolution = 0.2f;
//
//			//How many times should we loop?
//			int loops = Mathf.FloorToInt(1f / resolution);
//
//			for (int i = 0; i <= loops; i++)
//			{
//				//Which t position are we at?
//				float t = i * resolution;
//
//				//Find the coordinate between the end points with a Catmull-Rom spline
//				Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);
//
//				//Draw this line segment
////				Gizmos.DrawLine(lastPos, newPos);
//
//				//Save this pos so we can draw the next line segment
//				if(!newPos.Equals(lastPos))
//					points.Add (lastPos);
//				
//				lastPos = newPos;
//
//			}
//		}
//
//		Vector3 GetSplinePos(float t)
//		{
//			int pos = (int)Mathf.Floor(t * (controlPointsList.Length-1));
//
//			//The 4 points we need to form a spline between p1 and p2
//			Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
//			Vector3 p1 = controlPointsList[pos].position;
//			Vector3 p2 = controlPointsList[ClampListPos(pos + 1)].position;
//			Vector3 p3 = controlPointsList[ClampListPos(pos + 2)].position;
//
////			Debug.Log ((t * (controlPointsList.Length-1))-pos);
//			return GetCatmullRomPosition(((t * (controlPointsList.Length-1))-pos), p0, p1, p2, p3);
//
//		}
//
//		//Clamp the list positions to allow looping
//		int ClampListPos(int pos)
//		{
//			if (pos < 0)
//			{
//				pos = controlPointsList.Length - 1;
//			}
//
//			if (pos > controlPointsList.Length)
//			{
//				pos = 1;
//			}
//			else if (pos > controlPointsList.Length - 1)
//			{
//				pos = 0;
//			}
//
//			return pos;
//		}
//
//		//Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
//		//http://www.iquilezles.org/www/articles/minispline/minispline.htm
//		Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
//		{
//			//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
//			Vector3 a = 2f * p1;
//			Vector3 b = p2 - p0;
//			Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
//			Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;
//
//			//The cubic polynomial: a + b * t + c * t^2 + d * t^3
//			Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
//
//			return pos;
//		}
//
//	}
//}