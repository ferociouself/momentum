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

    public float editModeInactiveTrailAlpha = 0; // Alpha of trails when not in edit mode
    public float editModeActiveTrailAlpha = 1; // Alpha of trails in edit mode

    private float transitionTimer = 0.0f;
    private EditModeState editModeState; // Current status of edit mode

	private int pauseCount = 0;
	private bool resourceDrain = false;
	private float pauseResource = 50.0f;

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
            // Don't allow going into edit mode if resource < 10
            //
            if (pauseResource > 10.0f || editModeState == EditModeState.Active || editModeState == EditModeState.Entering)
            {
                ToggleEditMode();
            }
        }
		if (resourceDrain) {
			pauseResource = Mathf.Max(pauseResource - 0.05f, 0.0f);
		} else {
			pauseResource = Mathf.Min(pauseResource + 0.05f, 50.0f);
		}
		//Debug.Log(pauseResource + " " + pauseCount);
		if (pauseResource == 0.0f) {
			BeginEditModeTransition(false);
		}
        UpdateTransitions();
    }

    /// <summary>
    /// If edit mode is active, set to inactive.
    /// If edit mode is inactive, set to active.
    /// </summary>
    public void ToggleEditMode() {
        bool toEditMode = (editModeState == EditModeState.Exiting || editModeState == EditModeState.Inactive);
        BeginEditModeTransition(toEditMode);
    }
    
    /// <summary>
    /// Exit Edit Mode
    /// </summary>
    public void ExitEditMode() {
        if(editModeState == EditModeState.Exiting || editModeState == EditModeState.Inactive) {
            return; // Editmode is already inactive (or moving to inactive).
        }

        BeginEditModeTransition(false);
    }

    /// <summary>
    /// Enter Edit Mode
    /// </summary>
    public void EnterEditMode() {
        if (editModeState == EditModeState.Entering || editModeState == EditModeState.Active)
        {
            return; // Editmode is already active (or moving to active).
        }

        BeginEditModeTransition(true);
    }

    // FixedUpdate is called at regular time intervals
    void UpdateTransitions() {
        // If in transitioning state - lerp to desired state.
        if(editModeState == EditModeState.Entering)
        {
            LerpToMode(EditModeState.Active);
        } else if(editModeState == EditModeState.Exiting)
        {
            LerpToMode(EditModeState.Inactive);
        }
    }

    /// <summary>
    /// Toggle edit mode
    /// </summary>
    void BeginEditModeTransition(bool toEdit)
    {
		resourceDrain = toEdit;
        // Smooth switching when in transition state.
        if (transitionTimer > 0)
        {
            transitionTimer = 1 - transitionTimer;
        }
        else
        {
            transitionTimer = 0;
        }
		if (toEdit) {
			pauseCount++;
			pauseResource -= 5.0f;
		}
        editModeState = (toEdit ? EditModeState.Entering : EditModeState.Exiting);
    }

    /// <summary>
    /// Linear Interpolation Between Edit Modes
    /// </summary>
    /// <param name="initial">Initial Value</param>
    /// <param name="final">Final Value</param>
    /// <param name="totalTime">Time for Interpolation</param>
    /// <param name="destState">Edit Mode State to Enter when Complete</param>
    void LerpToMode(EditModeState destState)
    {
        if(destState != EditModeState.Active && destState != EditModeState.Inactive) {
            Debug.LogError("Error: Calling LerpMode with invalid destination state (" + destState + "). Must call with Active or Inactive.");
            return;
        }

        bool entering = destState == EditModeState.Active;
        float totalTime = (entering ? enterEditModeTime : exitEditModeTime);

        float timeScaleInit = (entering ? 1 : editModeTimeScale);
        float timeScaleFinal = (entering ? editModeTimeScale : 1);

        float alphaInit = (entering ? editModeInactiveTrailAlpha : editModeActiveTrailAlpha);
        float alphaFinal = (entering ? editModeActiveTrailAlpha : editModeInactiveTrailAlpha);

        float timerVal = (totalTime == 0 ? 1 : transitionTimer / totalTime);

        // Alpha
        SetTrailAlphas(Mathf.Lerp(alphaInit, alphaFinal, timerVal));

        // Time Scale
        Time.timeScale = Mathf.Lerp(timeScaleInit, timeScaleFinal, timerVal);

        // Clamping to Final States
        if (transitionTimer >= totalTime)
        {
            Time.timeScale = timeScaleFinal;
            SetTrailAlphas(alphaFinal);
            editModeState = destState;
            transitionTimer = 0;
        }
        else
        {
            transitionTimer += MyTime.deltaTime;
        }
    }

    /// <summary>
    /// Set all trail renderer alphas to the given alpha value
    /// </summary>
    /// <param name="alpha">Alpha value between 0 and 1</param>
    void SetTrailAlphas(float alpha) {
        if (alpha < 0) alpha = 0;
        if (alpha > 1) alpha = 1;

        foreach(TrailRenderer renderer in FindObjectsOfType<TrailRenderer>()) {
            renderer.material.color = new Color(1, 1, 1, alpha);
        }
    }

    /// <summary>
    /// Check if edit mode is active.
    /// </summary>
    public bool EditModeActive() {
        return editModeState == EditModeState.Active;
    }
}
