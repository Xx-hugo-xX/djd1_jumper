using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isAttacking;
    float attackTimer;
    float attackCooldown = 0.5f;

    public Collider2D attackTrigger;

    private Animator animator;



    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        attackTimer = 0.0f;
        isAttacking = false;
        attackTrigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && !isAttacking)
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
        animator.SetBool("isAttacking", isAttacking);

        Debug.Log("Attacking" + isAttacking);
    }
}
