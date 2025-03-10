using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterFill : MonoBehaviour
{
    public float minFillY = -10f;

    public float maxFillY = -1.6f;

    public float fillSpeed = 3f;

    public bool enableWave = true;

    public float waveAmplitude = 0.05f;

    public float waveFrequency = 2f;

    public bool scrollTexture = true;

    public float scrollSpeed = 0.5f;

    private SpriteRenderer spriteRenderer;
    private float currentFill = 0f;
    private float targetFill = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * fillSpeed);

        float newY = Mathf.Lerp(minFillY, maxFillY, currentFill);
        Vector3 pos = transform.localPosition;

        if (enableWave)
        {
            newY += Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        }

        transform.localPosition = new Vector3(pos.x, newY, pos.z);

        if (scrollTexture && spriteRenderer.material.HasProperty("_MainTex"))
        {
            Vector2 offset = spriteRenderer.material.mainTextureOffset;
            offset.x += Time.deltaTime * scrollSpeed;
            spriteRenderer.material.mainTextureOffset = offset;
        }
    }

    public void SetFillAmount(float fillAmount)
    {
        targetFill = Mathf.Clamp01(fillAmount);
    }
}
