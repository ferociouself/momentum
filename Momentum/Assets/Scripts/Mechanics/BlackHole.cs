using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : MonoBehaviour {

	public float pullValue;
    public float minAlpha = 0;
    public float maxAlpha = 1;

	Vector2 pullForce;

	bool active;
	List<GameObject> activeObjectList;

	// Use this for initialization
	void Start () {
		pullForce = new Vector2(0.0f, 0.0f);
		activeObjectList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		active = activeObjectList.Count > 0;
		if (active) {
			foreach (GameObject obj in activeObjectList) {
				pullForce.Set(gameObject.transform.localPosition.x - obj.transform.localPosition.x, 
					gameObject.transform.localPosition.y - obj.transform.localPosition.y);
				SpriteRenderer objectColor = obj.GetComponent<SpriteRenderer>();

				float dist = StaticMethods.Distance(gameObject.transform, obj.transform);
				float maxDist = gameObject.GetComponent<CircleCollider2D>().radius * 5;
				float newAlpha = minAlpha + ((maxAlpha - minAlpha) * Mathf.Min(1, dist / maxDist));

				objectColor.color = new Color(
					objectColor.color.r,
					objectColor.color.g,
					objectColor.color.b,
					newAlpha
				);

				obj.GetComponent<Rigidbody2D>().AddForce(pullForce * pullValue);
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D coll) {
		if (!activeObjectList.Contains(coll.gameObject))
			activeObjectList.Add(coll.gameObject);
	}

	public void OnTriggerExit2D(Collider2D coll) {
		if (activeObjectList.Contains(coll.gameObject)) 
			activeObjectList.Remove(coll.gameObject);
		SpriteRenderer objectColor = coll.gameObject.GetComponent<SpriteRenderer>();
		objectColor.color = new Color(objectColor.color.r, objectColor.color.g, objectColor.color.b, 1f);
	}
}
