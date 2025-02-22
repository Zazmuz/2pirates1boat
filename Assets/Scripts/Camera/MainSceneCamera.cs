using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCamera : MonoBehaviour
{
    public float swaySpeed = 1f;  // Speed of the sway
    public float swayAmount = 2f; // Amount of rotation in degrees
    private float timeOffset;
    public float unitsToShowHorizontally; //40 is good

    void Start(){
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        CameraSway();
        float screenWidth = unitsToShowHorizontally;

        float screenHeight = screenWidth * Screen.height / Screen.width;

        float orthographicSize = screenHeight / 2f;

        Camera.main.orthographicSize = orthographicSize;

        Camera.main.aspect = screenWidth / screenHeight;
    }

    void CameraSway(){
        float roll = Mathf.Sin(Time.time * swaySpeed + timeOffset) * swayAmount;  // Side-to-side tilt
        float pitch = Mathf.Sin((Time.time * swaySpeed * 0.8f) + timeOffset) * (swayAmount * 0.5f); // Forward-backward tilt

        // Apply rotation
        transform.localRotation = Quaternion.Euler(pitch, 0f, roll);
    }
}
