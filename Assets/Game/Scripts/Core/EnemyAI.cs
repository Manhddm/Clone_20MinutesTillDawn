using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private float separationForce = 2f;
    [SerializeField] private LayerMask enemyLayer;

    private void Update()
    {
        if (player == null) return;
        
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 separation = CalculateSeparation();
        Vector2 moveDirection = (directionToPlayer + separation).normalized;

        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }

    Vector2 CalculateSeparation()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        Vector2 separation = Vector2.zero;
        foreach (Collider2D neighbor in neighbors)
        {
            if (neighbor.transform != transform)
            {
                Vector2 directionAway = (Vector2)(transform.position - neighbor.transform.position);
                float distance = directionAway.magnitude;
                if (distance > 0)
                {
                    separation += directionAway.normalized / distance;
                }
            }
        }
        return separation * separationForce;
    }
    public void SetTarget(Transform target)
    {
        player = target;
    }

}
