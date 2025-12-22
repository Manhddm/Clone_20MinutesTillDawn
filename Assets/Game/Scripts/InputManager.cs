using System;
using System.Collections;
using System.Collections.Generic;
using CommonScripts;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public InputController inputController;
    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        if (inputController == null)
        {
            inputController = new InputController();
        }
        //Subscribe to input events
        inputController.Player.Movement.performed += context => MoveInput = context.ReadValue<Vector2>();
        inputController.Player.Movement.canceled += context => MoveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputController.Enable();
    }
    private void OnDisable()
    {
        inputController.Disable();
    }
}
