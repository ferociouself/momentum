using UnityEngine;
using System.Collections;

public class StaticMethods : MonoBehaviour {
	public static float Distance(Transform t1, Transform t2) {
		return (t1.localPosition - t2.localPosition).magnitude;
	}
}
