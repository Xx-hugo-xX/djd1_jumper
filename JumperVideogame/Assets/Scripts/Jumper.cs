using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{

    public float moveSpeed = 50.0f;
    public float jumpVelocity = 250.0f;
    public float jumpTime = 0.15f;

    float timeOfJump;
    float hAxis;
    bool isJumpPressed;


    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Ground"));

            if (collider != null) return true;

            return false;
        }
    }


    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);


        if (isJumpPressed)
        {
            if (isOnGround)
            {
                rigidBody.gravityScale = 1.0f;
                currentVelocity.y = jumpVelocity;
                timeOfJump = Time.time;
            }
            else if ((Time.time - timeOfJump) < jumpTime)
            {
                rigidBody.gravityScale = 1.0f;
            }
            else
            {
                rigidBody.gravityScale = 4.0f;
            }
        }
        else
        {
            timeOfJump = -1000.0f;
            rigidBody.gravityScale = 4.0f;
        }



        rigidBody.velocity = currentVelocity;
    }

    private void Update()
    {

        Vector2 currentVelocity = rigidBody.velocity;

        hAxis = Input.GetAxis("Horizontal");
        isJumpPressed = Input.GetButton("Jump");


        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
