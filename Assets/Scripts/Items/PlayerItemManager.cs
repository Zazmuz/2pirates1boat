using UnityEngine;
public class PlayerItemManager : MonoBehaviour{
    public SpriteRenderer itemSpriteRenderer;
    public static int maxHeldItems = 1;
    public Item item;
    public Item[] inventory = new Item[maxHeldItems]; // Should be at most one held item. Maybe separate array for passive upgrades?
    public void InitializeItem(Item newItem){
        item = newItem;
        //sets items attributes
        item.itemSprite = newItem.itemSprite;
        item.itemType = newItem.itemType;
        item.ActionType = newItem.ActionType;
    }
    public void AddItemToInventory(Item item){
        if(inventory[0] == null){
            inventory[0] = item;
            itemSpriteRenderer.sprite = item.itemSprite;
        }
    }
    public void DropHeldItem(){
        if(inventory[0] != null){
            inventory[0] = null;
            itemSpriteRenderer.sprite = null;
        }
    }
    public Item GetHeldItem(){
        if(inventory[0] == null){
            Debug.LogError("Theres nothing in inventory");
        }
        return inventory[0];
        
    }
}
