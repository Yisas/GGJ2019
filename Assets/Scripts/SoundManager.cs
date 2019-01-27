using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip normalMood;
    public AudioClip panickedMood;
    public AudioClip normalToPanicTransition;
    public AudioClip panicToNormalTransition;

    public float moodTransitionDuration;

    private enum Transitioning { No, ToNormal, ToPanic }
    private Transitioning transitioning;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning == Transitioning.ToNormal && !audioSource.isPlaying)
        {
            Normal();
        }

        if (transitioning == Transitioning.ToPanic && !audioSource.isPlaying)
        {
            Panic();
        }
    }

    public void TransitionToPanic()
    {
        audioSource.clip = normalToPanicTransition;
        audioSource.Play();
        transitioning = Transitioning.ToPanic;
    }

    public void TransitionToNormal()
    {
        audioSource.clip = panicToNormalTransition;
        audioSource.Play();
        transitioning = Transitioning.ToNormal;
    }

    public void Panic()
    {
        audioSource.clip = panickedMood;
        audioSource.Play();
        transitioning = Transitioning.No;
    }

    public void Normal()
    {
        audioSource.clip = normalMood;
        audioSource.Play();
        transitioning = Transitioning.No;
    }
}
