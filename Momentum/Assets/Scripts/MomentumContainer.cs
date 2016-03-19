using UnityEngine;
using System.Collections;

public class MomentumContainer : MonoBehaviour {

    Rigidbody2D myRigidBody;
    
    // Use this for initialization
    void Start() {
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        if (myRigidBody == null) {
            Debug.LogError("Momentum Container could not find RigidBody2D. Cannot have momentum withouut RigidBody2D.");
        }
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// Get the momentum of the object.
    /// </summary>
    public float GetMomentum() {
        return myRigidBody.mass * myRigidBody.velocity.magnitude;
    }

    /// <summary>
    /// Add a momentum change at the given magnitude and angle (degrees)
    /// </summary>
    public void AddMomentum(float magnitude, float angle) {
        float addVelocityMagnitude = magnitude / myRigidBody.mass;
        float angleInRad = angle * Mathf.Deg2Rad;
        Vector2 newMomentum = new Vector2(addVelocityMagnitude * Mathf.Cos(angleInRad), addVelocityMagnitude * Mathf.Sin(angleInRad));
        myRigidBody.velocity += newMomentum;
    }

    /// <summary>
    /// Clear the momentum of this object.
    /// </summary>
    public void ZeroMomentum() {
        myRigidBody.velocity = Vector2.zero; // Clear source destination velocity.
        myRigidBody.angularVelocity = 0.0f; // Clear source angular velocity.
    }
}
