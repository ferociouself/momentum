using UnityEngine;
using System.Collections;

public class MomentumTransferController : MonoBehaviour
{

    public GameObject sourceTargetIndicatorTemplate; // Template for the object to indicate the selected source target.
    public GameObject destTargetIndicatorTemplate; // Template for the object to indicate the selected destination target.

    private GameObject sourceTarget; // Source target (null if none).
    private GameObject destTarget; // Destination target (null if none).

    private GameObject sourceTargetIndicator; // Actual source target indicator (null if no source selected).
    private GameObject destTargetIndicator; // Actual destination target indicator (null if no destination selected).

    // Use this for initialization
    void Start()
    {

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
    private void HandleMouseClick() {
        if (sourceTarget != null && destTarget != null)
        {
            ClearTargets();
        } else {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 0f);
            if (hit.collider != null)
            {
                HandleMouseClickOnObject(hit.collider.gameObject);
            }
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
        return true;
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
}
