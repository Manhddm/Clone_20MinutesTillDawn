using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer playerSprite; 
    [SerializeField] private bool isFacingRight = true;
    private bool isShooting;

    private void Awake()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (playerSprite == null)
        {
            playerSprite = GetComponent<SpriteRenderer>();
        }
    }

    public void Movement(float speed, Vector2 move)
    {
        if (move == Vector2.zero)
        {
            GameEventManager.PlayerMove(false);
            rigidbody2D.velocity = Vector2.zero;
            return;
        }
        GameEventManager.PlayerMove(true);
        if (isShooting) speed /= 2;
        else FlipSprite(move.x > 0);
        Vector2 movement = move.normalized * speed;
        rigidbody2D.velocity = movement;
    }

    private void FlipSprite(bool isFacingRight)
    {
        
        if (this.isFacingRight != isFacingRight)
        {
            this.isFacingRight = isFacingRight;
            playerSprite.flipX = !isFacingRight;
        }
    }
    public void FlipSprite(bool isShooting, Vector2 direction)
    {
        this.isShooting = isShooting;
        if (!isShooting) return;
        if (direction.x != 0)
        {
            FlipSprite(direction.x > 0);
        }
    }


}
