using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variable Declaration
    public float moveSpeed = 50.0f;
    public float jumpSpeed = 250.0f;

    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D airCollider;

    Rigidbody2D rigidBody;
    Animator animator;
    float hAxis;


    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position
                + new Vector3(1, -20, 0), 2.0f, LayerMask.GetMask("Ground"));
            return collider;
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

        bool grounded = isOnGround;

        // moves player on the vertical axis when the user presses Jump button and is on ground
        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround)
                currentVelocity.y = jumpSpeed;
        }


        rigidBody.velocity = currentVelocity;

        // Defines which collider is utilized 
        groundCollider.enabled = grounded;
        airCollider.enabled = !grounded;


    }


    private void Update()
    {

        hAxis = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rigidBody.velocity;

        // Start of Animation Updates
        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }

            // Begins Walking animation when the player 
        if (Mathf.Abs(rigidBody.velocity.x) > 0 && rigidBody.velocity.y == 0)
        {
            animator.SetBool("isWalking", true);
        }
            // Stops Walking animation when the player's X velocity is 0 (begins Idle animation)
        else
        {
            animator.SetBool("isWalking", false);
        }
            // Sets animation to Jumping animation when the player jumps
        if (!isOnGround && rigidBody.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
            // Stops Jumping animation and begins Falling animation when the player begins to fall
        if (!isOnGround && rigidBody.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
            // Stops Jumping or Falling animation when player hits the ground
        if (isOnGround)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        // End of Animation Updates
    }
}