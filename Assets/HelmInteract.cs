using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelmInteract : ZoneBehaviour
{
    public GameInformation gameInformation;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    private TextMeshPro textMeshPro;

    void Start(){
        tag = "Interact";
        textMeshPro = GetComponentInChildren<TextMeshPro>();

        gameInformation.PlayerAtTheHelm(false);
        textMeshPro.enabled = false;
    }
    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }

        if(currentPlayerInZone != null && currentPlayerInput.InteractIsHeld){
            gameInformation.PlayerAtTheHelm(true);
            textMeshPro.enabled = true;
            
        }else{
            gameInformation.PlayerAtTheHelm(false);
            textMeshPro.enabled = false;
        }
    }
    void Update(){
        gameInformation.UpdateTimer();
    }

    public override void OnLeavingZone(){
        currentPlayerInZone = null;
    }

}


