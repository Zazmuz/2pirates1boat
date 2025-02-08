using UnityEngine;

[CreateAssetMenu(menuName = "Zone Stats")]
public class ZoneStats : ScriptableObject
{
    [Header("Gold Zone")]
    [Range(1f,10f)] public float goldZoneInteractionTime = 3f;    
}
