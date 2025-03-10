using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSway : MonoBehaviour
{
    private Vector2 startPos;
    private RectTransform rectTransform;
    public float swaySpeed;
    public float swayRange;
    public float timeCounter;
    public float randomOffset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        swaySpeed = Random.Range(1.5f, 3f); 
        swayRange = Random.Range(0.05f, 0.15f);
        timeCounter = 0f;
        randomOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void Update()
    {
        timeCounter += Time.deltaTime * swaySpeed;
        float oscillation = Mathf.Sin(timeCounter + randomOffset) * swayRange;

        float xVariation = oscillation + Random.Range(-0.005f, 0.005f);
        float yVariation = oscillation * 0.5f + Random.Range(-0.002f, 0.002f);

        rectTransform.anchoredPosition = new Vector2(startPos.x + xVariation, startPos.y + yVariation);
    }
}
