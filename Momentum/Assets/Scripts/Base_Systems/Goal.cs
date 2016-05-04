using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    enum GoalState
    {
        NoObject,
        PullingObject,
        LockedObject,
        Tripped
    };

    public float pullStrength = 50.0f; // Will be Multipled by 1000
    public float tripDelay = 1.0f; // Seconds to wait after filling goal and registering the goal as tripped.

    public bool fadeToColOnComplete = true;
    public Color fadeColor = Color.white;
    public float fadeToColTime = 0.5f;
    private float fadeToColTimer = -1.0f;

    public float finalAngularRotation = 0.0f; // Final angular rotation of the balls.
    public float angularSlowdownTime = 1.0f; // Seconds. How long it takes to slow down the ball's rotation.
    private float initialAngularRotation; // Used to store the anguilar rotation when the ball first collides with the goal.
    private float angularSlowTimer = -1.0f; // Current time on slowing down ball.

    private float pullStrengthMagnifier = 1000.0f;
    private GoalState currentState = GoalState.NoObject;
    private bool selectedBallStopped = false;
    

	GameObject selectedObject;
	Rigidbody2D selectedObjectRb;
    SpriteRenderer spriteRend;
    Color initCol;
    OrbitalsScript orbitalScript;

    ObjectColor objColor;
    
	// Use this for initialization
	void Start () {
		selectedObject = null;
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
        // If a selection has been made, but it hasn't yet reached the center, pull it in.
		if (currentState == GoalState.PullingObject) {
            
			if (!selectedBallStopped) {
				ZeroTargetGravity();
			}

			Vector2 pullForceDirection = new Vector2(
                gameObject.transform.localPosition.x - selectedObject.transform.localPosition.x, 
				gameObject.transform.localPosition.y - selectedObject.transform.localPosition.y
                );

            Vector2 correctionForce = pullForceDirection - selectedObjectRb.velocity;
            float correctionForceMagnitude = correctionForce.magnitude;
            
            float magnifiedPullStrength = pullStrength * pullStrengthMagnifier;

            selectedObjectRb.AddForce(pullForceDirection.normalized * magnifiedPullStrength * Time.deltaTime);
            selectedObjectRb.AddForce(correctionForce.normalized * magnifiedPullStrength * Time.deltaTime);
            
            // After a timeframe, or once the player is in the center, register tripped.

            if (StaticMethods.Distance(gameObject.transform, selectedObject.transform) < 1.0001) {
				selectedObject.transform.position = gameObject.transform.position;
				selectedObjectRb.constraints = RigidbodyConstraints2D.FreezePosition;
                EnterLockedObjectState();
			}
		}

        UpdateAngularVelocitySlowdown();

        UpdateFadeToColor();
	}

    /// <summary>
    /// Update the gradual slowdown of the selected objects angular velocity.
    /// </summary>
    void UpdateAngularVelocitySlowdown() {
        if (angularSlowTimer > 0 && selectedObjectRb != null)
        {
            float lerpTime = (angularSlowdownTime - angularSlowTimer) / angularSlowdownTime;
            float newAngularVel = Mathf.Lerp(initialAngularRotation, finalAngularRotation, lerpTime);

            angularSlowTimer -= Time.deltaTime;
            if (angularSlowTimer <= 0)
            {
                angularSlowTimer = -1.0f;
                newAngularVel = finalAngularRotation;
            }

            selectedObjectRb.angularVelocity = newAngularVel;
        }
    }

    /// <summary>
    /// Update the color fade
    /// </summary>
    void UpdateFadeToColor() {
        if (fadeToColTimer > 0)
        {
            float lerpTime = (fadeToColTime - fadeToColTimer) / fadeToColTime;
            float lerpColR = Mathf.Lerp(initCol.r, fadeColor.r, lerpTime);
            float lerpColG = Mathf.Lerp(initCol.g, fadeColor.g, lerpTime);
            float lerpColB = Mathf.Lerp(initCol.b, fadeColor.b, lerpTime);
            float lerpColA = Mathf.Lerp(initCol.a, fadeColor.a, lerpTime);

            Color lerpCol = new Color(lerpColR, lerpColG, lerpColB, lerpColA);
            fadeToColTimer -= MyTime.deltaTime;
            if (fadeToColTimer <= 0)
            {
                lerpCol = fadeColor;
                fadeToColTimer = -1;
            }

            spriteRend.color = lerpCol;
        }
    }

    /// <summary>
    /// Triggered when an object collides with the goal.
    /// </summary>
	void OnTriggerEnter2D(Collider2D coll) {
        if(selectedObjectRb != null) {
            return; // Goal is already filled.
        }

        ObjectColor otherColor = coll.gameObject.GetComponent<ObjectColor>();
        if (otherColor != null && objColor.CheckSameColor(otherColor))
        {
            currentState = GoalState.PullingObject;
            selectedObject = coll.gameObject;
            if (selectedObject != null)
            {
                selectedObjectRb = selectedObject.GetComponent<Rigidbody2D>();
            }
        }
	}

    /// <summary>
    /// Launched once the ball has fixed into position in the center of the goal.
    /// </summary>
    void EnterLockedObjectState() {
        currentState = GoalState.LockedObject; // Change State
        BeginColorFade(); // Fade Color
        BeginAnguilarRotationStopper(); // Stop Angular Momentum
        StartCoroutine(BeginTrippedTimer(tripDelay)); // Start timer to register tripped
    }

    /// <summary>
    /// Begin the fade to the goal's completion color.
    /// </summary>
    void BeginColorFade() {
        if (fadeToColOnComplete)
        {
            fadeToColTimer = fadeToColTime;
        }
    }

    /// <summary>
    /// Stop the angular rotation of the selected object.
    /// </summary>
    void BeginAnguilarRotationStopper() {
        if (selectedObjectRb != null)
        {
            initialAngularRotation = selectedObjectRb.angularVelocity;
        }
    }

    /// <summary>
    /// Zero gravity force on the object.
    /// </summary>
    void ZeroTargetGravity() {
        if (selectedObjectRb != null)
        {
            selectedObjectRb.gravityScale = 0;
        }
        
        selectedBallStopped = true;
	}

    /// <summary>
    /// Starts a countdown to register the goal as tripped.
    /// </summary>
	IEnumerator BeginTrippedTimer(float delay) {
		yield return new WaitForSeconds(delay);
        currentState = GoalState.Tripped;
		yield break;
	}

    /// <summary>
    /// Returns true if the goal is filled.
    /// False otherwise.
    /// </summary>
	public bool getActive() {
        return currentState == GoalState.Tripped;
	}
}
