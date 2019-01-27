using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float minSpeed = 0.0f;
    public float maxSpeed = 1.0f;
    public float speedChange = 0.1f;
    public float angleChangeSpeed = 15.0f;
    public float maxAngleChange = 30.0f;

    internal float direction;
    internal float speed;

    void FixedUpdate()
    {
        var angleChange = (Random.Range(-maxAngleChange, maxAngleChange)
                         + Random.Range(-maxAngleChange, maxAngleChange))
                         / 2;
        direction += angleChange * angleChangeSpeed * Time.deltaTime;
        speed += ((Random.value > 0.5f)? 1 : -1) * speedChange * Time.deltaTime;
        speed = Mathf.Min(speed, maxSpeed);
        speed = Mathf.Max(speed, minSpeed);
    }

    public Vector3 GetAcceleration()
    {
        return Quaternion.AngleAxis(direction, Vector3.up) * Vector3.forward * speed;
    }
}
