using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public float transitionTime;
    public AudioSource normalAudiosource;
    public AudioSource toNormalAudiosource;
    public AudioSource panicAudiosource;
    public AudioSource toPanicAudiosource;
    public AudioMixerSnapshot normalMood;
    public AudioMixerSnapshot toNormalTransition;
    public AudioMixerSnapshot panickedMood;
    public AudioMixerSnapshot toPanicTransition;
    public float mixerTransitionTime;

    private enum Transitioning { No, ToNormal, ToPanic }
    private Transitioning transitioning;
    private AudioSource audioSource;
    private float transitionTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transitionTimer -= Time.deltaTime;

        if(transitioning == Transitioning.ToNormal && transitionTimer <= 0)
        {
            Normal();
        }

        if (transitioning == Transitioning.ToPanic && transitionTimer <= 0)
        {
            Panic();
        }
    }

    public void TransitionToPanic()
    {
        toPanicAudiosource.Play();
        toPanicTransition.TransitionTo(mixerTransitionTime);
        transitioning = Transitioning.ToPanic;
        transitionTimer = transitionTime;
    }

    public void TransitionToNormal()
    {
        toNormalAudiosource.Play();
        toNormalTransition.TransitionTo(mixerTransitionTime);
        transitioning = Transitioning.ToNormal;
        transitionTimer = transitionTime;
    }

    public void Panic()
    {
        panicAudiosource.Play();
        normalMood.TransitionTo(mixerTransitionTime);
        transitioning = Transitioning.No;
        transitionTimer = 0;
    }

    public void Normal()
    {
        normalAudiosource.Play();
        normalMood.TransitionTo(mixerTransitionTime);
        transitioning = Transitioning.No;
        transitionTimer = 0;
    }
}
