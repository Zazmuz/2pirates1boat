using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectState : MonoBehaviour
{
    public SharedDeviceInputManager sharedDeviceInputManager;

    // Update is called once per frame
    void Update(){
        if(sharedDeviceInputManager.playerCount >= 1 && Input.GetKeyDown(KeyCode.Space)){
            SceneChanger.ChangeScene("Game");
        }
    }
}
