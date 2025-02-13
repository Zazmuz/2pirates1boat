using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GoldZone : ZoneBehaviour{
    private Slider progressBar;
    private Canvas canvas;
    public ZoneStats zoneStats;
    private float interactionTime;
    private Coroutine progressCoroutine;
    public PlayerMovement player;
    public InputManager currentPlayerInZone; //to check which player is in the zone :3
    
    void Start(){
        zoneName = "Gold Zone";
        tag = "Interact";

        progressBar = GetComponentInChildren<Slider>(); 
        canvas =  GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }
    public override void UniqueBehaviour(InputManager currentPlayerInput){

        if(currentPlayerInZone == null){
            currentPlayerInZone = currentPlayerInput;
        }
        if (currentPlayerInZone != null && currentPlayerInZone.InteractIsHeld){
            Debug.Log(currentPlayerInZone);
            if (progressCoroutine == null) {
                progressCoroutine = StartCoroutine(FillProgressBar());
            }
        }
        else{
            if (progressCoroutine != null){
                StopCoroutine(progressCoroutine);
                progressCoroutine = null;
                progressBar.value = 0f;
                canvas.enabled = false;
                currentPlayerInZone = null;
            }
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

    public override void OnLeavingZone(){
        currentPlayerInZone = null;
    }
}
