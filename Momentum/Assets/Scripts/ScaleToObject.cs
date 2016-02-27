using UnityEngine;
using System.Collections;

public class ScaleToObject : MonoBehaviour
{

    public GameObject initialTarget; // The initial target object (null if none).
    public float sizeRatio = 1.0f; // Desired fraction of other objects size.

    private GameObject currentTagret; // The current target object (null if none).
    private SpriteRenderer mySpriteRenderer; // My Sprite Renderer.
    private SpriteRenderer targetSpriteRenderer; // Sprite Renderer of target.
    private float origWidth, origHeight, origDepth; // Original dimensions of this object.

    /// <summary>
    ///  Use this for initialization
    /// </summary>
    void Start()
    {
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();

        if (mySpriteRenderer == null)
        {
            Debug.LogError("Scale to Object Script Requires Sprite Renderer.");
            origWidth = 1;
            origHeight = 1;
            origDepth = 1;
        }
        else {
            origWidth = mySpriteRenderer.bounds.size.x / mySpriteRenderer.transform.lossyScale.x;
            origHeight = mySpriteRenderer.bounds.size.y / mySpriteRenderer.transform.lossyScale.y;
            origDepth = mySpriteRenderer.bounds.size.z / mySpriteRenderer.transform.lossyScale.z;
        }

        if (initialTarget != null)
        {
            SetTarget(initialTarget);
        }
    }

    /// <summary>
    /// Set the object whose size this object should imitate.
    /// Null clears target.
    /// </summary>
    public void SetTarget(GameObject obj) {
        currentTagret = obj;
        if (currentTagret != null)
        {
            targetSpriteRenderer = currentTagret.GetComponent<SpriteRenderer>();
        } else {
            targetSpriteRenderer = null;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (currentTagret != null && targetSpriteRenderer == null)
        {
            targetSpriteRenderer = currentTagret.GetComponent<SpriteRenderer>();
        }
        else if (currentTagret == null && targetSpriteRenderer != null)
        {
            targetSpriteRenderer = null;
        }

        if (targetSpriteRenderer != null)
        {
            var targetBounds = targetSpriteRenderer.bounds;

            var widthProportion = (targetBounds.size.x * sizeRatio) / origWidth;
            var heightProportion = (targetBounds.size.y * sizeRatio) / origHeight;
            var depthProportion = (targetBounds.size.z * sizeRatio) / origDepth;

            transform.localScale = new Vector3(widthProportion, heightProportion, depthProportion);
        }
    }
}
