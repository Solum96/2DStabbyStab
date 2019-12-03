using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float speed;
    // Update is called once per frame

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            myRigidbody.velocity = transform.up * speed;
        }
    }
}
