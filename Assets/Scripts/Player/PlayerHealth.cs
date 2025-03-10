using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameInformation gameInformation;
    [Header("max health")]
    [Range(1,100)] public int maxhealth = 100;
    
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            gameInformation.RemovePlayer(gameObject.GetComponent<GameObject>());
        }
    }
}
