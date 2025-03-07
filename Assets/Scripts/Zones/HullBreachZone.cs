using System.Collections;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HullBreachZone : ZoneBehaviour
{
    public HullBreachParent hullBreachParent;
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
    
    private Slider hammerDamageBar;
    void Start()
    {
        hasPlanks = false;
        zoneName = zoneStats.name;   
  
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        progressBar = GetComponentInChildren<Slider>();
        hammerDamageBar = GetComponentInChildren<Slider>();

        hullBreachParent = GetComponentInParent<HullBreachParent>();
        
        canvas.enabled = false;
        hammerDamageBar.maxValue = 3;

        SetZoneSize();
    }

        void SetZoneSize()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(2, 2);
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
            if (playerItemManager.GetHeldItem() != null && playerItemManager.GetHeldItem().name == "Hammer"){
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
        if(!hasPlanks && playerItemManager.GetHeldItem().name == "Plank"){
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

        while (elapsedTime < interactionTime){
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / interactionTime;
            yield return null;
        }

        progressBar.enabled = false;
        hullBreachParent.RemoveHullBreach(gameObject);

        playerItemManager.UseItem();
        hammerDamageBar.value++;
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
