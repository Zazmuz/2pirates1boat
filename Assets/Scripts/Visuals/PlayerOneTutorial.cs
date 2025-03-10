using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneTutorial : TutorialPlayerText 
{
    protected override void OnPlayerJoined(PlayerInput input)
    {
        if(sharedDeviceInputManager.playerCount == 1){
            canvas.enabled = true;
        }
        
    }
}
