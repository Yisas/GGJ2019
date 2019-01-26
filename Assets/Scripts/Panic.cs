using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float level = 0;
    [SerializeField] float max = 100;
    [SerializeField] float regenRate = 1;

    public CameraCanvas cameraCanvas;

    private void Update()
    {
        cameraCanvas.UpdatePanicLevel((max - level) / max);
    }

    void FixedUpdate()
    {
        if (InSpotLight())
        {
            level += regenRate * Time.deltaTime;
            level = Mathf.Min(level, max);
        }
    }

    bool InSpotLight()
    {
        var line = transform.position - light.transform.position;
        var angle = Vector3.Angle(line, light.transform.forward);
        return angle < light.spotAngle;
    }
}
