using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
    public Camera targetCamera;
    public bool matchPosition = false;
    public bool matchRotation = true;

    void Start(){ // attach to any gameobject that shouldnt sway. thinking of ui and water stuff.
        if (targetCamera == null){
            targetCamera = Camera.main;
        }
    }

    void Update(){
        if (targetCamera != null){   
            MatchRotation();
            //MatchPosition();
        }
    }
    void MatchRotation(){
        transform.rotation = targetCamera.transform.rotation;
    }
    void MatchPosition(){
        transform.position = targetCamera.transform.position;
    }
}
