using UnityEngine;

public class ItemPickup : MonoBehaviour{
    public Item item;
    private void OnTriggerEnter2D(Collider2D other){
        Transform currentTransform = other.transform;

        while (currentTransform != null){
            PlayerItemManager player = currentTransform.GetComponentInChildren<PlayerItemManager>(); //check if there's itemManager in the children.
            if (player != null){
                player.InitializeItem(item);
                player.AddItemToInventory(item);
                Destroy(gameObject);
                return;
            }

            currentTransform = currentTransform.parent; // yeah so the player object is fucked, needs to look around in the hierarchy a bunch
        }
    }
}
