using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Collider playerCollider;
    private float distanceToGround;
    public float speed;
    public float jump;
    private Vector3 change;
    private Animator animator;
    // Update is called once per frame

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        distanceToGround = playerCollider.bounds.extents.y;
    }
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");
        if(change != Vector3.zero)
        { 
            MovePlayer(); 
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveZ", change.z);
            animator.SetBool("isWalking", true); 
        }
        else { animator.SetBool("isWalking", false); }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            PlayerJump();
        }
    }

    void MovePlayer()
    {
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void PlayerJump()
    {
        myRigidbody.velocity.Normalize();
        myRigidbody.AddForce(new Vector3(0,1,0) * jump, ForceMode.VelocityChange);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.2f);
    }
}
