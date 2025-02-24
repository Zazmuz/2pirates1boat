using UnityEngine;
using UnityEngine.InputSystem;

public class SwordRack : ZoneBehaviour{ 
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
