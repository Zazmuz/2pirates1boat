using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimer : MonoBehaviour
{
    private Canvas canvas;
    private Slider progressBar;
    public GameInformation gameInformation;
    private float maxWater;

    private void Start()
    {
        maxWater = gameInformation.maxWater;
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        canvas.enabled = false;
        progressBar.enabled = false;

        StartCoroutine(ManageWaterLevel());
    }

    private void Update()
    {
        if (gameInformation.gameStarted)
        {
            ShowWaterUI(true);
        }
        else
        {
            ShowWaterUI(false);
        }

        if (gameInformation.atDestination)
        {
            ResetWaterLevel();
        }
    }

    private void ShowWaterUI(bool isVisible){
        canvas.enabled = isVisible;
        progressBar.enabled = isVisible;
    }

    private void ResetWaterLevel()
    {
        gameInformation.SetCurrentWater(0);
        ShowWaterUI(false);
        StopAllCoroutines();
    }

    private IEnumerator ManageWaterLevel()
    {
        while (true)
        {
            if (gameInformation.gameStarted && gameInformation.numberOfHullBreaches > 0)
            {
                gameInformation.ModifyWater(Time.deltaTime * gameInformation.numberOfHullBreaches);
                progressBar.value = gameInformation.GetCurrentWater() / maxWater;
            }
            yield return null;
        }
    }
}
