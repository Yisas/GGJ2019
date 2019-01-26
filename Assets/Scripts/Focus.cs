using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    [SerializeField] Transform source;
    [SerializeField] Transform target;
    [SerializeField] float distance = 10.0f;

    Light light;
    Camera camera;

    void Start()
    {
        light = GetComponent<Light>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        var point = camera.WorldToViewportPoint(source.position);
        point = new Vector3(
            point.x,
            point.y,
            distance
        );
        light.transform.position = camera.ViewportToWorldPoint(point);
        var orientation = target.position - light.transform.position;
        light.transform.rotation = Quaternion.LookRotation(orientation);
    }
}
