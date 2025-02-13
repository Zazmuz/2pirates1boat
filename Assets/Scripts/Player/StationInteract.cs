using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StationInteract : MonoBehaviour
{
    public ZoneBehaviour inZone;
    private bool inInteractZone;
    private InputManager currentPlayerInput;
    void Awake(){
        currentPlayerInput = GetComponent<InputManager>();
    }
    private void Update(){
        if(inInteractZone){
            Debug.Log("In interact zone");
            inZone.UniqueBehaviour(currentPlayerInput);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Interact")) {
            inZone = collision.GetComponent<ZoneBehaviour>();
            Debug.Log("In interact zone");
            if (inZone != null) {
                inInteractZone = true;
            } else {
                Debug.LogWarning("No ZoneBehaviour found");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Interact")){
            inInteractZone = false;
            inZone.OnLeavingZone();
        }
    }
}
