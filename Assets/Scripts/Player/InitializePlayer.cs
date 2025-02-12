using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitilizePlayer : MonoBehaviour
{
    public PlayerInformation player1;
    public PlayerInformation player2;
    private PlayerInformation player;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        setPlayer();
        if(player != null){
            setSprite();
            setPlayerPosition();
        }  
    }
    private void setPlayer(){
        switch(playerInput.currentControlScheme){
            case "WASD":
                player = player1;
                break;
            case "Arrows":
                player = player2;
                break;
            default:
                player = null;
                break;

        }
        
    }
    private void setSprite(){
        spriteRenderer.sprite = player.sprite;
    }
    private void setPlayerPosition(){
        transform.position = new Vector2(player.posX, player.posY);
    }
}
