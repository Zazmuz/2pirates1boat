using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public SharedDeviceInputManager sharedDeviceInputManager;

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneChanger.ChangeScene("MainMenu");
        }
    }
}
