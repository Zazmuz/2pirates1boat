using UnityEngine;
public class PlayerItemManager : MonoBehaviour{
    public SpriteRenderer itemSpriteRenderer;
    public static int maxHeldItems = 1;
    public Item item;
    public Item[] inventory = new Item[maxHeldItems]; // Should be at most one held item. Maybe separate array for passive upgrades?
    public void InitializeItem(Item newItem){
        
        //item = newItem;
        item = ScriptableObject.CreateInstance<Item>();
        //sets items attributes
        item.itemSprites = newItem.itemSprites;
        item.itemType = newItem.itemType;
        item.ActionType = newItem.ActionType;
        if (newItem.itemName == "Hammer"){
            item.durability = 3;
        } else if (newItem.itemName == "Plank"){
            item.durability = 1;
        } else {
            item.durability = 999999999;
        }
        //Debug.Log("Item initialized " + item.durability + " " + newItem.itemName);

    }
    public void AddItemToInventory(Item item){
        if(inventory[0] == null){
        if (item.itemName == "Hammer"){
            item.durability = 3;
        } else if (item.itemName == "Plank"){
            item.durability = 1;
        } else {
            item.durability = 999999999;
        }
            inventory[0] = item;
            
            itemSpriteRenderer.sprite = item.itemSprites[item.durability-1];
        }
    }
    public void DropHeldItem(){
        if (inventory[0] != null){
            inventory[0] = null;
            itemSpriteRenderer.sprite = null;
        }
    }
    public Item GetHeldItem(){
        //if(inventory[0] == null){
        //    Debug.Log("Nothing in inventory");
        //}
        return inventory[0];
    }
    public void UseItem(){
        if (inventory[0] != null){
            inventory[0].durability--;
            if (inventory[0].durability <= 0){
                DropHeldItem();
            }
            else {
                itemSpriteRenderer.sprite = item.itemSprites[inventory[0].durability - 1];
            }
        }
    }
}
