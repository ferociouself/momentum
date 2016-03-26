using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public float teleportYChange = 10.0f;
	public float teleportXChange = 0.0f;

	private Vector2 teleportDelta;

	void Start() {
		teleportDelta = new Vector2(teleportXChange, teleportYChange);
	}

	// Called whenever another object enters the attached object's collider.
	void OnTriggerEnter2D(Collider2D coll) {
		Vector3 currPos = coll.gameObject.transform.position;
		coll.gameObject.transform.position.Set(currPos.x + teleportDelta.x, currPos.y + teleportDelta.y, 0);

        // Reset the trail
        TrailRenderer trail = coll.gameObject.GetComponent<TrailRenderer>();
        if (trail != null) {
            trail.Clear();
        }
	}
}
