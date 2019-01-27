using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour
{
    [SerializeField] Image panic;
    [SerializeField] Image power;
    [SerializeField] Image wheel;
    [SerializeField] Vector3 finalWheelPosition;
    [SerializeField] Vector3 finalWheelScale;
    [SerializeField] CanvasGroup panel;
    [SerializeField] float updateSpeed = 1;
    [SerializeField] float popupDuration = 1;
    public Image blackoutImage;
    public float fadeSpeed;

    bool fadeOutIn = false;
    bool fadeOutInDone = false;
    private CanvasCallbackReceiver callbackReceiver;
    Vector3 initialWheelPosition;
    Vector3 initialWheelScale;

    void Start()
    {
        panel.GetComponent<Animator>().CrossFadeInFixedTime("Out", 0.0f);
        panic.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        power.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        panic.fillAmount = 0;
        power.fillAmount = 0;
        initialWheelPosition = wheel.rectTransform.anchoredPosition;
        initialWheelScale = wheel.rectTransform.localScale;
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
        panic.fillAmount = Mathf.Lerp(panic.fillAmount, percentage, Time.deltaTime * updateSpeed);
        wheel.rectTransform.anchoredPosition = Vector3.Lerp(initialWheelPosition, finalWheelPosition, panic.fillAmount);
        wheel.rectTransform.localScale = Vector3.Lerp(initialWheelScale, finalWheelScale, panic.fillAmount);
        wheel.GetComponent<Animator>().speed = panic.fillAmount;
        var alpha = panic.transform.parent.GetComponent<CanvasGroup>().alpha;
        if (panic.fillAmount < 0.025f && alpha > 0)
        {
            panic.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.2f);
        }
        else if (panic.fillAmount > 0.025f && alpha == 0)
        {
            panic.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Visible", 0.2f);
        }
    }

    public void UpdatePowerLevel(float percentage, bool show = false)
    {
        power.fillAmount = Mathf.Lerp(power.fillAmount, percentage, Time.deltaTime * updateSpeed);
        var alpha = power.transform.parent.GetComponent<CanvasGroup>().alpha;
        if (Mathf.Approximately(power.fillAmount, 1) && alpha > 0)
        {
            power.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.2f);
        }
        if (show)
        {
            power.transform.parent.GetComponent<Animator>().CrossFadeInFixedTime("Visible", 0.2f);
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
