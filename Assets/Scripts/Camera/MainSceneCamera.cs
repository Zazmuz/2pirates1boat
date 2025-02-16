using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCamera : MonoBehaviour
{
    public float swaySpeed = 1f;  // Speed of the sway
    public float swayAmount = 2f; // Amount of rotation in degrees

    private float timeOffset;

    void Start()
    {
        // Randomize starting offset to prevent synchronized movement in case multiple objects sway
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        CameraSway();
    }

    void CameraSway()
    {
        // Smooth oscillation using sine waves
        float roll = Mathf.Sin(Time.time * swaySpeed + timeOffset) * swayAmount;  // Side-to-side tilt
        float pitch = Mathf.Sin((Time.time * swaySpeed * 0.8f) + timeOffset) * (swayAmount * 0.5f); // Forward-backward tilt

        // Apply rotation
        transform.localRotation = Quaternion.Euler(pitch, 0f, roll);
    }
}
