using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("Checking after enemy");
        if (other.CompareTag("Enemy"))
        {
            // Damage enemy (implement damage logic here)
            
        }
    }

}
