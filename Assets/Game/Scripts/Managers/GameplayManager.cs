using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private EnemyAI enemyPrefab;
    [SerializeField] private int enemyCount = 10;
    private void Start()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        for (int  i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            EnemyAI enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.SetTarget(player);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnRadius = 10f;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * spawnRadius;
        return spawnPosition;
    }
}
