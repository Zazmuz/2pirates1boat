using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestinationTimer : GameTimers
{
    private float timeTilDestination;
    private Canvas canvas;
    private Slider progressBar;

    void Start()
    {
        timeTilDestination = gameInformation.timeTilDestination;
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        // Initially, hide the UI elements
        canvas.enabled = false;
        progressBar.enabled = false;
    }

    void OnEnable()
    {
        progressBar.value = 0f;
    }

    void Update()
    {
        // Debugging to check if gameStarted is true and the correct flag state
        Debug.Log($"Game Started: {gameInformation.gameStarted}, At Destination: {gameInformation.atDestination}");

        // When the game has started and destination is not reached
        if (gameInformation.gameStarted && !gameInformation.atDestination)
        {
            // Enable the progress bar and start the countdown
            if (!canvas.enabled) // Only enable if not already enabled
            {
                canvas.enabled = true;
                progressBar.enabled = true;
                StartCoroutine(FillProgressBar());
            }
        }
        // When the destination is reached, disable the UI elements
        else if (gameInformation.atDestination)
        {
            StopCoroutine(FillProgressBar());
            canvas.enabled = false;
            progressBar.enabled = false;
        }
    }

    private IEnumerator FillProgressBar()
    {
        // Reset the progress bar each time the round starts
        progressBar.value = 0f;

        float elapsedTime = 0f;

        // Start filling the progress bar
        while (elapsedTime < timeTilDestination)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / timeTilDestination;
            yield return null;
        }

        // Once the progress bar is full, mark the destination as reached
        if (progressBar.value >= 1f)
        {
            progressBar.enabled = false; // Hide the progress bar
            gameInformation.atDestination = true; // Mark that the destination has been reached
        }
    }
}
