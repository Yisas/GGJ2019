using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float level = 0;
    [SerializeField] float max = 100;
    [SerializeField] float panicRegenRate = 1;
    [SerializeField] float panicIncreaseRate = 1;

    public CameraCanvas cameraCanvas;

    private void Update()
    {
        cameraCanvas.UpdatePanicLevel(level / max);

        if (InSpotLight())
            level -= panicRegenRate * Time.deltaTime;
        else
            level += panicIncreaseRate * Time.deltaTime;

        level = Mathf.Clamp(level, 0, max);
    }

    bool InSpotLight()
    {
        var line = transform.position - light.transform.position;
        var angle = Vector3.Angle(line, light.transform.forward);
        return angle < light.spotAngle;
    }
}
