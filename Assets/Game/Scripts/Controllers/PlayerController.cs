using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private PlayerMovement playerMovement;
    [Range(0.1f,1f)]
    [SerializeField] private float weaponRadius = 0.2f;
    private Vector2 _moveInput;
    private float _nextFireTime;
    void Update()
    {

        //Get Input
        _moveInput = InputManager.Instance.MoveInput;
        var isShooting = InputManager.Instance.ShotInput;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Fire 
        if (isShooting)
        {
            if (Time.time >= _nextFireTime)
            {
                GameEventManager.OnPlayerShot?.Invoke();
                _nextFireTime = Time.time + 0.3f; // Fire rate limit
            }
        }
        
        //Move Player
        playerMovement.Movement(playerSpeed, _moveInput, isShooting);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRadius);
    }
#endif
    
}
