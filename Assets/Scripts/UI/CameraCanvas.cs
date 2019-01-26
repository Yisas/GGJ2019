using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvas : MonoBehaviour
{
    [SerializeField] Slider panic;
    [SerializeField] Slider power;
    [SerializeField] float updateSpeed = 1;


    public void UpdatePanicLevel(float percentage)
    {
        panic.value = Mathf.Lerp(panic.value, percentage, Time.deltaTime * updateSpeed);
    }

    public void UpdatePowerLevel(float percentage)
    {
        power.value = Mathf.Lerp(power.value, percentage, Time.deltaTime * updateSpeed);
    }
}
