using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseColorController : MonoBehaviour {

	private PauseResourceController pauseCont;
	private float pausability;
	// Use this for initialization
	void Start () {
		pauseCont = GameObject.Find ("EditModeController").GetComponent<PauseResourceController> ();
	}
	
	// Update is called once per frame
	void Update () {
		pausability = pauseCont.getPauseResource ();
		Color c = colorFunc (pausability);
		gameObject.GetComponent<Image>().color = c;
	}
	Color colorFunc(float pause){
		float t = pause / 50.0f;
		return Color.Lerp (Color.red, Color.green, t);
	}
}
