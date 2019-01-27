using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAvoid : MonoBehaviour
{
    public float smoothness = 1;

    float checkHeight = 50.0f;
    float initialHeight;

    void Start()
    {
        initialHeight = transform.position.y;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, ComputeOptimalHeight(), transform.position.z),
            Time.deltaTime / smoothness
        );
    }

    float ComputeOptimalHeight()
    {
        var distance = DistanceAt(transform.position);
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * 1));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * 2));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * 3));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * 4));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * 5));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * -1));
        distance = Mathf.Min(distance, DistanceAt(transform.position + transform.forward * -2));
        if (distance > checkHeight)
        {
            return initialHeight;
        }
        else
        {
            return initialHeight + (initialHeight + checkHeight - distance) + initialHeight;
        }
    }

    float DistanceAt(Vector3 position)
    {
        RaycastHit hit;
        Physics.Raycast(new Vector3(position.x, initialHeight + checkHeight, position.z), Vector3.down, out hit);
        return hit.distance;
    }
}
