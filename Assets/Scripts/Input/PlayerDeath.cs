using UnityEngine;
public class PlayerDeath : MonoBehaviour{
    public GameInformation gameInformation;
    public Transform respawnSpot;
    private Transform boat;
    void Awake()
    {
        respawnSpot = GameObject.FindWithTag("Respawn").transform;
        boat = GameObject.FindWithTag("Ship").transform;
        
        if (respawnSpot == null)
        {
            Debug.LogError("Respawn spot not found!");
        }

        if (boat == null)
        {
            Debug.LogError("Boat not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Death")){
            if (respawnSpot == null || boat == null)
            {
                Debug.LogError("Respawn spot or Boat not found!");
                Destroy(gameObject);
                gameInformation.RemovePlayer(gameObject);
                return;
            }
            else
            {
                Vector2 boatOffset = respawnSpot.position - boat.position;
                transform.position = (Vector2)boat.position + boatOffset;
            }
        }
    }
}
