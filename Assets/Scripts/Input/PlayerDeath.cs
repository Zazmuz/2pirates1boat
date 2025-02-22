using UnityEngine;
public class PlayerDeath : MonoBehaviour{
    public GameInformation gameInformation;
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Death")){
            Destroy(gameObject);
            gameInformation.RemovePlayer(gameObject.GetComponent<GameObject>());
        }
    }
        
}
