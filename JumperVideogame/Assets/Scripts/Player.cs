﻿using System.Collections;
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


    [SerializeField] Transform      startPosition;
    [SerializeField] Transform      checkpoint_1;
    [SerializeField] Transform      checkpoint_2;
    [SerializeField] Transform      checkpoint_3;


    [SerializeField] float teleportCooldown = 3.0f;


    Transform respawnPoint;
    
    Rigidbody2D     rigidBody;
    Animator        animator;
    SpriteRenderer  sprite;
    float           hAxis;
    public int      currentHP;
    public int      teleportsAvailable = 2;
    float           invulnerabilityTimer;


    float playerDeathWait = 2.0f;

    bool isWalking;
    bool isFalling;

    float teleportTimer;
    bool isTeleportCharging;

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
        transform.position = startPosition.position;

        teleportTimer = 0.0f;
        isTeleportCharging = false;
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rigidBody.velocity;

        currentVelocity = new Vector2(hAxis * moveSpeed, currentVelocity.y);

        bool grounded = isOnGround;


        rigidBody.velocity = currentVelocity;

        groundCollider.enabled = grounded;
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


        // Start of Animation Updates
        if ((hAxis < 0.0f) && (transform.right.x > 0.0f))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if ((hAxis > 0.0f) && (transform.right.x < 0.0f))
        {
            transform.rotation = Quaternion.identity;
        }


        if (Mathf.Abs(rigidBody.velocity.x) > 0 && rigidBody.velocity.y == 0) isWalking = true;
        else isWalking = false;


        if (!isOnGround && rigidBody.velocity.y < 0) isFalling = true;
        else if(isOnGround) isFalling = false;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isFalling", isFalling);
        animator.SetFloat("ySpeed", rigidBody.velocity.y);

        if(teleportsAvailable < 2 && !isTeleportCharging)
        {
            isTeleportCharging = true;
            teleportTimer = teleportCooldown;
        }

        if (isTeleportCharging)
        {
            if (teleportTimer > 0.0f) teleportTimer -= Time.deltaTime;
            else
            {
                isTeleportCharging = false;
                teleportsAvailable += 1;
            }
        }
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

        if (collider.tag == "Victory") levelManager.WinScreen();

        if (collider.tag == "Elevator_1")
        {
            respawnPoint = checkpoint_1;
            transform.position = checkpoint_1.position;
        }

        if (collider.tag == "Elevator_2")
        {
            respawnPoint = checkpoint_2;
            transform.position = checkpoint_2.position;
        }

        if (collider.tag == "Elevator_3")
        {
            respawnPoint = checkpoint_3;
            transform.position = checkpoint_3.position;
        }
    }

    public void TakeDamage(int nDamage)
    {
        if (isInvulnerable) return;
        currentHP = currentHP - nDamage;
        isInvulnerable = true;

        if (currentHP <= 0)
        {
            transform.position = respawnPoint.position;
            currentHP = maxHP;
        }
    }

    IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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