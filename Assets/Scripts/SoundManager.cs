using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public float panicLevelForTransition = 70;
    public AudioSource normalAudiosource;
    public AudioSource toNormalAudiosource;
    public AudioSource panicAudiosource;
    public AudioSource toPanicAudiosource;
    public AudioSource endGameAudioSource;
    public AudioMixerSnapshot normalMood;
    public AudioMixerSnapshot panickedMood;
    public AudioMixerSnapshot endGameSnapshot;
    public float mixerTransitionTime;
    public float timeToTransition = 0;

    private enum Transitioning { No, ToNormal, ToPanic }
    private Transitioning transitioning = Transitioning.No;
    private float transitionTimer;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transitioning == Transitioning.ToPanic || transitioning == Transitioning.ToNormal)
        {
            transitionTimer -= Time.deltaTime;
            if (transitionTimer <= 0)
            {
                Transition();
                transitioning = Transitioning.No;
            }
        }
    }

    public void TransitionToPanic()
    {
        toPanicAudiosource.Play();
        transitioning = Transitioning.ToPanic;
        transitionTimer = timeToTransition;
    }

    public void TransitionToNormal()
    {
        toNormalAudiosource.Play();
        transitioning = Transitioning.ToNormal;
        transitionTimer = timeToTransition;
    }

    public void Transition()
    {
        if(transitioning == Transitioning.ToNormal)
            normalMood.TransitionTo(mixerTransitionTime);
        else if(transitioning == Transitioning.ToPanic)
            panickedMood.TransitionTo(mixerTransitionTime);
    }

    public void EndGame()
    {
        transitioning = Transitioning.No;
        endGameAudioSource.Play();
        endGameSnapshot.TransitionTo(mixerTransitionTime);
    }
}
