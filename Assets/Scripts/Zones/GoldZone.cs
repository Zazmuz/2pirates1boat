using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class GoldZone : ZoneBehaviour
{
    private Slider progressBar;
    private Canvas canvas;
    public ZoneStats zoneStats;
    private float interactionTime;
    private Coroutine progressCoroutine;
    
    void Start(){
        zoneName = "Gold Zone";
        tag = "Interact";

        progressBar = GetComponentInChildren<Slider>(); 
        canvas =  GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }
    public override void UniqueBehaviour(){

        if (InputManager.InteractIsHeld)
        {
            if (progressCoroutine == null) 
            {
                progressCoroutine = StartCoroutine(FillProgressBar());
            }
        }
        else
        {
            if (progressCoroutine != null)
            {
                StopCoroutine(progressCoroutine);
                progressCoroutine = null;
                progressBar.value = 0f;
                canvas.enabled = false;
            }
        }
    }

    private IEnumerator FillProgressBar(){     
        canvas.enabled = true; 
        progressBar.enabled = true;
        float elapsedTime = 0f;
        progressBar.value = 0f;
        interactionTime = zoneStats.goldZoneInteractionTime;


        while (elapsedTime < interactionTime){
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / interactionTime;
            yield return null;
        }

        progressBar.enabled = false;
        gameObject.SetActive(false);
    }
}
