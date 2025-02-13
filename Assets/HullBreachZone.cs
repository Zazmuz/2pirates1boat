using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HullBreachZone : ZoneBehaviour
{
    private Slider progressBar;
    private Canvas canvas;
    public PlayerMovement player;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    public GameInformation gameInformation;
    private PlayerItemManager playerItemManager;
    private SpriteRenderer spriteRenderer;
    private float interactionTime;
    private bool hasPlanks;
    void Start()
    {
        hasPlanks = false;
        zoneName = zoneStats.name;   
        interactionTime = zoneStats.interactionTime;

        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();
        canvas.enabled = false;
    }
    public override void OnLeavingZone()
    {
        currentPlayerInZone = null;
    }
    public override void UniqueBehaviour(InputManager currentPlayerInput)
    {
        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }

        playerItemManager = currentPlayerInput.GetComponentInParent<PlayerItemManager>();
        
        if(currentPlayerInZone != null && currentPlayerInput.InteractWasPressed){
            AddPlanks();
        }
        if (hasPlanks && currentPlayerInZone != null && currentPlayerInZone.InteractIsHeld && playerItemManager.GetHeldItem().name == "Hammer"){
            StartCoroutine(FillProgressBar());
        }else{
            StopAllCoroutines();
        }
    }
    private void AddPlanks(){
        if(!hasPlanks && playerItemManager.GetHeldItem().name == "Plank"){
            hasPlanks = true;
            spriteRenderer.sprite = zoneStats.altSprite; //changes to the sprite with da planks
            playerItemManager.DropHeldItem(); // TODO create drop method or smth
        }
    }
    private IEnumerator FillProgressBar(){     
        canvas.enabled = true; 
        progressBar.enabled = true;
        float elapsedTime = 0f;
        progressBar.value = 0f;
        interactionTime = zoneStats.interactionTime;

        while (elapsedTime < interactionTime){
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / interactionTime;
            yield return null;
        }

        progressBar.enabled = false;
        gameObject.SetActive(false);
    }

}
