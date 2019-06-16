using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    // Variable Declaration

    [SerializeField] public float   moveSpeed = 50.0f;
    [SerializeField] int            maxHP = 3;
    [SerializeField] float          invulnerabilityDuration = 1.0f;
    [SerializeField] Collider2D     groundCollider;

    [SerializeField] Transform      groundSensor;
    [SerializeField] Transform      wallSensor;
    [SerializeField] Transform      damageSensor;

    [SerializeField] LevelManager   levelManager;


    [SerializeField] Collider2D enemyDamageSensor;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Collider2D attackTrigger;


    Rigidbody2D     rigidBody;
    Animator        animator;
    SpriteRenderer  sprite;
    float           hAxis;
    int             currentHP;
    float           invulnerabilityTimer;

    bool isAttacking;
    float attackTimer;
    float attackCooldown = 0.5f;
   


    bool isOnGround
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(groundSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            return collider;
        }
    }

    bool isAgainstWall
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(wallSensor.position, 2.0f, LayerMask.GetMask("Ground"));
            return collider;
        }
    }

    bool isInvulnerable
    {
        get
        {
            if (invulnerabilityTimer > 0.0f) return true;
            return false;
        }
        set
        {
            if (value) invulnerabilityTimer = invulnerabilityDuration;
            else invulnerabilityTimer = 0.0f;
        }
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator  = GetComponent<Animator>();
        sprite    = GetComponent<SpriteRenderer>();

        currentHP = maxHP;

        attackTrigger.enabled = false;
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

        bool grounded = isOnGround;


        rigidBody.velocity = currentVelocity;

        // Defines which collider is utilized 
        groundCollider.enabled = grounded;

        Collider2D collider = Physics2D.OverlapCircle(damageSensor.position,
            2.0f, LayerMask.GetMask("Enemy"));


        if (collider != null)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage(1);
            }
        }
    }


    private void Update()
    {

        hAxis = Input.GetAxis("Horizontal");

        Vector2 currentVelocity = rigidBody.velocity;

        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            sprite.enabled = (Mathf.FloorToInt(invulnerabilityTimer * 10.0f)%2) == 0;

            if (invulnerabilityTimer <= 0.0f)
            {
                sprite.enabled = true;
            }
        }


        if(Input.GetKeyDown("f") && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;

            attackTrigger.enabled = true;
        }

        if (isAttacking)
        {
            if (attackTimer > 0.0f)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                attackTrigger.enabled = false;
            }
        }

        /*
        if (enemyDamageSensor.IsTouching(playerCollider))
        {

        }
        */













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
            if (!isAgainstWall) animator.SetBool("isWalking", true);
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

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "MedKit")
        {

            Debug.Log("Touched Medkit");

            if (currentHP != 3)
            {
                currentHP = 3;
                Destroy(collider.gameObject);
            }
        }
        if (collider.tag == "SubwayEntry")
        {
            levelManager.UndergroundScene();
        }
        if (collider.tag == "SubwayEntry") levelManager.UndergroundScene();

        if (collider.tag == "Victory") levelManager.RestartScene();
    }

    public void TakeDamage(int nDamage)
    {
        if (isInvulnerable) return;
        currentHP = currentHP - nDamage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
            levelManager.RestartScene();
        }

        isInvulnerable = true;
    }


    private void OnDrawGizmosSelected()
    {
        if (damageSensor)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(damageSensor.position, 1.0f);
        }

        if (groundSensor)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(groundSensor.position, 2.0f);
        }
    }
}