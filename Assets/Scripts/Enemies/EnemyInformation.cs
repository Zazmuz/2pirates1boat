using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

[CreateAssetMenu(menuName = "Enemy Information")]
public class EnemyInformation : ScriptableObject
{
    [Header("Visuals")]
    public Sprite sprite;
    public Animation idle;
    public Animation running;

    [Header("Position")]
    public float posX; //start pos
    public float posY;

    [Header("Gameplay")]
    [Range(0f,100f)] public float maxHealth;
    [Range(0f, 100f)] public float health = 100;

    public void TakeDamage(float amount){
        health -= amount;
    }

}
