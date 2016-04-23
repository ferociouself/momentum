using UnityEngine;
using System.Collections;

public class HardRepulsor : MonoBehaviour {

	private Vector2 pushForce;
	private float pushMagnitude;

	public float pushModifier;

	ParticleSystem particles;
	ParticleSystem.EmissionModule emis;

	// Use this for initialization
	void Start () {
		particles = gameObject.GetComponent<ParticleSystem>();
		emis = particles.emission;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		particles.Emit(1000);
		coll.gameObject.GetComponent<MomentumContainer>().ZeroMomentum();
		pushForce = new Vector2(coll.transform.position.x - gameObject.transform.position.x, 
			coll.transform.position.y - gameObject.transform.position.y);
		//pushMagnitude = coll.rigidbody.velocity.magnitude * pushModifier;
		pushForce = pushForce * pushModifier;
		coll.gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce);
	}
}
