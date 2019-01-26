using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] internal string horizontalInputName = "Horizontal";
    [SerializeField] internal string verticalInputName = "Vertical";

    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float drag = 0.0f;
    [SerializeField] float maxVelocity = 0.0f;

    internal Vector3 velocity = Vector3.zero;

    private bool invertedControls = false;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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

        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            var direction = (new Vector3(horizontal, 0.0f, vertical)).normalized;
            velocity += direction * acceleration * Time.deltaTime;
            if (velocity.magnitude > maxVelocity)
            {
                velocity = direction * maxVelocity;
            }
        }
    }

    void FixedUpdate()
    {
        velocity *= 1 - (drag * Time.deltaTime);
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }

    public void ToggleInvertControls(bool on)
    {
        invertedControls = on;
    }
}
