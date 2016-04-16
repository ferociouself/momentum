using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseConnector : MonoBehaviour {

	private PauseResourceController pauseCont;
	private float pausability;
	// Use this for initialization
	void Start () {
		pauseCont = GameObject.Find ("EditModeController").GetComponent<PauseResourceController> ();
	}
	
	// Update is called once per frame
	void Update () {
		pausability = pauseCont.getPauseResource ();
		gameObject.GetComponent<Slider> ().value = pausability;
	}
}
