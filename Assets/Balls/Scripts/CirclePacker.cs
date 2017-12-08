using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CirclePacker : MonoBehaviour {
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

public class CirclePacker : MonoBehaviour
{
	public List<Circle> mCircles = new List<Circle>();
	public Circle mDraggingCircle = null;
	protected Vector2 mPackingCenter;
	public float mMinSeparation = 1f;

	public GameObject ball;

	public List<GameObject> balls;

	public int amount;
	public Vector2 minMax;
	public int setupIterations = 200;
	public Vector2 clip;

	/// <summary>
	/// Generates a number of Packing circles in the constructor.
	/// Random distribution is linear
	/// </summary>
	/// 
	void Start(){
		balls = new List<GameObject> ();
		Pack (Vector2.zero, amount, minMax.x, minMax.y);
		foreach (Circle c in mCircles) {
			GameObject b =Instantiate (ball, new Vector3 (c.mCenter.x, c.mCenter.y, 0), Quaternion.identity);
			b.transform.localScale = new Vector3 (c.mRadius, c.mRadius, c.mRadius);
			b.GetComponentInChildren<BirdMaker> ().MakeBird ();
			b.GetComponentInChildren<BirdMaker> ().transform.parent.gameObject.SetActive (false);
			b.transform.localEulerAngles = Random.insideUnitSphere * 360;
			b.transform.parent = this.transform;

			if (b.transform.childCount > 0) {
				b.transform.GetChild (0).eulerAngles = b.transform.parent.localEulerAngles;
			}
			balls.Add (b);
		}
		StartCoroutine(Solver());
//		Debug.Log (balls.Count);
//		Debug.Log (mCircles.Count);
	}

	IEnumerator Solver(){
		float count = 0;

		while (count < setupIterations) {
			OnFrameMove ((long)Time.deltaTime);

			for (int i = 0; i < mCircles.Count; i++) {
				Circle c = mCircles [i];
				if (c.mCenter.x + c.mRadius > clip.x || c.mCenter.x- c.mRadius < -clip.x ||
					c.mCenter.y+ c.mRadius > clip.y || c.mCenter.y- c.mRadius < -clip.y) {
					mCircles.RemoveAt (i);
					GameObject b = balls [i];
					balls.RemoveAt (i);
					Destroy (b);
				} else {
					float rad = c.mRadius * 2;
					balls [i].transform.localScale = new Vector3 (rad, rad, rad);
					balls [i].transform.localPosition = new Vector3 (c.mCenter.x, c.mCenter.y, balls [i].transform.localScale.x * .5f);
				}
			}
			count += 1;


			yield return null;// new WaitForSeconds (Time.deltaTime);
		}
//		yield return null;

	}

	public void Pack(Vector2 pPackingCenter, int pNumCircles,
		double pMinRadius, double pMaxRadius)
	{
		this.mPackingCenter = pPackingCenter;

		// Create random circles
		this.mCircles.Clear();
//		Random Rnd = new Random(System.DateTime.Now.Millisecond);
		for (int i = 0; i < pNumCircles; i++)
		{
			Vector2 nCenter = new Vector2((float)(this.mPackingCenter.x +
				Random.value * pMinRadius),
				(float)(this.mPackingCenter.y +
					Random.value * pMinRadius));
			float nRadius = (float)(pMinRadius + Random.value *
				(pMaxRadius - pMinRadius));
			this.mCircles.Add(new Circle(nCenter, nRadius));
		}
	}

	private float DistanceToCenterSq(Circle pCircle)
	{
		return Vector2.Distance (pCircle.mCenter, mPackingCenter);
	}

	private int Comparer(Circle p1, Circle P2)
	{
		float d1 = DistanceToCenterSq(p1);
		float d2 = DistanceToCenterSq(P2);
		if (d1 < d2)
			return 1;
		else if (d1 > d2)
			return -1;
		else return 0;
	}

	public void OnFrameMove(long iterationCounter)
	{
		// Sort circles based on the distance to center
		mCircles.Sort(Comparer);

		float minSeparationSq = mMinSeparation * mMinSeparation;
		for (int i = 0; i < mCircles.Count - 1; i++)
		{
//			Debug.Log (mCircles [i].mCenter);
			for (int j = i + 1; j < mCircles.Count; j++)
			{
				if (i == j)
					continue;

				Vector2 AB = mCircles[j].mCenter - mCircles[i].mCenter;
				float r = mCircles[i].mRadius + mCircles[j].mRadius;

				// Length squared = (dx * dx) + (dy * dy);
				float d = Mathf.Pow( Vector2.Distance(mCircles[j].mCenter, mCircles[i].mCenter),2);//- minSeparationSq;
//				float minSepSq = Mathf.Min(d, minSeparationSq);
//				d -= minSepSq;

				if (d < (r * r)- 0.01 )
				{
					AB.Normalize();

					AB *= (float)((r - Mathf.Sqrt(d)) *.5f );

//					if (mCircles[j] != mDraggingCircle)
						mCircles[j].mCenter += AB;
//					if (mCircles[i] != mDraggingCircle)
						mCircles[i].mCenter -= AB;
				}
			}
		}


//		float damping = 0.1f / (float)(iterationCounter);
//		for (int i = 0; i < mCircles.Count; i++)
//		{
//			if (mCircles[i] != mDraggingCircle)
//			{
//				Vector2 v = mCircles[i].mCenter - this.mPackingCenter;
//				v *= damping;
//				mCircles[i].mCenter -= v;
//			}
//		}
	}
	/// <summary>
	///
	/// </summary>
//	public void OnMouseDown(MouseEventArgs e)
//	{
//		Vector2 center = new Vector2(e.X, e.Y);
//		mDraggingCircle = null;
//		foreach (Circle circle in mCircles)
//		{
//			float dist = (circle.mCenter - center).LengthSquared();
//			if (dist < (circle.mRadius * circle.mRadius))
//			{
//				mDraggingCircle = circle;
//				break;
//			}
//		}
//	}
//	/// <summary>
//	///
//	/// </summary>
//	public void OnMouseMove(MouseEventArgs e)
//	{
//		if (mDraggingCircle == null)
//			return;
//
//		mDraggingCircle.mCenter = new Vector2(e.X, e.Y);
//	}
}