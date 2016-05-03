using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PauseResourceController : MonoBehaviour {

	private int pauseCount = 0;
	private bool resourceDrain = false;
	private float pauseResource = 50.0f;

	private Stopwatch stopwatch;

	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch();
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
		stopwatch.Start();
	}

	public void setResourceDrain(bool draining) {
		resourceDrain = draining;
		if (!resourceDrain) {
			stopwatch.Stop();
		}
	}

	public float getPauseResource() {
		return pauseResource;
	}

	public int getPauseCount() {
		return pauseCount;
	}

	public System.TimeSpan getPauseTime() {
		return stopwatch.Elapsed;
	}
}
