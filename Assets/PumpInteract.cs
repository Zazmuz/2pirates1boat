using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PumpInteract : ZoneBehaviour //not in use at the moment since the mechanic is not fun. Might adjust later.
{
    public GameInformation gameInformation;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    private TextMeshPro textMeshPro;
    private bool isPumping = false;

    void Start(){
        tag = "Interact";
        textMeshPro = GetComponentInChildren<TextMeshPro>();

        textMeshPro.enabled = false;
    }
    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }
        if(currentPlayerInZone != null && currentPlayerInput.InteractIsHeld){
            StartPumping();
        }
    }
    private void StartPumping()
    {
        gameInformation.PlayerAtThePump(true);
        textMeshPro.enabled = true;
        isPumping = true;
    }

    private void Update(){
        if (isPumping){
            gameInformation.UpdateWaterTimer();
        }
        isPumping = false;
    }

    public override void OnLeavingZone(){
        isPumping = false;
        gameInformation.PlayerAtThePump(false);
        textMeshPro.enabled = false;
        currentPlayerInZone = null;
    }

}


