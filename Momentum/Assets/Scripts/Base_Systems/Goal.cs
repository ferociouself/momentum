using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	bool active = false;
	bool stopped = false;

	bool tripped = false;

	GameObject playerObj;
	Rigidbody2D rb;

	Vector2 pullForce;

    ObjectColor objColor;

	// Use this for initialization
	void Start () {
		playerObj = null;
		pullForce = new Vector2(0.0f, 0.0f);
        objColor = this.GetComponent<ObjectColor>();
        if(objColor == null) {
            Debug.LogError("Goal must have an object color component.");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			// Pull the Player in here

			if (!stopped) {
				StopBall();
			}

			pullForce.Set(gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
				gameObject.transform.localPosition.y - playerObj.transform.localPosition.y);

			float gForce = 100000 / pullForce.sqrMagnitude;
			rb.AddForce(pullForce.normalized * gForce * Time.deltaTime);
			rb.drag = 20*Time.deltaTime;

            // After a timeframe, or once the player is in the center, end the level.
            
			if (Distance(gameObject.transform, playerObj.transform) < 1.0001) {
				playerObj.transform.position = gameObject.transform.position;
				rb.constraints = RigidbodyConstraints2D.FreezePosition;
				StartCoroutine(Wait1Second());
				active = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
        ObjectColor otherColor = coll.gameObject.GetComponent<ObjectColor>();
        if (otherColor != null && objColor.CheckSameColor(otherColor))
        {
            active = true;
            playerObj = coll.gameObject;
            if (playerObj != null)
            {
                rb = playerObj.GetComponent<Rigidbody2D>();
            }
        }
	}

	//void EndLevel() {
	//	UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	//}

	void StopBall() {
        if (rb != null)
        {
            rb.gravityScale = 0;
        }

        MomentumContainer playerMomentumContainerScript = playerObj.GetComponent<MomentumContainer>();
        if (playerMomentumContainerScript != null)
        {
            playerMomentumContainerScript.ZeroMomentum();
            stopped = true;
        }
	}

	float Distance(Transform t1, Transform t2) {
		return (t1.localPosition - t2.localPosition).magnitude;
	}

	IEnumerator Wait1Second() {
		yield return new WaitForSeconds(1);
		tripped = true;
		yield break;
	}

	public bool getActive() {
		return tripped;
	}
}
