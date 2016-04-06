using UnityEngine;
using System.Collections;

public class ObjectPuller : MonoBehaviour {

    GameObject playerObj;
    public float pullForce;
    Vector2 pullVector;
    Rigidbody2D rb;
    bool isActive = false;

	// Use this for initialization
	void Start () {
        pullVector = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(isActive && playerObj != null)
        {
            pullVector.Set(gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, gameObject.transform.localPosition.y - playerObj.transform.localPosition.y);
            rb.AddForce(pullForce * pullVector);
        }
	    else
        {

        }
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        playerObj = coll.gameObject;
        isActive = true;
        rb = playerObj.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        playerObj = null;
        isActive = false;
        rb = null;
    }
}
