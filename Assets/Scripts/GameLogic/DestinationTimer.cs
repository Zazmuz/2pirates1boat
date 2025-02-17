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

        StartCoroutine(FillProgressBar());
    }
    public IEnumerator FillProgressBar(){
        canvas.enabled = true; 
        progressBar.enabled = true;
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
