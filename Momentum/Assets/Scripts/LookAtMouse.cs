using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{

    public float angleOffset = 0.0f; // Angle offset in degrees.

    private float angle; // Angle to mouse

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        positionDiff.Normalize();

        // Get angle from 0
        angle = Mathf.Atan2(positionDiff.y, positionDiff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
    }

    /// <summary>
    /// Get the last recorded angle towards the mouse
    /// </summary>
    /// <param name="includeOffset">Include the assigned offset from mouse position</param>
    /// <returns>Angle to mouse in degrees</returns>
    public float getAngleFromMouse(bool includeOffset)
    {
        return angle + (includeOffset ? 0 : angleOffset);
    }
}
