using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public MainSceneCamera camera;
    public GameInformation gameInformation;
    public MoveShip moveShip;
    private SharedDeviceInputManager sharedDeviceInputManager;
    public Grid grid;
    public Vector2 startPosition;
    public Vector2 inViewPosition;
    public Vector2 atDestinationPosition;

    public GamePhase currentPhase = GamePhase.Waiting;

    void Awake(){
        gameInformation.ResetGame();
    }

    void Start(){
        sharedDeviceInputManager = GetComponent<SharedDeviceInputManager>();
        sharedDeviceInputManager.enabled = false;

        moveShip.OnShipMovementComplete += HandleShipMovementComplete;

        StartCoroutine(MoveShipInViewStart(0f));
    }

    void Update()
    {
        if (gameInformation.isGameOver)
        {
            // Uncomment this if needed for game over handling.
            // StartCoroutine(HandleGameOver());
        }

        if (currentPhase == GamePhase.Running)
        {
            float swayValue = Mathf.Lerp(camera.swayAmount, 1.5f, Time.deltaTime * 2f);
            if (gameInformation.atDestination)
            {
                StartCoroutine(MoveShipToDestination(0f));
                gameInformation.gameStarted = false;
                gameInformation.isSpawningHullBreaches = false;
                StartCoroutine(StopSway());
                currentPhase = GamePhase.AtDestination;
            }
        }
        if (currentPhase == GamePhase.AtDestination)
        {
            StartCoroutine(MoveShipFromDestination(2f));
        }
    }

    private void HandleShipMovementComplete(){
        if (currentPhase == GamePhase.ShipMovingIn)
        {
            currentPhase = GamePhase.Running;
            StartGame();
        }
        else if (currentPhase == GamePhase.ShipMovingOut)
        {
            currentPhase = GamePhase.AtDestination;
        }
    }

    private void StartGame()
    {
        gameInformation.ResetGame();
        gameInformation.StartGame();
        sharedDeviceInputManager.enabled = true;
        sharedDeviceInputManager.ManuallyJoinPlayer();
        sharedDeviceInputManager.ManuallyJoinPlayer();
    }

    private IEnumerator MoveShipInViewStart(float delay){   
        yield return new WaitForSeconds(delay);
        currentPhase = GamePhase.ShipMovingIn;
        moveShip.Move(startPosition, inViewPosition, 3f);
    }

    private IEnumerator MoveShipToDestination(float delay){
        yield return new WaitForSeconds(delay);
        currentPhase = GamePhase.ShipMovingOut;
        moveShip.Move(inViewPosition, atDestinationPosition, 3f);
    }

    private IEnumerator MoveShipFromDestination(float delay)
    {
        
        if (gameInformation.atDestination && Input.GetKey(KeyCode.Space)){
            currentPhase = GamePhase.ShipMovingIn;
            moveShip.Move(atDestinationPosition, inViewPosition, 3f);
            gameInformation.atDestination = false;
            yield return new WaitForSeconds(delay);
        }
    }

    public enum GamePhase
    {
        Waiting,
        ShipMovingIn,
        Running,
        ShipMovingOut,
        AtDestination
    }
        // Coroutine to gradually reduce sway
    IEnumerator StopSway(){
        float duration = 2f; // Time to stop sway
        float elapsed = 0f;
        float startSway = camera.swayAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            camera.swayAmount = Mathf.Lerp(startSway, 0f, elapsed / duration);
            yield return null;
        }

        camera.swayAmount = 0f; // Ensure it reaches 0 exactly
    }
}
