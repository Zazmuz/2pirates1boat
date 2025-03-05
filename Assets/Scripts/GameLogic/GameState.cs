using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Canvas victoryCanvas;
    void Awake(){
        gameInformation.ResetGame();
        victoryCanvas.enabled = false;
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
            ShowVictoryScreen();
        }

        if (Input.GetKeyDown(KeyCode.Space)  && (currentPhase == GamePhase.AtDestination || currentPhase == GamePhase.ShipMovingIn))
        {
            RestartGame();
        }
    }

    private void HandleShipMovementComplete()
    {
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

    private IEnumerator MoveShipInViewStart(float delay)
    {   
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

    private void ShowVictoryScreen()
    {
        victoryCanvas.enabled = true;
        //victoryText.text = "You Win!";
    }

    private void RestartGame()
    {   
        Debug.Log("Restarting game");
        Debug.Log("Restarting game");
        Debug.Log("Restarting game");
        Debug.Log("Restarting game");
        Debug.Log("Restarting game");
        Debug.Log("Restarting game");
        


        SceneChanger.ChangeScene("MainMenu");
        Debug.Log("Restarting game");
        //victoryCanvas.enabled = false;
        //ameInformation.ResetGame();
        //moveShip.ResetPosition(startPosition);
        //sharedDeviceInputManager.enabled = false;
        //currentPhase = GamePhase.Waiting;
        //StartCoroutine(MoveShipInViewStart(0f));
    }

    public enum GamePhase
    {
        Waiting,
        ShipMovingIn,
        Running,
        ShipMovingOut,
        AtDestination // Phase for when the ship is at the destination
    }
}
