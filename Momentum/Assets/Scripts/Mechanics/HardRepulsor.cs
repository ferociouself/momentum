using UnityEngine;
using System.Collections;

public class HardRepulsor : MonoBehaviour {

	private Vector2 pushForce;
	private float pushMagnitude;

	public float pushModifier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		pushForce = new Vector2(coll.transform.position.x - gameObject.transform.position.x, 
			coll.transform.position.y - gameObject.transform.position.y);
		pushMagnitude = coll.rigidbody.velocity.magnitude * pushModifier;
		pushForce = pushForce * pushMagnitude;
		coll.gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce);
	}
}
