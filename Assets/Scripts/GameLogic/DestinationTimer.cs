using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestinationTimer : GameTimers
{
    private Canvas canvas;
    private Slider progressBar;
    private Coroutine progressCoroutine;
    private float startTime; // Track when the timer starts

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        // Initially hide UI
        canvas.enabled = false;
        progressBar.enabled = false;
    }

    void Update()
    {
        if (gameInformation.gameStarted && !gameInformation.atDestination)
        {
            if (!canvas.enabled)
            {
                canvas.enabled = true;
                progressBar.enabled = true;

                if (progressCoroutine == null)
                {
                    startTime = Time.time; // Reset start time
                    progressCoroutine = StartCoroutine(FillProgressBar());
                }
            }
        }
        else if (gameInformation.atDestination)
        {
            if (progressCoroutine != null)
            {
                StopCoroutine(progressCoroutine);
                progressCoroutine = null;
            }

            canvas.enabled = false;
            progressBar.enabled = false;
        }
    }

    private IEnumerator FillProgressBar()
    {
        while (!gameInformation.atDestination)
        {
            float elapsedTime = Time.time - startTime;
            float dynamicTimeRemaining = gameInformation.timeTilDestination;

            // Ensure we don't divide by zero
            if (dynamicTimeRemaining > 0){
                progressBar.value = Mathf.Clamp01(elapsedTime / dynamicTimeRemaining);
            }

            // If progress bar reaches 100%, set destination reached
            if (progressBar.value >= 1f){
                gameInformation.atDestination = true;
                progressBar.enabled = false;
                progressCoroutine = null;
                yield break; // Stop the coroutine
            }

            yield return null;
        }
    }
}
