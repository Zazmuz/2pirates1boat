using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Game")]
public class GameInformation : ScriptableObject{
    [Header("Timers")]
    [Range(1f,120f)] public float timeTilDestination = 5f;
    [Range(1f,10f)] public float timeTilNewBreach = 5f; //should be randomized every time new breach spawns
    [Header("Hull Breaches")]
    [Range(0,5)]public int numberOfHullBreaches;
    [Range(0,10)]public int maxHullBreaches;
    [Range(1f,100f)] public float maxWater = 100f;
    [Range(1f,100f)] [SerializeField] private float currentWater = 0f;
    [Header("Players")]
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    public bool isSpawningHullBreaches;
    [Header("Other shits")]
    public bool isGameOver;
    public bool atDestination;
    public bool gameStarted;
    public bool timeToGame;
    public bool isCutscene;
    public float GetCurrentWater(){
        return currentWater;
    }
    public void SetCurrentWater(float amount){
        currentWater = amount;
        CheckWaterLevel();
    }
    public void ModifyWater(float amount)
    {
        currentWater += amount;
        currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        CheckWaterLevel();
    }
    public void StartGame(){
        gameStarted = true;
    }
    public void ResetGame(){
        numberOfHullBreaches = 0;
        currentWater = 0f;
        isGameOver = false;
        atDestination = false;
    }
    private void CheckWaterLevel(){
        if (currentWater >= maxWater){
            isGameOver = true;
            LoadGameOverScene();
        }
    }
    public void LoadGameOverScene(){
        SceneChanger.ChangeScene("GameOver");
    }
    public void AddPlayer(GameObject player){
        if (!players.Contains(player)){
            players.Add(player);
        }
    }

    public void RemovePlayer(GameObject player){
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    public List<GameObject> GetPlayers(){
        return new List<GameObject>(players);
    }

}
