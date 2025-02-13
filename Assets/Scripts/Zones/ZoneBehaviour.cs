using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ZoneBehaviour : MonoBehaviour
{
    public string zoneName;
    public ZoneStats zoneStats; //information about the zones time to interact with and such.
    public virtual void EnterZone(){
        Debug.Log($"Entering {zoneName}");
    }

    public abstract void UniqueBehaviour(InputManager currentPlayerInput);
    public abstract void OnLeavingZone();
}