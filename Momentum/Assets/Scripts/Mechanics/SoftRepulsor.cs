using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoftRepulsor : MonoBehaviour {

	private Vector2 pushForce;
	private float pushMagnitude;

	private bool active = false;

	public float pushModifier;

	private List<GameObject> activeObjectList;

	ParticleSystem particles;
	ParticleSystem.EmissionModule emis;

	// Use this for initialization
	void Start () {
		activeObjectList = new List<GameObject>();
		pushForce = new Vector2(0.0f, 0.0f);
		particles = gameObject.GetComponent<ParticleSystem>();
		emis = particles.emission;
		emis.enabled = false;
		particles.Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		active = activeObjectList.Count > 0;
		if (active) {
			emis.enabled = true;
			foreach (GameObject obj in activeObjectList) {
				pushForce.Set(obj.transform.position.x - gameObject.transform.position.x, 
					obj.transform.position.y - gameObject.transform.position.y);
				pushMagnitude = pushModifier * (1/StaticMethods.Distance(obj.transform, gameObject.transform));
				pushForce = pushForce * pushMagnitude;
				obj.gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce);
			}
		}
		else {
			emis.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!activeObjectList.Contains(coll.gameObject))
			activeObjectList.Add(coll.gameObject);
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (activeObjectList.Contains(coll.gameObject))
			activeObjectList.Remove(coll.gameObject);
	}


}
