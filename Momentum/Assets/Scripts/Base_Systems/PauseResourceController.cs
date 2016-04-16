using UnityEngine;
using System.Collections;

public class PauseResourceController : MonoBehaviour {

	private int pauseCount = 0;
	private bool resourceDrain = false;
	private float pauseResource = 50.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (resourceDrain) {
			pauseResource = Mathf.Max(pauseResource - 0.05f, 0.0f);
		} else {
			pauseResource = Mathf.Min(pauseResource + 0.05f, 50.0f);
		}
	}

	public void Paused() {
		pauseCount++;
		pauseResource -= 5.0f;
	}

	public void setResourceDrain(bool draining) {
		resourceDrain = draining;
	}

	public float getPauseResource() {
		return pauseResource;
	}
}
