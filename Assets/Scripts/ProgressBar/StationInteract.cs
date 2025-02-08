using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationInteract : MonoBehaviour
{
    public ZoneBehaviour inZone;
    private bool inInteractZone;

    private void Update(){
        if(inInteractZone){
            inZone.UniqueBehaviour();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Interact")) {
        Debug.Log("In interact zone");

        inZone = collision.GetComponent<ZoneBehaviour>();

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
        }
    }
}
