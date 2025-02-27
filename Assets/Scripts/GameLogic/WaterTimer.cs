using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimer : MonoBehaviour
{
    private Canvas canvas;
    private Slider progressBar;
    public GameInformation gameInformation;
    private float maxWater;
    private Coroutine progressCoroutine; // Fixed type (was object)
    private float startTime;

    private void Start()
    {
        maxWater = gameInformation.maxWater;
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        canvas.enabled = false;
        progressBar.enabled = false;
    }

    private void Update()
    {
        if (gameInformation.gameStarted)
        {
            ShowWaterUI(true);
            if (progressCoroutine == null){ // Ensure only one coroutine runs
                progressCoroutine = StartCoroutine(ManageWaterLevel());
            }
        }
        else
        {
            ShowWaterUI(false);
            StopWaterLevelCoroutine();
        }

        if (gameInformation.atDestination)
        {
            ResetWaterLevel();
        }
    }

    private void ShowWaterUI(bool isVisible)
    {
        canvas.enabled = isVisible;
        progressBar.enabled = isVisible;
    }

    private void ResetWaterLevel()
    {
        gameInformation.SetCurrentWater(0);
        ShowWaterUI(false);
        StopWaterLevelCoroutine();
    }

    private void StopWaterLevelCoroutine()
    {
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
            progressCoroutine = null;
        }
    }

    private IEnumerator ManageWaterLevel()
    {
        while (!gameInformation.atDestination)
        {
            float dynamicWaterRemaining = gameInformation.GetCurrentWater();
            progressBar.value = dynamicWaterRemaining / maxWater;

            if (gameInformation.gameStarted && gameInformation.numberOfHullBreaches > 0)
            {
                gameInformation.ModifyWater(Time.deltaTime * gameInformation.numberOfHullBreaches);
            }

            if (progressBar.value >= 1f)
            {
                progressBar.enabled = false;
                StopWaterLevelCoroutine();
                yield break;
            }

            yield return null;
        }
    }
}
