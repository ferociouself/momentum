using UnityEngine;

public class MyTime : MonoBehaviour {
    
    // Stack Overflow: http://forum.unity3d.com/threads/version-of-deltatime-not-affected-by-time-timescale.211615/

    private static float prevRealTime;
    private static float thisRealTime;

    void Update()
    {
        prevRealTime = thisRealTime;
        thisRealTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Delta Time Independent From Time.TimeScale.
    /// </summary>
    public static float deltaTime
    {
        get
        {
            if (Time.timeScale > 0f) return Time.deltaTime / Time.timeScale; // Take into account time scale
            return Time.realtimeSinceStartup - prevRealTime; // Checks realtimeSinceStartup again because it may have changed since Update was called
        }
    }
}
