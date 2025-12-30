using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private PlayerMovement playerMovement;
    private Vector2 moveInput;
    void Update()
    {
        var speed = playerSpeed;
        //Get Input
        moveInput = InputManager.Instance.MoveInput;
        if (InputManager.Instance.ShotInput)
        {
            GameEventManager.OnPlayerShot?.Invoke();
            speed = speed*0.5f; //Reduce speed while shooting
        }
        
        //Move Player
        playerMovement.Movement(speed, moveInput);
    }
}
