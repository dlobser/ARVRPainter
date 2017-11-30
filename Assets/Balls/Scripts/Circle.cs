//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//
//public class Circle : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//}
public class Circle
{
	public Vector2 mCenter;
	public float mRadius;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="iCenter"></param>
	/// <param name="Radius"></param>
	public Circle(Vector2 iCenter, float Radius)
	{
		mCenter = iCenter;
		mRadius = Radius;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public override string ToString()
	{
		return "Rad: " + mRadius + " _ Center: " + mCenter.ToString();
	}     
}
