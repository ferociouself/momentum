using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    GameObject playerObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
      
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        playerObj = coll.gameObject;
        Debug.Log("Collision " + playerObj);
        Destroy(playerObj);
        Debug.Log("Destroyed " + playerObj);
        Debug.Log("This is " + gameObject);
    }
}
