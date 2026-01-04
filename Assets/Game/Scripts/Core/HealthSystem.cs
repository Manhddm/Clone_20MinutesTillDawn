using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageAble
{
    [SerializeField] private int maxHealth = 100;
    private int _currentHealth;
    private void Awake()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void Knockback(Vector2 direction, float force)
    {
        StartCoroutine(HandleKnockback(direction, force));
    }

    private IEnumerator HandleKnockback(Vector2 direction, float force)
    {
        Vector2 knockbackVelocity = direction.normalized * force;
        float knockbackDuration = 0.2f;
        float elapsedTime = 0f;
        while ( elapsedTime < knockbackDuration)
        {
            transform.Translate(knockbackVelocity * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
