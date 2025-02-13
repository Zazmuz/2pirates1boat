using UnityEngine;
using UnityEngine.InputSystem;

public class PlankZone : ZoneBehaviour{
    public Item item;
    private PlayerItemManager player;
    public override void OnLeavingZone()
    {
        throw new System.NotImplementedException();
    }

    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInput.InteractWasPressed){
            if (player != null){
                    player.InitializeItem(item);
                    player.AddItemToInventory(item);
                    return;
                }
        }
    }
}
