using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour
{
    [SerializeField] Slider panic;
    [SerializeField] Slider power;
    [SerializeField] CanvasGroup panel;
    [SerializeField] float updateSpeed = 1;
    [SerializeField] float popupDuration = 1;
    public Image blackoutImage;
    public float fadeSpeed;

    bool fadeOutIn = false;
    bool fadeOutInDone = false;
    private CanvasCallbackReceiver callbackReceiver;

    void Start()
    {
        panel.GetComponent<Animator>().CrossFadeInFixedTime("Out", 0.0f);
        panic.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        power.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        panic.value = 0;
        power.value = 0;
        //DisplayMessage("test");
    }

    private void Update()
    {
        if (fadeOutIn && fadeOutInDone)
        {
            fadeOutIn = false;
            fadeOutInDone = false;
            if (callbackReceiver != null)
            {
                callbackReceiver.Execute();
                callbackReceiver = null;
            }

            StartCoroutine("FadeInCoroutine");
        }
    }

    public void UpdatePanicLevel(float percentage)
    {
        panic.value = Mathf.Lerp(panic.value, percentage, Time.deltaTime * updateSpeed);
        var alpha = panic.GetComponent<CanvasGroup>().alpha;
        if (panic.value < 0.025f && alpha > 0)
        {
            panic.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.2f);
        }
        else if (panic.value > 0.025f && alpha == 0)
        {
            panic.GetComponent<Animator>().CrossFadeInFixedTime("Visible", 0.2f);
        }
    }

    public void UpdatePowerLevel(float percentage, bool show = false)
    {
        power.value = Mathf.Lerp(power.value, percentage, Time.deltaTime * updateSpeed);
        var alpha = power.GetComponent<CanvasGroup>().alpha;
        if (Mathf.Approximately(power.value, 1) && alpha > 0)
        {
            power.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.2f);
        }
        else if (show)
        {
            power.GetComponent<Animator>().CrossFadeInFixedTime("Visible", 0.2f);
        }
    }

    public void DisplayMessage(string message)
    {
        panel.GetComponentInChildren<Text>().text = message;
        StartCoroutine(StartDisplayMessage());
    }

    IEnumerator StartDisplayMessage()
    {
        panel.GetComponent<Animator>().CrossFadeInFixedTime("In", 0.5f);
        yield return new WaitForSeconds(popupDuration);
        panel.GetComponent<Animator>().CrossFadeInFixedTime("Out", 0.25f);
    }

    public void FadeOutIn(CanvasCallbackReceiver callbackReceiver = null)
    {
        fadeOutIn = true;
        StartCoroutine("FadeOutCoroutine");
        this.callbackReceiver = callbackReceiver;
    }

    public void FadeOut()
    {
        StartCoroutine("FadeOutCoroutine");
    }

    public void FadeIn()
    {
        StartCoroutine("FadeInCoroutine");
    }

    IEnumerator FadeInCoroutine()
    {
        Color c;

        for (float f = 1f; f >= 0; f -= fadeSpeed)
        {
            c = blackoutImage.color;
            c.a = f;
            blackoutImage.color = c;
            yield return null;
        }

        c = blackoutImage.color;
        c.a = 0;
        blackoutImage.color = c;
    }

    IEnumerator FadeOutCoroutine()
    {
        Color c;

        for (float f = 0f; f <= 1.0f; f += fadeSpeed)
        {
            c = blackoutImage.color;
            c.a = f;
            blackoutImage.color = c;
            Debug.Log(f);
            yield return null;
        }

        c = blackoutImage.color;
        c.a = 1;
        blackoutImage.color = c;

        if (fadeOutIn)
        {
            fadeOutInDone = true;
        }
    }
}
