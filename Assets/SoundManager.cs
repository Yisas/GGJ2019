using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip normalMood;
    public AudioClip panickedMood;
    public AudioClip normalToPanicTransition;
    public AudioClip panicToNormalTransition;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PanicMood()
    {
        audioSource.clip = panickedMood;
    }

    void NormalMood()
    {
        audioSource.clip = normalMood;
    }
}
