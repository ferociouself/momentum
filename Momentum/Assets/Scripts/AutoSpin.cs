using UnityEngine;
using System.Collections;

public class AutoSpin : MonoBehaviour {

    public float xSpinSpeed = 0; // Degrees per second.
    public float ySpinSpeed = 0; // Degrees per second.
    public float zSpinSpeed = 180.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Fixed Update is called in fixed time intervals
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(xSpinSpeed * Time.deltaTime, ySpinSpeed * Time.deltaTime, zSpinSpeed * Time.deltaTime));
    }
}
