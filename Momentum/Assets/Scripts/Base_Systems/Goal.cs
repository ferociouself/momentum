using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public bool fadeToColOnComplete = true;
    public Color fadeColor = Color.white;
    public float pullStrength = 50.0f; // Will be Multipled by 1000
    private float fadeToColTimeMax = 0.5f;
    private float fadeToColTimer = -1.0f;
    private float pullStrengthMagnifier = 1000.0f;

    bool active = false;
	bool stopped = false;

	bool tripped = false;

	GameObject playerObj;
	Rigidbody2D rb;
    SpriteRenderer spriteRend;
    Color initCol;
    OrbitalsScript orbitalScript;

    ObjectColor objColor;
    

	// Use this for initialization
	void Start () {
		playerObj = null;
        objColor = this.GetComponent<ObjectColor>();
        spriteRend = this.GetComponent<SpriteRenderer>();
        orbitalScript = this.GetComponent<OrbitalsScript>();
        initCol = spriteRend.color;
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

			Vector2 pullForceDirection = new Vector2(
                gameObject.transform.localPosition.x - playerObj.transform.localPosition.x, 
				gameObject.transform.localPosition.y - playerObj.transform.localPosition.y
                );

            Vector2 correctionForce = pullForceDirection - rb.velocity;
            float correctionForceMagnitude = correctionForce.magnitude;
            
            float magnifiedPullStrength = pullStrength * pullStrengthMagnifier;

            rb.AddForce(pullForceDirection.normalized * magnifiedPullStrength * Time.deltaTime);
            rb.AddForce(correctionForce.normalized * magnifiedPullStrength * Time.deltaTime);
            
            // After a timeframe, or once the player is in the center, end the level.

            if (Distance(gameObject.transform, playerObj.transform) < 1.0001) {
				playerObj.transform.position = gameObject.transform.position;
				rb.constraints = RigidbodyConstraints2D.FreezePosition;
				StartCoroutine(Wait1Second());
				active = false;

                if(fadeToColOnComplete) {
                    fadeToColTimer = fadeToColTimeMax;
                }
			}
		}

        if(fadeToColTimer > 0) {
            float lerpTime = (fadeToColTimeMax - fadeToColTimer) / fadeToColTimeMax;
            float lerpColR = Mathf.Lerp(initCol.r, fadeColor.r, lerpTime);
            float lerpColG = Mathf.Lerp(initCol.g, fadeColor.g, lerpTime);
            float lerpColB = Mathf.Lerp(initCol.b, fadeColor.b, lerpTime);
            float lerpColA = Mathf.Lerp(initCol.a, fadeColor.a, lerpTime);

            Color lerpCol = new Color(lerpColR, lerpColG, lerpColB, lerpColA);
            spriteRend.color = lerpCol;
            fadeToColTimer -= MyTime.deltaTime;
            if(fadeToColTimer <= 0) {
                fadeToColTimer = -1;
            }
        }
	}

	void OnTriggerEnter2D(Collider2D coll) {
        if(rb != null) {
            return; // Goal is already filled.
        }

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
            // playerMomentumContainerScript.ZeroMomentum();
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
