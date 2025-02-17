using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimer : MonoBehaviour
{
    private Canvas canvas;
    private Slider progressBar;
    public GameInformation gameInformation;
    private float maxWater;

    void Start(){
        maxWater = gameInformation.maxWater;

        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        canvas.enabled = true;
        progressBar.enabled = true;
        
    }
    void Update()
    {
        if(gameInformation.gameStarted){
            StartCoroutine(ManageWaterLevel()); 
        }
        if(gameInformation.atDestination){
            gameInformation.SetCurrentWater(0);
            Destroy(gameObject);
        }
    }

    private IEnumerator ManageWaterLevel(){
        while (true)
        {
            if (gameInformation.numberOfHullBreaches > 0){
                gameInformation.ModifyWater(Time.deltaTime * gameInformation.numberOfHullBreaches);
            }

            progressBar.value = gameInformation.GetCurrentWater() / maxWater;

            if (gameInformation.GetCurrentWater() >= maxWater){
                GameOver();
                yield break;
            }
            yield return null;
        }
    }

    private void GameOver(){
        progressBar.enabled = false;
        gameInformation.isGameOver = true;
        Debug.Log("Game Over! The ship is flooded.");
    }
}
