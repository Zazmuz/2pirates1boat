using UnityEngine;
public class MainMenuImageSway : MonoBehaviour
{
    private Vector2 minPos;
    private Vector2 maxPos;
    private RectTransform rectTransform;
    private float swaySpeed = 0.2f;
    private float swayRange = 5f;
    private float timeCounter = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        minPos = rectTransform.anchoredPosition;
        maxPos = minPos + new Vector2(swayRange, 0);
    }

    void Update()
    {
        timeCounter += Time.deltaTime * swaySpeed;
        float oscillation = Mathf.Sin(timeCounter) * swayRange; 
        rectTransform.anchoredPosition = new Vector2(minPos.x + oscillation, minPos.y);
    }
}
