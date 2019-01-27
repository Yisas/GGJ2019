using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour
{
    [SerializeField] Slider panic;
    [SerializeField] Slider power;
    [SerializeField] float updateSpeed = 1;

    void Start()
    {
        panic.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        power.GetComponent<Animator>().CrossFadeInFixedTime("Hidden", 0.0f);
        panic.value = 0;
        power.value = 0;
    }

    public void UpdatePanicLevel(float percentage)
    {
        panic.value = Mathf.Lerp(panic.value, percentage, Time.deltaTime * updateSpeed);
        var alpha = panic.GetComponent<CanvasGroup>().alpha;
        Debug.Log(panic.value);
        Debug.Log(Mathf.Approximately(panic.value, 0));
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
}
