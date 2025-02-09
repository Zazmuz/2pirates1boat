using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerItemManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public static int maxHeldItems = 1;
    public Item item;
    public Item[] inventory = new Item[maxHeldItems]; // Should be at most one held item. Maybe separate array for passive upgrades?
    


    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
            spriteRenderer.sprite = item.itemSprite;
        }
    }
    public void DropHeldItem(){
        if(inventory[0] != null){
            inventory[0] = null;
        }
    }
}
