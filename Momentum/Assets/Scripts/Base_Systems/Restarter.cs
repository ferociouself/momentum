using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Reset")) {
			gameObject.GetComponent<EditModeScript>().BeginEditModeTransition(true);
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
			    EndGameStats.restarted();
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		if (Input.GetButtonUp("Restart")) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
