using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    private void OnEnable()
    {
        GameEventManager.OnPlayerShoot += FireProjectile;
    }
    private void OnDisable()
    {
        GameEventManager.OnPlayerShoot -= FireProjectile;
    }

    private void FireProjectile(bool isShooting)
    {
        if (isShooting)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * projectileSpeed;
    }
    
    public void HandleTransform(Vector3 playerPosition, Vector3 playerToMouseDirection, float radius )
    {
        transform.position = playerPosition + (playerToMouseDirection.normalized * radius);
        transform.right = playerToMouseDirection;
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else 
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}
