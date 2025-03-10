using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPlayerText : MonoBehaviour
{
    public Transform playerSpawn;
    public SharedDeviceInputManager sharedDeviceInputManager;
    protected Canvas canvas;
    private RectTransform rectTransform;

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;

        rectTransform = GetComponent<RectTransform>();
        
    }
    void Update(){
        rectTransform.position = new Vector2(playerSpawn.position.x, playerSpawn.position.y + 2f);
        
    }
    void OnEnable()
    {
        sharedDeviceInputManager.onPlayerJoined += OnPlayerJoined;
    }

    void OnDisable()
    {
        sharedDeviceInputManager.onPlayerJoined -= OnPlayerJoined;
    }

    protected virtual void OnPlayerJoined(PlayerInput input)
    {
        canvas.enabled = true;
    }
}
