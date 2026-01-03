using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer _playerSprite; 
    [SerializeField] private bool isFacingRight = true;
    private bool isShooting;
    public void Movement(float speed, Vector2 move)
    {
        if (move == Vector2.zero)
        {
            GameEventManager.PlayerMove(false);
            return;
        }
        GameEventManager.PlayerMove(true);
        if (isShooting) speed /= 2;
        else FlipSprite(move.x > 0);
        transform.Translate(move * (speed * Time.deltaTime));
    }

    private void FlipSprite(bool isFacingRight)
    {
        
        if (this.isFacingRight != isFacingRight)
        {
            this.isFacingRight = isFacingRight;
            _playerSprite.flipX = !isFacingRight;
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
