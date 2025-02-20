using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPostProcessing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        QualitySettings.antiAliasing = 2;
    }
}
