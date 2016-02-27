﻿using UnityEngine;
using System.Collections;

public class MomentumTransferController : MonoBehaviour
{

    public GameObject sourceTargetIndicatorTemplate; // Template for the object to indicate the selected source target.
    public GameObject destTargetIndicatorTemplate; // Template for the object to indicate the selected destination target.
    public bool onlyAllowTransfersInEditMode = true; // Only allow momentum transfer in edit mode?

    private GameObject sourceTarget; // Source target (null if none).
    private GameObject destTarget; // Destination target (null if none).

    private EditModeScript editModeControllerScript; // Used to determine if in edit mode.
    private GameObject sourceTargetIndicator; // Actual source target indicator (null if no source selected).
    private GameObject destTargetIndicator; // Actual destination target indicator (null if no destination selected).

    // Use this for initialization
    void Start()
    {
        editModeControllerScript = FindObjectOfType<EditModeScript>();
        if (editModeControllerScript == null && onlyAllowTransfersInEditMode)
        {
            Debug.LogError("The Momentum Transfer Controller is set to only allow transfers in edit mode, but it cannot find edit mode controller script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseClick();
        }
    }

    /// <summary>
    /// Handle a mouse click event.
    /// </summary>
    private void HandleMouseClick()
    {
        // Check Edit Mode
        if (onlyAllowTransfersInEditMode && editModeControllerScript != null && !editModeControllerScript.EditModeActive())
        {
            return;
        }

        if (sourceTarget != null && destTarget != null)
        {
            TransferMomentum(sourceTarget, destTarget, GetMomentumTransferAngle());
            ClearTargets();
        }
        else {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 0f);
            if (hit.collider != null)
            {
                HandleMouseClickOnObject(hit.collider.gameObject);
            }
        }
    }

    /// <summary>
    /// Transfer the momentum from the source target to the destination target at the given angle
    /// Dest null will clear momentum from source.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    public void TransferMomentum(GameObject source, GameObject dest, float angle)
    {
        // Check valid parameters.
        if (source == null)
        {
            Debug.LogError("Cannot transfer momentum from null source.");
            return;
        }

        MomentumContainer srcMomentumContainer = source.GetComponent<MomentumContainer>();
        if(srcMomentumContainer == null) {
            Debug.LogError("Transfer momentum source must have a momentum container component.");
            return;
        }

        float momentum = srcMomentumContainer.GetMomentum();
        srcMomentumContainer.ZeroMomentum();

        if(dest != null) {
            // Transfer to destination.
            MomentumContainer destMomentumContainer = dest.GetComponent<MomentumContainer>();
            if(destMomentumContainer == null) {
                Debug.LogWarning("Tranfer Momentum Destination has no momentum container. Cannot tranfer momentum to an object without momentum container.");
                return;
            }
            
            destMomentumContainer.AddMomentum(momentum, angle);
        }
    }

    /// <summary>
    /// Select the given object
    /// </summary>
    void HandleMouseClickOnObject(GameObject obj)
    {
        if (!ValidMomentumObject(obj))
        {
            return;
        }

        if (sourceTarget == null)
        {
            SetSource(obj);
        }
        else {
            SetDest(obj);
        }
    }

    /// <summary>
    /// Check if the given object is valid for momentum tranfer.
    /// </summary>
    private bool ValidMomentumObject(GameObject obj)
    {
        return (obj.GetComponent<MomentumContainer>() != null);
    }

    /// <summary>
    /// Set momentum source target.
    /// </summary>
    private void SetSource(GameObject obj)
    {
        sourceTarget = obj;
        sourceTargetIndicator = (GameObject)Instantiate(sourceTargetIndicatorTemplate, obj.transform.position, Quaternion.identity);
        FollowObject followObjScript = sourceTargetIndicator.GetComponent<FollowObject>();
        if (followObjScript != null)
        {
            followObjScript.SetTarget(obj);
        }
        ScaleToObject scaleObjScript = sourceTargetIndicator.GetComponent<ScaleToObject>();
        if (scaleObjScript != null)
        {
            scaleObjScript.SetTarget(obj);
        }
    }

    /// <summary>
    /// Set momentum destination target.
    /// </summary>
    private void SetDest(GameObject obj)
    {
        // If source is the same as dest, destroy the source indicator.
        if (sourceTarget == destTarget && sourceTargetIndicator != null)
        {
            Destroy(sourceTargetIndicator);
            sourceTargetIndicator = null;
        }

        destTarget = obj;
        destTargetIndicator = (GameObject)Instantiate(destTargetIndicatorTemplate, obj.transform.position, Quaternion.identity);
        FollowObject followObjScript = destTargetIndicator.GetComponent<FollowObject>();
        if (followObjScript != null)
        {
            followObjScript.SetTarget(obj);
        }
        ScaleToObject scaleObjScript = destTargetIndicator.GetComponent<ScaleToObject>();
        if (scaleObjScript != null)
        {
            scaleObjScript.SetTarget(obj);
        }
    }

    /// <summary>
    /// Clear all targets.
    /// </summary>
    private void ClearTargets()
    {
        ClearDest();
        ClearSource();
    }

    /// <summary>
    /// Clear the source target
    /// </summary>
    private void ClearSource()
    {
        if (destTarget != null)
        {
            Debug.LogWarning(
                "Caution: Clearing Source Target While Destination target is still set. "
                + "This could cause problems if destination tries to pull momentum from a nonexistant source."
            );
        }
        if (sourceTargetIndicator != null)
        {
            Destroy(sourceTargetIndicator);
            sourceTargetIndicator = null;
        }
        sourceTarget = null;
    }

    /// <summary>
    /// Clear the dest target
    /// </summary>
    private void ClearDest()
    {
        if (destTargetIndicator != null)
        {
            Destroy(destTargetIndicator);
            destTargetIndicator = null;
        }
        destTarget = null;
    }

    /// <summary>
    /// Get the desired momentum tranfer angle from the destination target indicator.
    /// </summary>
    /// <returns></returns>
    private float GetMomentumTransferAngle()
    {
        if (destTargetIndicator == null)
        {
            return 0.0f; // No indicator to store angle
        }

        LookAtMouse lookAtMouseScript = destTargetIndicator.GetComponent<LookAtMouse>();
        if(lookAtMouseScript == null) {
            return 0.0f; // Destination target indicator is missing the component required for getting the angle.
        }

        return lookAtMouseScript.getAngleFromMouse(true);
    }
}
