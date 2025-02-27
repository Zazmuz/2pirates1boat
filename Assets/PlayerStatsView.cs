using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsView : MonoBehaviour
{
    public GameInformation gameInformation;
    public PlayerInformation playerInformation;
    public Slider health;
    public Slider vitaminC;
    void Update(){
        health.value = Mathf.Lerp(health.value,playerInformation.GetHealth(), 0.1f * Time.deltaTime);
        vitaminC.value = Mathf.Lerp(vitaminC.value,playerInformation.GetVitaminC(), 0.1f * Time.deltaTime);
    }
}
