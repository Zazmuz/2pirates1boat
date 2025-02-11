using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HullBreachBehaviour : ZoneBehaviour
{
    private Slider progressBar;
    private Canvas canvas;
    public ZoneStats zoneStats;
    private float interactionTime;
    private Coroutine progressCoroutine;
    //private Plank plank;
    private bool hasPlank; 
    private bool hasTool;
    private InputManager inputManager;
    void Start(){
        zoneName = "Hull breach zone";
        tag = "Interact";

        progressBar = GetComponentInChildren<Slider>(); 
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }
    public override void UniqueBehaviour(){
        if (inputManager.InteractIsHeld && hasPlank && hasTool)
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
