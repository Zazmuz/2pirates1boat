using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimer : MonoBehaviour
{
    private Canvas canvas;
    private Slider progressBar;
    public GameInformation gameInformation;
    private float maxWater;
    private Transform waterTransform;
    private bool[] shakeTriggered = new bool[5];

    private void Start()
    {
        maxWater = gameInformation.maxWater;
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();
        waterTransform = progressBar.transform;

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

    private void ShowWaterUI(bool isVisible)
    {
        canvas.enabled = isVisible;
        progressBar.enabled = isVisible;
    }

    private void ResetWaterLevel()
    {
        gameInformation.SetCurrentWater(0);
        ShowWaterUI(false);
        StopAllCoroutines();
        shakeTriggered = new bool[5]; // Reset shake triggers
    }

    private IEnumerator ManageWaterLevel()
    {
        while (true)
        {
            if (gameInformation.gameStarted && gameInformation.numberOfHullBreaches > 0)
            {
                gameInformation.ModifyWater(Time.deltaTime * gameInformation.numberOfHullBreaches);
                float waterLevel = gameInformation.GetCurrentWater() / maxWater;
                progressBar.value = waterLevel;

                CheckShake(waterLevel);
            }
            yield return null;
        }
    }

    private void CheckShake(float waterLevel)
    {
        float[] thresholds = { 0.25f, 0.50f, 0.75f, 0.90f, 0.95f };
        for (int i = 0; i < thresholds.Length; i++)
        {
            if (waterLevel >= thresholds[i] && !shakeTriggered[i])
            {
                shakeTriggered[i] = true;
                StartCoroutine(ShakeEffect(0.5f, 0.5f * (i + 1))); // Increase intensity each time
            }
        }
    }

    private IEnumerator ShakeEffect(float duration, float magnitude)
    {
        Vector3 originalPosition = waterTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            waterTransform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        waterTransform.localPosition = originalPosition;
    }
}
