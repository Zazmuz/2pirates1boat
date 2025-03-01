using UnityEngine;

[CreateAssetMenu(menuName = "Player Information")]
public class PlayerInformation : ScriptableObject
{
    [Header("Visuals")]
    public Sprite sprite;
    public Animation idle;
    public Animation running;

    [Header("Position")]
    public float posX;
    public float posY;

    [Header("Gameplay")]
    public float health;
    public float vitaminC;
    public float vitaminCDrainRate = 0.1f;
    
    public void ModifyHealth(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, 100);
    }
    public void ModifyVitaminC(float amount)
    {
        vitaminC += amount;
        vitaminC = Mathf.Clamp(vitaminC, 0, 100);
    }
    public void ResetStats()
    {
        health = 100;
        vitaminC = 100;
    }
    public float GetHealth(){
        return health;
    }
    public float GetVitaminC(){
        return vitaminC;
    }
    



}
