using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrow : MonoBehaviour
{
    public float smoothness = 1;

    Wind wind;
    Vector3 initialScale;

    void Start()
    {
        wind = GameObject.FindWithTag("Wind").GetComponent<Wind>();
        initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0.0f, 0.0f, -wind.direction + 90),
            Time.deltaTime / smoothness
        );
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            wind.speed / wind.maxSpeed * initialScale,
            Time.deltaTime / smoothness
        );
    }
}
