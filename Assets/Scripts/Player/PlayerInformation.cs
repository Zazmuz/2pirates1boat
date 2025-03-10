using UnityEngine;

[CreateAssetMenu(menuName = "Player Information")]
public class PlayerInformation : ScriptableObject
{
    [Header("Visuals")]
    public Sprite sprite;
    public Animation idle;
    public Animation running;

    [Header("Gameplay and position")]
    public float posX;
    public float posY;

}
