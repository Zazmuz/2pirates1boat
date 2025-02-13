using UnityEngine;

[CreateAssetMenu(fileName = "New zone",menuName = "Zone Stats")]
public class ZoneStats : ScriptableObject
{
    [Header("On interact")] //information related to hull breaches
    [Range(1f,10f)] public float interactionTime = 5f;
    public string zoneName;
    public Item requiredItem;
    public Item givesItem;
    public bool needsTwoPlayers;
}
