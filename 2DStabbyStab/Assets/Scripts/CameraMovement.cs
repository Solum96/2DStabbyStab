using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float yAxis;
    public float zAxis;
    public float smoothing;
    // Update is called once per frame
    void Start()
    {
        yAxis = transform.position.y;
        zAxis = transform.position.z;
    }
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yAxis, target.position.z + zAxis);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
