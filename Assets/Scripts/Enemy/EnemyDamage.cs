using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damagePerSecond = 10; // Damage per second

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //Debug.Log("PLAYER TAKE DAMAGE");
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
