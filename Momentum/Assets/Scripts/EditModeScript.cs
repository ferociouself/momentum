using UnityEngine;
using System.Collections;

public class EditModeScript : MonoBehaviour {
    enum EditModeState
    {
        Inactive, // Edit Mode State is Disabled (Normal Game Time Scale)
        Entering, // Entering Edit Mode (Switching to Edit Mode Time Scale)
        Active, // Edit Mode State is Active (Edit Mode Time Scale)
        Exiting // Exiting Edit Mode (Switching to Normal Game Time Scale)
    };

    public bool editModeOnStart = true; // Enter edit mode in start
    public float enterEditModeTime = 0.15f; // Time it takes to pause
    public float exitEditModeTime = 0.15f; // Time it takes to unpause
    public float editModeTimeScale = 0.0f; // Timescale in edit mode

    private float transitionTimer = 0.0f;
    private EditModeState editModeState; // Current status of edit mode

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
        if(editModeOnStart) {
            editModeState = EditModeState.Active;
            Time.timeScale = editModeTimeScale;    
        } else {
            editModeState = EditModeState.Inactive;
            Time.timeScale = 1;
        }
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () {
	    if(Input.GetButtonDown("Pause"))
        {
            // Switch to edit mode if current mode is inactive or leaving.
            // Switch away from edit mode if current mode is active or entering.
            //
            bool toEditMode = (editModeState == EditModeState.Exiting || editModeState == EditModeState.Inactive);
            BeginEditModeTransition(toEditMode);
        }
        UpdateTransitions();
    }

    // FixedUpdate is called at regular time intervals
    void UpdateTransitions() {
        // Debug.Log("State: " + enterEditModeTime + " Transition Time: " + transitionTimer);
        if(editModeState == EditModeState.Entering)
        {
            float timerVal;
            if (enterEditModeTime == 0)
            {
                timerVal = 1;
            }
            else
            {
                timerVal = transitionTimer / enterEditModeTime;
            }
            Time.timeScale = Mathf.Lerp(1, editModeTimeScale, timerVal);
            if(transitionTimer >= enterEditModeTime)
            {
                Time.timeScale = editModeTimeScale;
                editModeState = EditModeState.Active;
                transitionTimer = 0;
            }
            else
            {
                transitionTimer += MyTime.deltaTime;
            }
        }
        else if (editModeState == EditModeState.Exiting)
        {
            float timerVal;
            if(exitEditModeTime == 0) {
                timerVal = 1;
            } else {
                timerVal = transitionTimer / exitEditModeTime;
            }
            Time.timeScale = Mathf.Lerp(editModeTimeScale, 1, timerVal);
            if (transitionTimer >= exitEditModeTime)
            {
                Time.timeScale = 1;
                editModeState = EditModeState.Inactive;
                transitionTimer = 0;
            }
            else
            {
                transitionTimer += MyTime.deltaTime;
            }
        }
    }

    /// <summary>
    /// Toggle edit mode
    /// </summary>
    void BeginEditModeTransition(bool toEdit)
    {
        // Smooth switching when in transition state.
        if (transitionTimer > 0)
        {
            transitionTimer = 1 - transitionTimer;
        }
        else
        {
            transitionTimer = 0;
        }
        editModeState = (toEdit ? EditModeState.Entering : EditModeState.Exiting);
    }
}
