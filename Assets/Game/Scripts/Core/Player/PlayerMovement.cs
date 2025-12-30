using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public void Movement(float speed,Vector2 move)
    {
        if (move == Vector2.zero) return;
        transform.Translate(move*speed*Time.deltaTime);
    } 
    
}
