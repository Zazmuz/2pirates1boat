using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game")]
public class GameInformation : ScriptableObject{
    [Header("Timers")]
    [Range(1f,10f)] public float timeTilDestination = 5f;
    [Range(1f,10f)] public float timeTilNewBreach = 5f; //should be randomized every time new breach spawns
    

    [Header("Hull Breaches")]
    [Range(0,10)]public int numberOfHullBreaches;
    [Range(0,10)]public int maxHullBreaches;
    [Range(1f,10f)] public float amountOfWater = 0f;
    public bool isSpawningHullBreaches;
    [Header("Other shits")]
    public bool isGameOver;
    

}
