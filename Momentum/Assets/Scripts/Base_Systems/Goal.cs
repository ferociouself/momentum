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
		EndLevel();
	}

	private bool EndLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("World1_2");
		UnityEngine.SceneManagement.SceneManager.SetActiveScene(
			UnityEngine.SceneManagement.SceneManager.GetSceneByName("World1_2"));
		Debug.Log("Ending Level");
		return true;
	}
}
