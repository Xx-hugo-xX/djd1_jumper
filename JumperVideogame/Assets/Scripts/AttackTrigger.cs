using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] int damage = 1;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.isTrigger && collider.CompareTag("Enemy"))
        {
            collider.SendMessageUpwards("TakeDamage", damage);
        }
    }
}