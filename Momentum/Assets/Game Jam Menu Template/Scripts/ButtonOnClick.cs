using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour {
    int sceneToEnable = 0;
    public GameObject UI;

	// Use this for initialization
	void Start () {
	
	}

    public void onClick(int scene)
    {
        //SceneManager.LoadScene(scene);
    }
	
	// Update is called once per frame
	void Update () {
        //UI.SetActive(true);
	}
}
