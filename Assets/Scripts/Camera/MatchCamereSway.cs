using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
    public Camera targetCamera;

    void Start(){ // attach to any gameobject that shouldnt sway. thinking of ui and water stuff.
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update(){
        if (targetCamera != null)
        {   
            MatchRotation();
        }
    }
    void MatchRotation(){
        transform.rotation = targetCamera.transform.rotation;
    }
}
