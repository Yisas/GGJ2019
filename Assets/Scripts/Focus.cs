using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    [SerializeField] Transform target;

    Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        var difference = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(difference);
        var distance = difference.magnitude;
        light.range = distance * 2;
        light.spotAngle = Mathf.Max(-0.05f * distance + 10.0f, 0.01f);
    }
}
