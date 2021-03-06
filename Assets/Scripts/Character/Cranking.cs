﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None,
    Horizontal,
    Vertical,
}

public class Cranking : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] int difficulty = 1;
    [SerializeField] float decayRate = 1;

    Steering player;
    Light light;
    float initialIntensity;
    string horizontalInputName;
    string verticalInputName;
    IEnumerator coroutine;
    State nextState;
    float progress;
    float bias = 0.1f;
    bool shownTutorial = false;

    CameraCanvas cameraCanvas;

    void Start()
    {
        cameraCanvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<CameraCanvas>();
        player = GetComponent<Focus>().target.GetComponent<Steering>();

        if (!GManager.singlePlayer)
        {
            horizontalInputName = player.horizontalInputName;
            verticalInputName = player.verticalInputName;
        }
        else
        {
            horizontalInputName = "Single Player Lighthouse Vertical";
            verticalInputName = "Single Player Lighthouse Horizontal";
        }


        light = GetComponent<Light>();
        initialIntensity = light.intensity;
        //Activate();
    }

    void Update()
    {
        if (nextState != State.None)
        {
            var horizontal = Input.GetAxis(horizontalInputName);
            var vertical = Input.GetAxis(verticalInputName);
            switch (nextState)
            {
                case State.Horizontal:
                    if ((IsMatch(horizontal, 1) || IsMatch(horizontal, -1)) && IsMatch(vertical, 0))
                    {
                        progress += 10 / difficulty;
                        nextState = State.Vertical;
                    }
                    break;
                case State.Vertical:
                    if (IsMatch(horizontal, 0) && (IsMatch(vertical, 1) || IsMatch(vertical, -1)))
                    {
                        progress += 10 / difficulty;
                        nextState = State.Horizontal;
                    }
                    break;
            }
            progress -= decayRate * Time.deltaTime;
            progress = Mathf.Max(progress, 0);
            progress = Mathf.Min(progress, 100);
        }
        cameraCanvas.UpdatePowerLevel(progress / 100);
    }

    bool IsMatch(float value, float target)
    {
        return value >= target - bias && value <= target + bias;
    }

    public void Activate()
    {
        coroutine = StartEvent();
        StartCoroutine(coroutine);
    }

    public void Deactivate()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime) / 2);
        while (true)
        {
            progress = 0;
            light.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            light.intensity = initialIntensity;
            yield return new WaitForSeconds(0.2f);
            light.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            light.intensity = initialIntensity;
            yield return new WaitForSeconds(0.2f);
            light.intensity = 0;
            yield return new WaitForSeconds(0.3f);
            light.intensity = initialIntensity / 2;
            yield return new WaitForSeconds(0.4f);
            cameraCanvas.UpdatePowerLevel(0, true);
            light.intensity = 0;
            player.enabled = false;
            nextState = State.Vertical;
            if (!shownTutorial)
            {
                yield return new WaitForSeconds(1.0f);
                cameraCanvas.DisplayMessage("The power went out!\nRotate the analog stick to crank the light back on.");
                shownTutorial = true;
            }
            yield return new WaitUntil(() => progress >= 100);
            nextState = State.None;
            yield return new WaitForSeconds(0.1f);
            light.intensity = initialIntensity / 2;
            yield return new WaitForSeconds(0.2f);
            light.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            light.intensity = initialIntensity / 2;
            yield return new WaitForSeconds(0.2f);
            light.intensity = 0;
            yield return new WaitForSeconds(0.3f);
            light.intensity = initialIntensity;
            player.enabled = true;
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
