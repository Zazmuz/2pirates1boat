using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainThemeScript : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("PitchBendGroup");

        audioSource.outputAudioMixerGroup = pitchBendGroup;
    }
    void Update(){
        
    }
}
