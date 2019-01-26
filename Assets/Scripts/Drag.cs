using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
    public float speed = 5;
    public float maxSpeed = 15;
    public static Transform dragTransform;
    private RaycastHit hit;
    private float length;

	void Update () {
        if (Input.GetMouseButton(0))
        {  
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!dragTransform)
            {
                if (Physics.Raycast(ray, out hit) && hit.rigidbody)
                {
                    dragTransform = hit.transform; 
                    length = hit.distance;
                }
            }
            else
            {
                var vel = (ray.GetPoint(length) - dragTransform.position) * speed;
                // limit max velocity to avoid pass through objects
                if (vel.magnitude > maxSpeed) vel *= maxSpeed / vel.magnitude;
                dragTransform.GetComponent<Rigidbody>().velocity = vel;
            }
        }
        else {
            dragTransform = null;
        }
	}
	
}
