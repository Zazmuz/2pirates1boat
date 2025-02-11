using UnityEngine;
using UnityEngine.InputSystem;

public class PlankZone : ZoneBehaviour{
    public Item item;
    private PlayerItemManager player;
    private InputManager currentPlayerInput; //to check which player is in the zone :3
    
    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInput.InteractWasPressed){
            if (player != null){
                    player.InitializeItem(item);
                    player.AddItemToInventory(item);
                    return;
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        Transform currentTransform = other.transform;

        while (currentTransform != null){
            player = currentTransform.GetComponentInChildren<PlayerItemManager>(); //check if there's itemManager in the children.
            currentPlayerInput = currentTransform.GetComponentInChildren<InputManager>();
            currentTransform = currentTransform.parent;
        }
    }
}
