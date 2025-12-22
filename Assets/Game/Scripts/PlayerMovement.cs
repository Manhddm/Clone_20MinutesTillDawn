using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerSetup playerSetup;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private Vector2 movement;

    private void Awake()
    {
        if (playerSetup == null)
        {
            playerSetup = GetComponent<PlayerSetup>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        movement = InputManager.Instance.MoveInput;
        if (movement != Vector2.zero)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void FixedUpdate()
    {
        var speed = playerSetup.NormalSpeed;
        var newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
