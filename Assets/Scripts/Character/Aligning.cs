using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligning : MonoBehaviour
{
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float drag = 0.0f;
    [SerializeField] float maxVelocity = 0.0f;

    internal float velocity = 0.0f;
    Steering steering;

    void Start()
    {
        steering = GetComponent<Steering>();
    }

    void FixedUpdate()
    {
        velocity *= 1 - (drag * Time.deltaTime);
        var angle = Vector3.Angle(transform.forward, steering.velocity);
        if (!Mathf.Approximately(angle, 0) && velocity < maxVelocity)
        {
            velocity += ((angle > 0)? 1 : -1) * acceleration * Time.deltaTime;
        }
        if (steering.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(steering.velocity),
                velocity
            );
        }
    }
}
