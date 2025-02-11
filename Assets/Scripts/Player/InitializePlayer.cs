using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitilizePlayer : MonoBehaviour
{
    public PlayerInformation player1;
    public PlayerInformation player2;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        setSprite();
    }
    private void setSprite(){
        if(playerInput.currentControlScheme == "WASD"){
            spriteRenderer.sprite = player1.sprite;
        }else if(playerInput.currentControlScheme == "Arrows"){
            spriteRenderer.sprite = player2.sprite;
        }
    }
}
