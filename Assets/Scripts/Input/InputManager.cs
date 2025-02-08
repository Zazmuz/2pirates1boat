using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;

    public static Vector2 Movement;

    //booleans to keep track of happenings
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool RunIsHeld;
    public static bool JumpWasReleased;
    public static bool InteractWasPressed;
    public static bool InteractIsHeld;


    //inputaction 
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _interactAction;

    public void Awake(){
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        _interactAction = PlayerInput.actions["Interact"];

    }

    // Update is called once per frame
    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();

        InteractWasPressed = _interactAction.WasPressedThisFrame();
        InteractIsHeld = _interactAction.IsPressed();
        
        RunIsHeld = _runAction.IsPressed();
    }
}
