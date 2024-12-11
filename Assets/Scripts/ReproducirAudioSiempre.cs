using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducirAudioSiempre : MonoBehaviour
{
   AudioSource fuenteAudio;
    void Start(){
        fuenteAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(){
        fuenteAudio.Play();
    }
}
