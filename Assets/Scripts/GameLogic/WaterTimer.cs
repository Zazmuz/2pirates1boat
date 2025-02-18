using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimer : MonoBehaviour
{
    private Canvas canvas;
    private Slider progressBar;
    public GameInformation gameInformation;
    private float maxWater;

    void Start(){
        maxWater = gameInformation.maxWater;

        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        canvas.enabled = true;
        progressBar.enabled = true;
        StartCoroutine(ManageWaterLevel()); 
    }
    void Update(){
        if(gameInformation.atDestination){
            gameInformation.SetCurrentWater(0);
            Destroy(gameObject);
        }
    }

    private IEnumerator ManageWaterLevel(){
        while (true)
        {
            if (gameInformation.numberOfHullBreaches > 0){
                gameInformation.ModifyWater(Time.deltaTime * gameInformation.numberOfHullBreaches);
            }

            progressBar.value = gameInformation.GetCurrentWater() / maxWater;
            yield return null;
        }
    }
}
