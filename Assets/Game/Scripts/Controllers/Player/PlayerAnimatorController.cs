using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Shooting = Animator.StringToHash("Attacking");

    private void OnEnable()
    {
        GameEventManager.OnPlayerMove += HandlePlayerMove;
        GameEventManager.OnPlayerShoot += HandlePlayerShoot;
    }
    private void OnDisable()
    {
        GameEventManager.OnPlayerMove -= HandlePlayerMove;
        GameEventManager.OnPlayerShoot -= HandlePlayerShoot;
    }

    private void HandlePlayerMove(bool isMove)
    {
        animator.SetBool(Moving, isMove);
    }
    private void HandlePlayerShoot(bool isShooting)
    {
        animator.SetBool(Shooting, isShooting);
    }
}
