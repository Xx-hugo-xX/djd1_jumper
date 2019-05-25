using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 50.0f;
    public float jumpSpeed = 250.0f;


    Rigidbody2D rigidBody;
    Animator animator;
    float hAxis;


    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));
            return (collider);
        }
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            currentVelocity.y = jumpSpeed;
        }

        rigidBody.velocity = currentVelocity;

    }

    private void Update()
    {

        hAxis = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rigidBody.velocity;


        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

        if (Mathf.Abs(rigidBody.velocity.x) > 0 && rigidBody.velocity.y == 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (rigidBody.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        /*
        if (rigidBody.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
            Debug.Log("Still Falling");
        }
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2.0f);
    }
}
