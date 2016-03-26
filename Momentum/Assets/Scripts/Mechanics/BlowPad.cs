using UnityEngine;
using System.Collections;

public class BlowPad : MonoBehaviour {

	GameObject playerObj;
	Rigidbody2D playerRB;

	Vector2 pushForce;

	bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			pushForce = new Vector2(0.0f, 0.0f) * (1/distance(playerObj.transform, gameObject.transform));
			Quaternion collRot = playerObj.transform.rotation;
			Vector3 rotatedPush3D = collRot * new Vector3(pushForce.x, pushForce.y);
			Vector2 rotatedPushForce = new Vector2(rotatedPush3D.x, rotatedPush3D.y);
			playerRB = playerObj.GetComponent<Rigidbody2D>();
			playerRB.AddForce(rotatedPushForce);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// Blow the player up
		playerObj = coll.gameObject;
		active = true;
	}

	void OnTriggerExit2D(Collider2D coll) {
		active = false;
		playerObj = null;
	}

	private float distance(Transform t1, Transform t2) {
		//gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
		//	gameObject.transform.localPosition.y - playerObj.transform.localPosition.y;
		return Mathf.Sqrt(Mathf.Pow(t1.localPosition.x - t2.localPosition.x, 2) + Mathf.Pow(t1.localPosition.y - t2.localPosition.y, 2));
	}
}
