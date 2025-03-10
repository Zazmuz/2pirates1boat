using UnityEngine;

public class PlayerSelectSpawn : MonoBehaviour
{
    public PlayerInformation player1;
    public PlayerInformation player2;
    public Transform playerOneSpawn;
    public Transform playerTwoSpawn;
    

    void Start(){
        player1.posX = playerOneSpawn.position.x;
        player1.posY = playerOneSpawn.position.y;

        player2.posX = playerTwoSpawn.position.x;
        player2.posY = playerTwoSpawn.position.y;
    }
}
