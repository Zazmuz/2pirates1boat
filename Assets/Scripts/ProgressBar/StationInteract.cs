using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationInteract : MonoBehaviour
{
    public Slider progressBar;
    private bool isInteracting;
    private bool inInteractZone;
    private float interactionTime = 3f;
    private void Awake(){
        progressBar.gameObject.SetActive(false);

    }
    private void Update(){
        if(InputManager.InteractIsHeld && !isInteracting && inInteractZone){
            StartCoroutine(Interact());
        }else{
            StopAllCoroutines();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Interact")){
            Debug.Log("In interact zone");
            inInteractZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Interact")){
            inInteractZone = false;
        }
    }

    IEnumerator Interact(){
        progressBar.gameObject.SetActive(true);
        progressBar.value = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < interactionTime)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / interactionTime; // Update progress bar
            yield return null;
        }

        progressBar.gameObject.SetActive(false);
        isInteracting = false;

    }
}
