using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public GameInformation GameLogic;
    void Start()
    {
        GameLogic.ResetGame();
    }
}
