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

}
