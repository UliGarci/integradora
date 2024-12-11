using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduciraudio : MonoBehaviour
{
    AudioSource fuenteAudio;
    void Start(){
        fuenteAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(){
        fuenteAudio.Play();
    }

    void OnTriggerExit(){
        fuenteAudio.Stop();
    }
}
