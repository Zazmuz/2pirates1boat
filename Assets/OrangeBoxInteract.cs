using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrangeBoxInteract : ZoneBehaviour //not in use at the moment since the mechanic is not fun. Might adjust later.
{
    private Item item;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    private PlayerItemManager playerItemManager;
    
    void Start(){
        tag = "Interact";
        item = zoneStats.givesItem;
    }
    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }
        playerItemManager = currentPlayerInput.GetComponentInParent<PlayerItemManager>();
        
        if(currentPlayerInZone != null && currentPlayerInput.InteractWasPressed){
            Debug.Log(currentPlayerInZone);
            if (playerItemManager != null){
                    playerItemManager.InitializeItem(item);
                    playerItemManager.AddItemToInventory(item);
                    return;
                }
        }
    }
    public override void OnLeavingZone()
    {
        currentPlayerInZone = null;
    }

}


