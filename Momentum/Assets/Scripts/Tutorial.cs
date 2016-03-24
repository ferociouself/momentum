using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	GameObject redBall;
	GameObject greenBall;
	GameObject canvas;
	Text[] textValue;

	// Use this for initialization
	void Start () {
		greenBall = GameObject.Find("circleGreen");
		redBall = GameObject.Find ("circleRed");
		canvas = GameObject.Find("Canvas");
		textValue = canvas.GetComponentsInChildren<Text>();
		Debug.LogError("ayylmao");
		foreach(Text txt in textValue)
		{
			txt.enabled = false;
		}
		StartCoroutine(MyMethod(textValue));
		Debug.LogError ("SSSSS");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator MyMethod(Text[] txt) {
		foreach(Text t in textValue)
		{
			t.enabled = true;
			yield return new WaitForSeconds(2);

		}
	}

}
