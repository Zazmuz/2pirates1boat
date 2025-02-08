using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZoneBehaviour : MonoBehaviour
{

    public string zoneName;
    public virtual void EnterZone(){
        Debug.Log($"Entering {zoneName}");
    }

    public abstract void UniqueBehaviour();
}