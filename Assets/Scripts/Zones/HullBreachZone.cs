using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

public class HullBreachZone : ZoneBehaviour
{
    public HullBreachParent hullBreachParent;
    public TutorialManager tutorialManager;
    private Slider progressBar;
    private Canvas canvas;
    public PlayerMovement player;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    private Coroutine progressCoroutine;
    public GameInformation gameInformation;
    private PlayerItemManager playerItemManager;
    private SpriteRenderer spriteRenderer;
    private float interactionTime;
    private bool hasPlanks;
    

    void Awake()
    {
        hasPlanks = false;
        zoneName = zoneStats.name;   
  
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();

        hullBreachParent = GetComponentInParent<HullBreachParent>();
        
        canvas.enabled = false;

        SetZoneSize();
    }

    void SetZoneSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(2.5f, 2.5f);
        }
    }

    public override void OnLeavingZone(){
        currentPlayerInZone = null;
        if(progressCoroutine != null){
            StopCoroutine(progressCoroutine);
            progressCoroutine = null;
            ResetProgressBar();
        }
    }

    public override void UniqueBehaviour(InputManager currentPlayerInput){
        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }

        playerItemManager = currentPlayerInput.GetComponentInParent<PlayerItemManager>();
        
        if(currentPlayerInZone != null && currentPlayerInput.InteractWasPressed){
            AddPlanks();
        }
        if (hasPlanks && currentPlayerInZone != null && currentPlayerInZone.InteractIsHeld){
            if (playerItemManager.GetHeldItem() != null && playerItemManager.GetHeldItem().itemName == "Hammer"){
                if(progressCoroutine == null){
                    progressCoroutine = StartCoroutine(FillProgressBar());
                }
            }
        }
        else{
            if (progressCoroutine != null){
                StopCoroutine(progressCoroutine);
                progressCoroutine = null;
                ResetProgressBar();
            }
        }
    }

    private void AddPlanks(){
        if(!hasPlanks && playerItemManager.GetHeldItem().itemName == "Plank"){
            hasPlanks = true;
            spriteRenderer.sprite = zoneStats.altSprite; //changes to the sprite with da planks

            playerItemManager.DropHeldItem();
        }
    }

    private IEnumerator FillProgressBar(){     
        canvas.enabled = true; 
        progressBar.enabled = true;
        float elapsedTime = 0f;
        progressBar.value = 0f;
        interactionTime = zoneStats.interactionTime;
        //Debug.Log("start fill progress bar");
        while (elapsedTime < interactionTime){
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / interactionTime;
            yield return null;
        }

        progressBar.enabled = false;

        // Update hammer durability and hammer damage bar
        if (playerItemManager.GetHeldItem() != null)
        {
            playerItemManager.UseItem();
        }

        // Remove the hull breach
        if (hullBreachParent != null)
        {
            hullBreachParent.RemoveHullBreach(gameObject);
        }
        if (tutorialManager != null)
        {
            tutorialManager.RemoveHullBreach();
        }

        Destroy(gameObject);
    }

    private void ResetProgressBar(){
        if (progressBar != null)
        {
            progressBar.value = 0f;
            canvas.enabled = false;
            progressBar.enabled = false;
        }
    }
}
