using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager 
{
    //Game Events
    public static Action OnGameStart;
    public static Action OnGameOver;
    // Player Events
    public static Action<bool> OnPlayerShoot;
    public static Action OnPlayerDeath;
    public static Action OnPlayerLevelUp;
    public static Action<bool> OnPlayerMove;
    // Enemy Events
    public static Action OnEnemySpawn;
    public static Action OnEnemyDeath;
    
    //Functions to invoke events
    public static void GameStart() => OnGameStart?.Invoke();
    public static void GameOver() => OnGameOver?.Invoke();
    public static void PlayerShoot(bool isValue = false) => OnPlayerShoot?.Invoke(isValue);
    public static void PlayerDeath() => OnPlayerDeath?.Invoke();
    public static void PlayerMove(bool isValue = false) => OnPlayerMove?.Invoke(isValue);
    public static void PlayerLevelUp() => OnPlayerLevelUp?.Invoke();
    public static void EnemySpawn() => OnEnemySpawn?.Invoke();
    public static void EnemyDeath() => OnEnemyDeath?.Invoke();
    
}
