using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float startDistance;
    [SerializeField] float transitionDistance = 1;

    Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        var vector = target.transform.position - transform.position;
        var distance = vector.magnitude;
        if (distance < startDistance)
        {
            transform.rotation = Quaternion.Lerp(
                initialRotation,
                Quaternion.LookRotation(vector),
                (startDistance - distance) / transitionDistance
            );
        }
    }
}
