using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] audios;

    private void Awake()
    {
        audio.GetComponent<AudioSource>();
    }
    
    public void Seleccionaaudio(int indice, float volumen)
    {
        audio.PlayOneShot(audios[indice], volumen);
    }
}
