using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	bool active = false;
	bool stopped = false;

	GameObject playerObj;
	Rigidbody2D rb;

	Vector2 pullForce;

	public enum MyColor
		{
			Blue,
			Gray,
			Green,
			Orange,
			Pink,
			Red,
			White,
			Yellow
		}

	public MyColor goalColor;

	// Use this for initialization
	void Start () {
		playerObj = null;
		pullForce = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			// Pull the Player in here

			if (!stopped) {
				StopBall();
			}

			pullForce.Set(gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
				gameObject.transform.localPosition.y - playerObj.transform.localPosition.y);

			rb.AddForce(pullForce * 10.0f);

			// After a timeframe, or once the player is in the center, end the level.

			if (Distance(gameObject.transform, playerObj.transform) < 1) {
				EndLevel();
				active = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		active = true;
		playerObj = coll.gameObject;
		if (playerObj != null)
			rb = playerObj.GetComponent<Rigidbody2D>();
	}

	void EndLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}

	void StopBall() {
		if (rb != null)
			rb.gravityScale = 0;
		playerObj.GetComponent<MomentumContainer>().ZeroMomentum();
		stopped = true;
	}

	float Distance(Transform t1, Transform t2) {
		return (t1.localPosition - t2.localPosition).magnitude;
	}
}
