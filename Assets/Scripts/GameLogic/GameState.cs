using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameInformation gameInformation;
    private List<PlayerInformation> players = new();
    public Grid grid;
    void Start(){
        gameInformation.ResetGame();
        players = gameInformation.GetPlayers();
        Debug.Log(players);
        if(players != null){
            Instantiate<PlayerInformation>(players[0]);
            Instantiate<PlayerInformation>(players[1]);
        }else
            Debug.LogError("No players in list");
    }
        

        
    
    void Update(){
        if(gameInformation.atDestination){
            Move();
        }
        if(gameInformation.isGameOver){
            
        } //define loss condition. player1 && player2 dead || too much water idk, something else?
    }
    IEnumerator MoveGridOut(){
    Vector2 startGridPos = grid.transform.position;
    Vector2 targetGridPos = new Vector2(-30, -2); //where to move the ship

    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    Vector2[] startPlayerPositions = new Vector2[players.Length];

    for (int i = 0; i < players.Length; i++){ //hacky to get the players to move with the grid
        startPlayerPositions[i] = (Vector2)players[i].transform.position - startGridPos;
    }

    float duration = 2f;
    float elapsedTime = 0f;

    while (elapsedTime < duration){
            float t = elapsedTime / duration;
            t *= t;
            Vector2 newGridPos = Vector2.Lerp(startGridPos, targetGridPos, t);
            grid.transform.position = newGridPos;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = startPlayerPositions[i] + newGridPos;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        grid.transform.position = targetGridPos;
    }


    void Move(){
        AttachPlayersToGrid();
        FindObjectOfType<HullBreachParent>().RemoveAllBreaches();
        StartCoroutine(MoveGridOut());
    }

    void AttachPlayersToGrid(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            player.transform.SetParent(grid.transform);
        }
    }
}
