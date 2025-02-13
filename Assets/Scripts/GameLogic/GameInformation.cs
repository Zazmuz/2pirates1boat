using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game")]
public class GameInformation : ScriptableObject{
    [Header("Timers")]
    [Range(1f,10f)] public float timeTilDestination = 5f;
    [Range(1f,10f)] public float amountOfWater = 5f;

    [Header("Other shits")]
    public bool isGameOver;

}
