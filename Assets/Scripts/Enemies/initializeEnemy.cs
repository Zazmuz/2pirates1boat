using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeEnemy : MonoBehaviour
{
    public EnemyInformation enemyInformation;
    private SpriteRenderer spriteRenderer;
    public GameInformation gameInformation;
    private Slider slider;
    private float smoothSpeed = 0.1f;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        slider = GetComponentInChildren<Slider>();
        InitValues();
        
        //setSprite();
    }
    void Update(){
        slider.value = Mathf.Lerp(slider.value, enemyInformation.health, smoothSpeed);
        if(enemyInformation.health <= 0){
            Destroy(gameObject);
        }
    }

    private void setEnemyPosition(){
        transform.position = new Vector2(enemyInformation.posX, enemyInformation.posY);
    }
    private void InitValues(){
        enemyInformation.health = enemyInformation.maxHealth;
        slider.maxValue = enemyInformation.maxHealth;
        slider.value = enemyInformation.health;
    }
}

