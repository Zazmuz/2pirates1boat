using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
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
            if (gameInformation.atDestination)
            {
                StartCoroutine(MoveShipToDestination(0f));
                gameInformation.gameStarted = false;
                gameInformation.isSpawningHullBreaches = false;
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
}
