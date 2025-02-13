using UnityEngine;

[CreateAssetMenu(fileName = "New zone",menuName = "Zone Stats")]
public class ZoneStats : ScriptableObject
{
    [Header("Information")]
    public string zoneName;
    [Range(1f,10f)] public float interactionTime = 5f;
    public Sprite altSprite; //ig fuckup solution to change sprite when adding material or something
    [Header("Items")]
    public Item[] requiredItem;
    public Item givesItem;
    [Header("Co-op")]
    public bool needsTwoPlayers;
}
