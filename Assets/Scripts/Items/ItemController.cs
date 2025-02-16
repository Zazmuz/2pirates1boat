using System.Runtime.Serialization.Json;
using UnityEngine;
public class ItemController : MonoBehaviour
{
    private PlayerItemManager playerItemManager;
    private InputManager inputManager;
    private ItemPickup itemInRange;
    void Start(){
        playerItemManager = GetComponent<PlayerItemManager>();
        inputManager = GetComponent<InputManager>();
    }
    void Update(){
        if(inputManager.InteractWasPressed && itemInRange != null){
            playerItemManager.AddItemToInventory(itemInRange.GetItem());
        }

        if(inputManager.DropItemWasPressed && playerItemManager.GetHeldItem() != null){
            playerItemManager.DropHeldItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            itemInRange = other.GetComponent<ItemPickup>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item") && itemInRange == other.GetComponent<ItemPickup>())
        {
            itemInRange = null;
        }
    }
}
