using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSelectText : MonoBehaviour
{
    public SharedDeviceInputManager sharedDeviceInputManager;
    private TextMeshProUGUI textMeshPro;
    private string onReadyText = "All pirates ready?\nPress spacebar to start sailing!";
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sharedDeviceInputManager.playerCount == 2){
            textMeshPro.SetText(onReadyText);
        }
    }
}
