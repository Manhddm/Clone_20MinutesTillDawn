using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonScripts;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public InputSystem inputSystem;
    public Vector2 MoveInput { get; private set;  }
    public bool ShotInput { get; private set;  }
    private void Awake()
    {
        inputSystem = new InputSystem();
        //Move Input
        inputSystem.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputSystem.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
        //Fire Input
        inputSystem.Player.Fire.performed += ctx => ShotInput = true;
        inputSystem.Player.Fire.canceled += ctx => ShotInput = false;   
        
    }

    private void OnEnable()
    {
        inputSystem.Player.Enable();
    }
    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }
}
