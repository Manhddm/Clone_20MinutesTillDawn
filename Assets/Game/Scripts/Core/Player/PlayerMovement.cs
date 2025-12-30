using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    

    public void Movement(float speed, Vector2 move, bool isShooting = false)
    {
        if (move == Vector2.zero) return;
        if (isShooting) speed /= 2;
        transform.Translate(move * (speed * Time.deltaTime));
    }

}
