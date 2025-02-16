using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public Vector2 Movement;

    //booleans to keep track of happenings
    public bool JumpWasPressed;
    public bool JumpIsHeld;
    public bool RunIsHeld;
    public bool JumpWasReleased;
    public bool InteractWasPressed;
    public bool InteractIsHeld;
    public bool DropItemWasPressed;


    //inputaction 
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _interactAction;
    private InputAction _dropItemAction;

    public void Awake(){
        playerInput = GetComponent<PlayerInput>();

        _moveAction = playerInput.actions["Move"];
        _jumpAction = playerInput.actions["Jump"];
        _runAction = playerInput.actions["Run"];
        _interactAction = playerInput.actions["Interact"];
        _dropItemAction = playerInput.actions["Drop Item"];

    }

    // Update is called once per frame
    void Update(){
        Movement = _moveAction.ReadValue<Vector2>();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();

        InteractWasPressed = _interactAction.WasPressedThisFrame();
        InteractIsHeld = _interactAction.IsPressed();

        DropItemWasPressed = _dropItemAction.WasPressedThisFrame();
        
        RunIsHeld = _runAction.IsPressed();
    }
}
