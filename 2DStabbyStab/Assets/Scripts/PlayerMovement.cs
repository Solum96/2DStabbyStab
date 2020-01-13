using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState {Walking, Attacking, Jumping}

public class PlayerMovement : MonoBehaviour
{
    private PlayerState currentState;
    private Rigidbody myRigidbody;
    private Collider playerCollider;
    private float distanceToGround;
    public float speed;
    public float jump;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        currentState = PlayerState.Walking;
        animator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        distanceToGround = playerCollider.bounds.extents.y;
    }
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.Mouse0) && currentState != PlayerState.Attacking)
        {
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.Walking)
        {
            UpdateAnimationAndMove();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && currentState == PlayerState.Walking)
        {
            PlayerJump();
        }

        if (currentState == PlayerState.Jumping)
        {
            if (myRigidbody.velocity.y < 0 && IsGrounded())
            {
                currentState = PlayerState.Walking;
            } 
        }
    }

    IEnumerator AttackCo()
    {
        animator.SetBool("isAttacking", true);
        currentState = PlayerState.Attacking;
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(.35f);
        currentState = PlayerState.Walking;
    }

    private void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MovePlayer();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveZ", change.z);
            animator.SetBool("isWalking", true);
        }
        else { animator.SetBool("isWalking", false); }
    }

    void MovePlayer()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void PlayerJump()
    {
        currentState = PlayerState.Jumping;
        myRigidbody.velocity.Normalize();
        myRigidbody.AddForce(new Vector3(0,1,0) * jump, ForceMode.VelocityChange);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(playerCollider.transform.position, -Vector3.up, distanceToGround + 0.5f);
    }
}
