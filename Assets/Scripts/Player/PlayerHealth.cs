using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameInformation gameInformation;
    [Header("max health")]
    [Range(1,100)] public int maxhealth = 100;
    
    public float currentHealth;
    private Transform respawnSpot;
    private Transform boat;
    public GameObject healthbar;
    private GameObject myHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        respawnSpot = GameObject.FindWithTag("Respawn").transform;
        boat = GameObject.FindWithTag("Ship").transform;

        myHealthBar = Instantiate(healthbar, new Vector3(0, 0, 0), Quaternion.identity);
        FollowPlayer playerFollowScript = myHealthBar.GetComponent<FollowPlayer>();
        playerFollowScript.playerTransform = this.gameObject.transform;
        HealthBar hpBarObject = myHealthBar.GetComponent<HealthBar>();
        hpBarObject.playerHealth = this;

        if (respawnSpot == null)
        {
            Debug.LogError("Respawn spot not found!");
        }

        if (boat == null)
        {
            Debug.LogError("Boat not found!");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        if (respawnSpot != null && boat != null)
        {
            Vector2 boatOffset = respawnSpot.position - boat.position;
            transform.position = (Vector2)boat.position + boatOffset;
            currentHealth = maxhealth; // Reset health
        }
        else
        {
            Debug.LogError("Respawn spot or Boat not found!");
        }
    }

    void OnDestroy()
    {
        Destroy(myHealthBar);
    }
}