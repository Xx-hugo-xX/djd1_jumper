using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float     moveSpeed = 50.0f;
    [SerializeField] Transform groundCensor;

    float moveDirection = -1.0f;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;
            
        currentVelocity.x = moveDirection * moveSpeed;

        rigidBody.velocity = currentVelocity;

        if (moveDirection < 0.0f) transform.rotation = Quaternion.identity;
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        Collider2D collider = Physics2D.OverlapCircle(groundCensor.position,
            2.0f, LayerMask.GetMask("Ground"));

        if (collider == null)
        {
            moveDirection = -moveDirection;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (groundCensor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundCensor.position, 0.5f);
        }
    }
}