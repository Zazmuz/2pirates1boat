using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwoTutorial : TutorialPlayerText
{

    protected override void OnPlayerJoined(PlayerInput input)
    {
        if(sharedDeviceInputManager.playerCount == 2){
            canvas.enabled = true;
        }
        
    }
}
