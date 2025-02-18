using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction _interactAction;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        _interactAction = playerInput.actions["Interact"];
    }
    void Update(){
        if(_interactAction.WasPressedThisFrame()){
            SceneChanger.ChangeScene("MainMenu");
        }
    }
}
