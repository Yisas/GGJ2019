using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Steering))]
public class Panic : MonoBehaviour
{
    [SerializeField] bool autoPanicActive = false;
    [SerializeField] Light light;
    [SerializeField] float level = 0;
    [SerializeField] float max = 100;
    [SerializeField] float panicRegenRate = 1;
    [SerializeField] float panicIncreaseRate = 1;
    [SerializeField] float panicEventDuration = 5;
    [SerializeField] float panicEventAutoRecovery = 30;
    [SerializeField] float obstacleCollisionPunishment;
    bool panicEventActive = false;
    private float panicEventTimer = 0;

    private Steering steering;
    private CameraCanvas cameraCanvas;

    private void Start()
    {
        steering = GetComponent<Steering>();
        cameraCanvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<CameraCanvas>();
    }

    private void Update()
    {
        cameraCanvas.UpdatePanicLevel(level / max);

        if (InSpotLight())
            level -= panicRegenRate * Time.deltaTime;
        else if(autoPanicActive)
            level += panicIncreaseRate * Time.deltaTime;

        level = Mathf.Clamp(level, 0, max);

        if (panicEventActive)
        {
            panicEventTimer -= Time.deltaTime;
            if(panicEventTimer <= 0)
            {
                PanicEventAutoRecover();
            }
        }
        else
        {
            if (level >= max)
            {
                TriggerPanicEvent();
            }
        }

        // For debugging purposes only, perhaps remove later
        if (Input.GetButtonDown("Force Panic"))
            level = 100;
    }

    bool InSpotLight()
    {
        var line = transform.position - light.transform.position;
        var angle = Vector3.Angle(line, light.transform.forward);
        return angle < light.spotAngle;
    }

    void TriggerPanicEvent()
    {
        panicEventActive = true;
        panicEventTimer = panicEventDuration;
        steering.ToggleInvertControls(true);
    }

    void StopPanicEvent()
    {
        panicEventActive = false;
        steering.ToggleInvertControls(false);
    }

    // After panic timer is done, reduce levels a little bit
    void PanicEventAutoRecover()
    {
        steering.ToggleInvertControls(false);
        panicEventTimer = panicEventDuration;
        level -= panicEventAutoRecovery;
        panicEventActive = false;
    }

    void HandleObstacleCollision()
    {
        if (!panicEventActive)
        {
            level += obstacleCollisionPunishment;
            level = Mathf.Clamp(level, 0, max);
            cameraCanvas.UpdatePanicLevel(level / max);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            HandleObstacleCollision();
        }
    }
}
