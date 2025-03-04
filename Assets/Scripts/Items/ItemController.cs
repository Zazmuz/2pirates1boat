using System.Collections;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.Timeline;
public class ItemController : MonoBehaviour
{
    private PlayerItemManager playerItemManager;
    private InputManager inputManager;
    private ItemPickup itemInRange;
    public GameObject itemObject;
    private CircleCollider2D weaponCollider;
    public float swingDuration = 0.2f; // Speed of the swing animation
    void Start(){
        playerItemManager = GetComponent<PlayerItemManager>();
        inputManager = GetComponent<InputManager>();
        weaponCollider = GetComponentInChildren<CircleCollider2D>();
    }
    void Update(){
        if(inputManager.InteractWasPressed && itemInRange != null){
            playerItemManager.AddItemToInventory(itemInRange.GetItem());
        }

        if(inputManager.DropItemWasPressed && playerItemManager.GetHeldItem() != null){
            playerItemManager.DropHeldItem();
        }
        if(inputManager.InteractWasPressed && playerItemManager.GetHeldItem() != null && (playerItemManager.GetHeldItem().ActionType == ActionType.Attack)){
            StartCoroutine(AttackFrames());
            StartCoroutine(SwordAnimation());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            itemInRange = other.GetComponent<ItemPickup>();
        }
        if(other.CompareTag("Enemy")){
            other.GetComponent<InitializeEnemy>().enemyInformation.TakeDamage(30f);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item") && itemInRange == other.GetComponent<ItemPickup>())
        {
            itemInRange = null;
        }
    }
    private IEnumerator AttackFrames(){
        weaponCollider.enabled = true;
        
        yield return new WaitForSeconds(0.1f);
        weaponCollider.enabled = false;
        Debug.Log(weaponCollider);

    }
    
    private IEnumerator SwordAnimation(){
        float elapsedTime = 0f;
        Quaternion initRotation = Quaternion.Euler(0, 0, 0);
        Quaternion startRotation = Quaternion.Euler(0, 0, -45);
        Quaternion endRotation = Quaternion.Euler(0, 0, 45);

        while (elapsedTime < swingDuration)
        {
            itemObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        itemObject.transform.rotation = endRotation; // Ensure final position is reached

        // Optional: Reset the sword after the swing
        yield return new WaitForSeconds(0.1f);
        itemObject.transform.rotation = initRotation;
    }
}
