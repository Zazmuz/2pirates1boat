using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipHealthBar : MonoBehaviour
{
    private Transform player;
    private Vector3 initialLocalScale;
    private float fixedZ;

    void Start()
    {
        player = transform.parent; // Assuming it's a child of the player
        initialLocalScale = transform.localScale; // Store original scale
        fixedZ = transform.position.z; // Store the correct Z position
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Keep the health bar upright even if the player flips
            transform.localScale = new Vector3(Mathf.Abs(player.localScale.x)* 0.13875f, initialLocalScale.y, initialLocalScale.z);

            // Prevent Z-coordinate flipping
            transform.position = new Vector3(transform.position.x, transform.position.y, fixedZ);
        }
    }
}
