using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

    void Awake()
    {
        // Initialize the game state defaults when the scene is loaded
        gameInformation.ResetGame();  // Reset all game-related variables to default
        gameInformation.gameStarted = false; // Set gameStarted to false by default
        gameInformation.timeToGame = false;  // Ensure timeToGame is initially false
        gameInformation.isGameOver = false;  // Set isGameOver to false when loading the scene
        gameInformation.atDestination = false;  // Ensure this flag is false as well
        Debug.Log("Game state initialized.");
    }
    void Start(){
        sharedDeviceInputManager = GetComponent<SharedDeviceInputManager>();
        sharedDeviceInputManager.enabled = false;
        MoveShipInView();
        
    }
    void Update(){
        if(gameInformation.atDestination){
            MoveShipOutOfView();
            StartCoroutine(NextScene("Game"));
        }
        if (gameInformation.timeToGame && !gameInformation.gameStarted){
            gameInformation.StartGame();
            gameInformation.timeToGame = false;
        }
        if(gameInformation.gameStarted){
            sharedDeviceInputManager.enabled = true;
            StartRound();
        }
        
        if(gameInformation.isGameOver){
            
        } //define loss condition. player1 && player2 dead || too much water idk, something else?
    }
    private void StartRound(){ 
        sharedDeviceInputManager.ManuallyJoinPlayer();
        sharedDeviceInputManager.ManuallyJoinPlayer();

    }
    private void MoveShipInView(){
        moveShip.Move(startPosition, inViewPosition, 3f);
    }
    private void MoveShipOutOfView(){
        moveShip.Move(inViewPosition, atDestinationPosition, 3f);
    }
    
    private IEnumerator NextScene(string scene){
        yield return new WaitForSeconds(12);
        SceneChanger.ChangeScene(scene); 
    }
}
