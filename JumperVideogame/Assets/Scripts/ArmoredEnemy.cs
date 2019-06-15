using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : MonoBehaviour
{
    [SerializeField] float      moveSpeed = 50.0f;
    [SerializeField] int        maxHP = 4;
    [SerializeField] Transform  groundSensor;
    [SerializeField] Transform  wallSensor;
    [SerializeField] Collider2D damageSensor;

    float waitTime = 2.0f;
    float moveDirection = -1.0f;
    int   currentHP;


    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rigidBody.velocity;
            
        currentVelocity.x = moveDirection * moveSpeed;

        rigidBody.velocity = currentVelocity;

        if (moveDirection < 0.0f) transform.rotation = Quaternion.identity;
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        Collider2D collider = Physics2D.OverlapCircle(groundSensor.position,
            2.0f, LayerMask.GetMask("Ground"));


        if (collider == null)
        {
            moveDirection = -moveDirection;
            StartCoroutine(TurnPause(waitTime));
        }
        else
        {
            collider = Physics2D.OverlapCircle(wallSensor.position,
            2.0f, LayerMask.GetMask("Ground"));

            if (collider != null) moveDirection = -moveDirection;
        }

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.ClearLayerMask();
        contactFilter.SetLayerMask(LayerMask.GetMask("Player"));

        Collider2D[] results = new Collider2D[8];

        int nCollisions = Physics2D.OverlapCollider(damageSensor, contactFilter, results);

        if (nCollisions > 0)
        {
            for (int i = 0; i < nCollisions; i++)
            {
                collider = results[i];
                
                Player player = collider.GetComponent<Player>();

                if (player)
                {
                    player.TakeDamage(1);
                }
            }
        }
    }

    public void TakeDamage(int nDamage)
    {

        currentHP = currentHP - nDamage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator TurnPause(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundSensor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundSensor.position, 0.5f);
        }

        if (wallSensor)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(wallSensor.position, 0.5f);
        }
    }
}