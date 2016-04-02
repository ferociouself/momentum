using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbitalsScript : MonoBehaviour {

    public float orbitalSpeed = 180.0f; // Degrees per second
    public List<GameObject> orbitals; // Horizontal Orbitals

    private Vector3 rotationAxis;

	// Use this for initialization
	void Start () {
        rotationAxis = new Vector3(0, 0, 1); // Rotate around z axis.
	}
	
	// Fixed Update is called in fixed time intervals
	void FixedUpdate () {
        foreach(GameObject orbital in orbitals) {
            orbital.transform.RotateAround(transform.position, rotationAxis, orbitalSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Add an orbital
    /// </summary>
    public void AddOrbital(GameObject obj) {
        orbitals.Add(obj);
    }

    /// <summary>
    /// Remove an orbital (if it exists).
    /// </summary>
    public void RemoveOrbital(GameObject obj) {
        if (orbitals.Contains(obj))
        {
            orbitals.Remove(obj);
        }
    }
}
