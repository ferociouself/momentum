using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PauseColorController : MonoBehaviour {
	
	// public Color start = new Color(1, 0.5f, 0);
	// public Color end = new Color(0.5f, 0, 0.5f);

	public Color start = new Color(255.0f/255.0f,3.0f/255.0f,48.0f/255.0f);
	public Color end = new Color(30.0f/255.0f,196.0f/255.0f,116.0f/255.0f);


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
		return Color.Lerp (start, end, t);
	}
}
