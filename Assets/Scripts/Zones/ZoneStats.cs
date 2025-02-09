using UnityEngine;

[CreateAssetMenu(menuName = "Zone Stats")]
public class ZoneStats : ScriptableObject
{
    [Header("Gold Zone")] //testing zone to make sure interactable zones are kinda working
    [Range(1f,10f)] public float goldZoneInteractionTime = 3f;   

    [Header("Hull Breach Zone")] //information related to hull breaches
    [Range(1f,10f)] public float hullBreachInteractionTime = 5f;


}
