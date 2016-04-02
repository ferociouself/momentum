using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public float pullValue;
    public float minAlpha = 0;
    public float maxAlpha = 1;

	Vector2 pullForce;

	bool active;

	GameObject playerObj;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		pullForce = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (active) {
			pullForce.Set(gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
				gameObject.transform.localPosition.y - playerObj.transform.localPosition.y);

			SpriteRenderer objectColor = playerObj.GetComponent<SpriteRenderer>();

			float dist = distance(gameObject.transform, playerObj.transform);
            float maxDist = gameObject.GetComponent<CircleCollider2D>().radius * 5;
            float newAlpha = minAlpha + ((maxAlpha - minAlpha) * Mathf.Min(1, dist / maxDist));

            objectColor.color = new Color(
                objectColor.color.r,
                objectColor.color.g,
                objectColor.color.b,
                newAlpha
                );

			rb.AddForce(pullForce * pullValue);
		} else {
			
		}
	}

	public void OnTriggerEnter2D(Collider2D coll) {
		active = true;
		playerObj = coll.gameObject;
		if (playerObj != null) {
			rb = playerObj.GetComponent<Rigidbody2D>();
		}
	}

	public void OnTriggerExit2D(Collider2D coll) {
		active = false;
		playerObj = null;
		rb = null;
		SpriteRenderer objectColor = coll.gameObject.GetComponent<SpriteRenderer>();
		objectColor.color = new Color(objectColor.color.r, objectColor.color.g, objectColor.color.b, 1f);
	}

	private float distance(Transform t1, Transform t2) {
		// gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
		// gameObject.transform.localPosition.y - playerObj.transform.localPosition.y;
		return Mathf.Sqrt(Mathf.Pow(t1.localPosition.x - t2.localPosition.x, 2) + Mathf.Pow(t1.localPosition.y - t2.localPosition.y, 2));
	}
}
