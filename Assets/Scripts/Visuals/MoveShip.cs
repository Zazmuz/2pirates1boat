using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public Grid grid;
    public GameInformation gameInformation;
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
        
        gameInformation.timeToGame = true;
        grid.transform.position = targetGridPos;
        
    }
    public void Move(Vector2 from, Vector2 to, float duration){
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

}
