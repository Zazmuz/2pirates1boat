using UnityEngine;
public class PlayerDeath : MonoBehaviour{
    public GameInformation gameInformation;
    private PlayerInformation playerInformation;
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Death")){
            Destroy(gameObject);
            gameInformation.RemovePlayer(gameObject.GetComponent<PlayerInformation>());
        }
    }
        
}
