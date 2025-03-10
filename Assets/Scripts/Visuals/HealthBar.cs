using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private RectTransform greenBarRect;

    void Start()
    {
        // Try to find the green bar among all children recursively
        FindGreenBarRecursively(transform);

        // Ensure that the green bar was found
        if (greenBarRect == null)
        {
            Debug.LogError("Green bar not found in the hierarchy!");
        }
    }

    void Update()
    {
        if (greenBarRect != null)
        {
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // Calculate the percentage of current HP
        float hpPercentage = playerHealth.currentHealth / playerHealth.maxhealth;

        // Update the green bar width based on HP
        greenBarRect.localScale = new Vector3(hpPercentage, 1f, 1f);

        // Optionally change the color based on HP
        UnityEngine.UI.Image greenBarImage = greenBarRect.GetComponent<UnityEngine.UI.Image>();
        if (greenBarImage != null)
        {
            greenBarImage.color = Color.green; // Default color
        }
    }

    // Recursively search for the green bar among all descendants
    void FindGreenBarRecursively(Transform parent)
    {
        // Check all children of the parent GameObject
        foreach (Transform child in parent)
        {
            Debug.Log(child.name.ToLower());
            Debug.Log(child.name.ToLower().Contains("green"));
            // If the child is the green bar (based on its name containing "green")
            if (child.name.ToLower().Contains("green"))
            {
                greenBarRect = child.GetComponent<RectTransform>();
                return;
            }

            // Recursively check the child's children
            FindGreenBarRecursively(child);
            if (greenBarRect != null)
            {
                return;
            }
        }
    }
}
