using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private LayerMask targetLayer;
    public float Speed { get => speed; set => speed = value; }
    public int Damage => damage;
    

    private void Update()
    {
        MoveAndCheckCollision();
    }

    private void MoveAndCheckCollision()
    {
        float moveDistance = speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, moveDistance, targetLayer);
        if (hit.collider != null)
        {
            OnHit(hit.collider);
        }
        else
        {
            transform.Translate(Vector3.right * moveDistance);
        }
    }

    private void OnHit(Collider2D hitCollider)
    {
        HealthSystem damageAble = hitCollider.GetComponent<HealthSystem>();
        if (damageAble != null)
        {
            damageAble.TakeDamage(damage);
            damageAble.Knockback(transform.right, force);
        }
        Destroy(gameObject);
    }
}
