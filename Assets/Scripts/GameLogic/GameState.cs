using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameInformation gameInformation;
    void Start(){
        gameInformation.ResetGame();
    }

}
