﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

	List<GameObject> goals = new List<GameObject>();
	List<bool> goalsActivated;

	public int GoalNum = 1;

	// Use this for initialization
	void Start () {
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
		for (int i = 0; i < goals.Count; i++) {
			if (goals[i].GetComponent<Goal>().getActive()) {
				goalsActivated[i] = true;
			}
		}
		if (!goalsActivated.Contains(false)) {
			EndLevel();
		}
	}

	void EndLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}
}