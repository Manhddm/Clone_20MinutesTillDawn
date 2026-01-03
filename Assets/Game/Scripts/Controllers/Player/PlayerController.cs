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
    [SerializeField] private WeaponController weaponController;
    private Vector2 _moveInput;
    private float _nextFireTime;

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponentInChildren<PlayerMovement>();
        if (weaponController == null)
            weaponController = GetComponentInChildren<WeaponController>();
    }

    void Update()
    {

        //Get Input
        _moveInput = InputManager.Instance.MoveInput;
        var isShooting = InputManager.Instance.ShotInput;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        mousePosition.z = 0;
        weaponController.HandleTransform(playerMovement.gameObject.transform.position, mousePosition -  playerMovement.gameObject.transform.position, weaponRadius);
        //Fire 
        if (isShooting)
        {
            if (Time.time >= _nextFireTime)
            {
                GameEventManager.PlayerShoot(true);
                playerMovement.FlipSprite(true, mousePosition -  playerMovement.gameObject.transform.position);
                _nextFireTime = Time.time + 0.3f; // Fire rate limit
            }
        }
        else
        {
            GameEventManager.PlayerShoot();
            playerMovement.FlipSprite(false, mousePosition -  playerMovement.gameObject.transform.position);
        }
        
        //Move Player
        playerMovement.Movement(playerSpeed, _moveInput);

    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRadius);
    }
#endif
    
}
