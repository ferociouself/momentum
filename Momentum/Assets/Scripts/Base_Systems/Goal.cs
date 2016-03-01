using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			// Pull the Player in here
			// After a timeframe, or once the player is in the center, end the level.
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		active = true;
	}

	private bool EndLevel() {
		return false;
	}
}
