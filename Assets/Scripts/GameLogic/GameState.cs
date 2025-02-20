using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameInformation gameInformation;
    private List<GameObject> players = new();
    public Grid grid;
    public Vector2 startPosition;
    public Vector2 inViewPosition;
    public Vector2 atDestinationPosition;
    private string sceneName;
    private bool timeToGame = false; // jag vill flå mig själv

    private void StartRound(){
        
        gameInformation.ResetGame();
        players = gameInformation.GetPlayers();
        //Debug.Log(players);
        if(players != null){
            //Instantiate<GameObject>(players[0]);
            //Instantiate<GameObject>(players[1]);
        }else
            Debug.LogError("No players in list");
        Move(startPosition, inViewPosition, 3f);

    }
    void Start(){
        Debug.Log(SceneManager.GetActiveScene());
        sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Game"){
            StartRound();
        }
        
    }

    void Update(){
        if(gameInformation.atDestination){
            Move(inViewPosition, atDestinationPosition, 6f);
            StartCoroutine(NextScene("Game"));
        }
          if(timeToGame && !gameInformation.gameStarted){
            Debug.Log("We are starting the game");
            gameInformation.StartGame();
            timeToGame = false;
            
        }
        
        if(gameInformation.isGameOver){
            
        } //define loss condition. player1 && player2 dead || too much water idk, something else?
    }
    IEnumerator MoveGridOut(Vector2 startGridPos, Vector2 targetGridPos, float duration)
    {
        grid.transform.position = startGridPos;
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector2[] startPlayerPositions = new Vector2[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            startPlayerPositions[i] = (Vector2)players[i].transform.position - startGridPos;
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t; // Quadratic easing for acceleration effect
            Vector2 newGridPos = Vector2.Lerp(startGridPos, targetGridPos, t);
            grid.transform.position = newGridPos;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = startPlayerPositions[i] + newGridPos;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        timeToGame = true;
        grid.transform.position = targetGridPos;
        
    }
    void Move(Vector2 from, Vector2 to, float duration){
        AttachPlayersToGrid();
        FindObjectOfType<HullBreachParent>().RemoveAllBreaches();
        StartCoroutine(MoveGridOut(from, to, duration));
    }

    void AttachPlayersToGrid(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players){
            player.transform.SetParent(grid.transform);
        }
    }

    private IEnumerator NextScene(string scene){
        yield return new WaitForSeconds(12);
        SceneChanger.ChangeScene(scene); 
    }
}
