using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	public float pullValue;

	Vector2 pullForce;

	bool active;

	GameObject playerObj;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		pullForce = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			pullForce.Set(gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
				gameObject.transform.localPosition.y - playerObj.transform.localPosition.y);

			rb.AddForce(pullForce * pullValue);
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
	}
}
