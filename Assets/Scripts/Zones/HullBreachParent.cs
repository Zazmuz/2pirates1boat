using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullBreachParent : MonoBehaviour
{
    public static Transform[] spawnPoints;
    public GameInformation gameInformation;
    public GameObject hullBreachZone;
    private bool isSpawning = false;
    private Dictionary<Transform, GameObject> spawnPointHullBreaches = new Dictionary<Transform, GameObject>();

    // Start is called before the first frame update
    void Start(){
        gameInformation.numberOfHullBreaches = 0;

        InitializeSpawnPoints();
    }

    void Update(){
        if (gameInformation.isSpawningHullBreaches && gameInformation.gameStarted && !isSpawning){
            StartCoroutine(TimeToSpawnNewHullBreach());
        }
        if(gameInformation.atDestination){
            StopCoroutine(TimeToSpawnNewHullBreach());
            RemoveAllBreaches();
        }
    }

    private void InitializeSpawnPoints()
    {
        Grid grid = GameObject.Find("Grid").GetComponent<Grid>();

        if (grid == null){
            return;
        }


        Vector3[] gridPositions = { //this is insane, fuckugly pos can someone do this clean would be sick.
            new Vector3(-11.5f, 1, 0),
            new Vector3(5.5f, 0, 0),
            new Vector3(-11.5f, -4, 0),
            new Vector3(5.5f, -4, 0),
            new Vector3(-6.5f, -4, 0),
            new Vector3(-15f, 5f, 0),
            new Vector3(-7f, 1f, 0)
        };

        spawnPoints = new Transform[gridPositions.Length];


        for (int i = 0; i < gridPositions.Length; i++)
        {
            Debug.Log("SpawnPoint" + (i + 1));
            GameObject spawnPointObj = new GameObject("SpawnPoint" + (i + 1));
            spawnPointObj.transform.position = gridPositions[i];
            spawnPoints[i] = spawnPointObj.transform;
        }
    }

    private IEnumerator TimeToSpawnNewHullBreach(){
        if(gameInformation.gameStarted){
            isSpawning = true;
            yield return new WaitForSeconds(gameInformation.timeTilNewBreach);
            Debug.Log("hey wtf");
            SpawnHullBreach();
            isSpawning = false;
        }
    }

    void SpawnHullBreach()
    {
        GameObject hullBreachParent = GameObject.Find("HullBreachParent");

        List<Transform> availableSpawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in spawnPoints){
            if (!spawnPointHullBreaches.ContainsKey(spawnPoint))
            {
                availableSpawnPoints.Add(spawnPoint);
            }
        }

        if (gameInformation.numberOfHullBreaches < gameInformation.maxHullBreaches && availableSpawnPoints.Count > 0)
        {
            Transform spawnPoint = availableSpawnPoints[UnityEngine.Random.Range(0, availableSpawnPoints.Count)];

            GameObject newBreach = Instantiate(hullBreachZone, spawnPoint.position, Quaternion.identity, hullBreachParent.transform);
            newBreach.SetActive(true);

            spawnPointHullBreaches[spawnPoint] = newBreach;
            gameInformation.numberOfHullBreaches++;

            Debug.Log($"Hull breach spawned at {spawnPoint.position}");
        }
    }
    public void RemoveHullBreach(GameObject breach)
    {
        Transform spawnPointToRemove = null;

        foreach (var entry in spawnPointHullBreaches){
            if (entry.Value == breach){
                spawnPointToRemove = entry.Key;
                break;
            }
        }

        if (spawnPointToRemove != null){
            spawnPointHullBreaches.Remove(spawnPointToRemove);
            Debug.Log("HEY");
            gameInformation.numberOfHullBreaches--;
        }
    }
    public void RemoveAllBreaches(){
        foreach(var i in spawnPointHullBreaches){
            Destroy(i.Value);
        }
        spawnPointHullBreaches.Clear();
        gameInformation.numberOfHullBreaches = 0;
    }
}
