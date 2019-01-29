using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public bool spotlightTarget = false;
    [SerializeField] internal string horizontalInputName = "Horizontal";
    [SerializeField] internal string verticalInputName = "Vertical";

    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float drag = 0.0f;
    [SerializeField] float maxVelocity = 0.0f;
    public bool affectedByWind = false;

    internal Vector3 velocity = Vector3.zero;

    private bool invertedControls = false;
    private Transform endGameLocation;

    Rigidbody rigidbody;
    Wind wind;

    bool controlsEnabled = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        wind = GameObject.FindWithTag("Wind").GetComponent<Wind>();
        endGameLocation = GameObject.FindWithTag("End Game Destination").transform;

        // 2 player code
        if (GManager.singlePlayer && spotlightTarget)
        {
            horizontalInputName = "Single Player Lighthouse Vertical";
            verticalInputName = "Single Player Lighthouse Horizontal";
        }
    }

    void Update()
    {
        var horizontal = Input.GetAxis(horizontalInputName);
        var vertical = Input.GetAxis(verticalInputName);

        if (invertedControls)
        {
            horizontal = -horizontal;
            vertical = -vertical;
        }

        Vector3 direction = new Vector3();

        if (controlsEnabled)
        {
            direction = (new Vector3(horizontal, 0.0f, vertical)).normalized;
        }
        else
        {
            direction = (endGameLocation.position - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
        }

        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f) || !controlsEnabled)
        {
            velocity += direction * acceleration * Time.deltaTime;

            if (velocity.magnitude > maxVelocity)
            {
                velocity = direction * maxVelocity;
            }
        }
    }

    void FixedUpdate()
    {
        if (affectedByWind)
        {
            velocity += wind.GetAcceleration() * Time.deltaTime;
            if (velocity.magnitude > maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }
        }
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
        velocity *= 1 - (drag * Time.deltaTime);

    }

    public void ToggleInvertControls(bool on)
    {
        invertedControls = on;
    }

    public void GoToEndGame()
    {
        controlsEnabled = false;
    }
}
