using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

	List<GameObject> goals = new List<GameObject>();
	List<bool> goalsActivated;

	public int GoalNum = 1;

	GameObject editModeController;

    bool levelOver = false;

	// Use this for initialization
	void Start () {
		editModeController = GameObject.Find("EditModeController");
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("goal")) {
			goals.Add(go);
		}
		goalsActivated = new List<bool>(goals.Count);
		for (int i = 0; i < goalsActivated.Capacity; i++) {
			goalsActivated.Add(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!EndGameStats.gameBegun) {
			EndGameStats.beginGame();
		}
		for (int i = 0; i < goals.Count; i++) {
			if (goals[i].GetComponent<Goal>().getActive()) {
				goalsActivated[i] = true;
			}
		}
		if (!goalsActivated.Contains(false) && !levelOver) {
            levelOver = true;
			EndLevel();
		}
	}

	void EndLevel() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
		EndGameStats.addToTotalPause(editModeController.GetComponent<PauseResourceController>().getPauseCount());
		EndGameStats.addToTotalPauseTime(editModeController.GetComponent<PauseResourceController>().getPauseTime());
		Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		EndGameStats.endLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex 
			== (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1)) {
			EndGame.victory = true;
			EndGameStats.finalLevel();
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}
}
