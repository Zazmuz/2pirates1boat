using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float movementSpeed;
    private int patrolDestination;
    private Transform playerTransform;
    private bool isChasing = false;
    
    private void Start()
    {
        patrolDestination = 0;
    }

    private void Update()
    {
        if (isChasing && playerTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            transform.localScale = new Vector3((patrolDestination == 0) ? 1 : -1, 1, 1);
        }
    }

    private void ChasePlayer()
    {
        Vector2 targetPosition = new Vector2(playerTransform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime * 1.5F);

        // Flip enemy based on player's position
        if (playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // Face right
        else
            transform.localScale = new Vector3(1, 1, 1); // Face left
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.CompareTag("Player"));
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected! Chasing...");
            isChasing = true;
            playerTransform = collision.transform;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player lost! Returning to patrol...");
            isChasing = false;
            playerTransform = null;
        }
    }
}