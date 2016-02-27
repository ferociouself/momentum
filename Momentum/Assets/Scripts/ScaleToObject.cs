using UnityEngine;
using System.Collections;

public class ScaleToObject : MonoBehaviour
{

    public GameObject target; // The object whose size to imitate.
    public float sizeRatio = 1.0f; // Desired fraction of other objects size.

    private SpriteRenderer mySpriteRenderer;
    private SpriteRenderer targetSpriteRenderer;

    private float origWidth, origHeight, origDepth;

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
            origWidth = mySpriteRenderer.bounds.size.x;
            origHeight = mySpriteRenderer.bounds.size.y;
            origDepth = mySpriteRenderer.bounds.size.z;
        }

        if (target != null)
        {
            targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (target != null && targetSpriteRenderer == null)
        {
            targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        }
        else if (target == null && targetSpriteRenderer != null)
        {
            targetSpriteRenderer = null;
        }

        if (target != null)
        {
            var targetBounds = targetSpriteRenderer.bounds;

            var widthProportion = (targetBounds.size.x * sizeRatio) / origWidth;
            var heightProportion = (targetBounds.size.y * sizeRatio) / origHeight;
            var depthProportion = (targetBounds.size.z * sizeRatio) / origDepth;

            transform.localScale = new Vector3(widthProportion, heightProportion, depthProportion);
        }
    }
}
