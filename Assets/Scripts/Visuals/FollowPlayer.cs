using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;  // Player's transform

    public float heightOffset = 1.5f;  // Height above the player's head (adjust as needed)

    // Update is called once per frame
    void Update()
    {
        // Update the position of the object to match the player's position, but slightly above the player's head
        if (playerTransform != null)
        {
            // Set the object's position with an offset above the player's head
            Vector3 newPosition = playerTransform.position;
            newPosition.y += heightOffset;  // Increase the y-coordinate to move the object higher
            transform.position = newPosition;
        }
    }
}
