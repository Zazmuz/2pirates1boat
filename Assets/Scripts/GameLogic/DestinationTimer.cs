using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestinationTimer : GameTimers
{
    private float timeTilDestination;
    private Canvas canvas;
    private Slider progressBar;

    void Start(){
        timeTilDestination = gameInformation.timeTilDestination;
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();
        canvas.enabled = false; 
        progressBar.enabled = false;
        
    }
    void Update()
    {
        if(gameInformation.gameStarted){
            StartCoroutine(FillProgressBar());  
            canvas.enabled = true; 
            progressBar.enabled = true;
        }else if(gameInformation.atDestination){
            StopCoroutine(FillProgressBar());
            canvas.enabled = false; 
            progressBar.enabled = false;
        }
        
    }
    private IEnumerator FillProgressBar(){
        
        float elapsedTime = 0f;
        progressBar.value = 0f;

        while (elapsedTime < timeTilDestination){
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / timeTilDestination;
            yield return null;
        }
        if(progressBar.value == 1){
            progressBar.enabled = false;
            Destroy(gameObject);
        }
        gameInformation.atDestination = true;

    }
}
