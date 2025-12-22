using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public CharacterData characterData;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private void OnValidate()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned in PlayerSetup.");
            return;
        }

        spriteRenderer.sprite = characterData.characterSprite;
        animator.runtimeAnimatorController = characterData.AnimatorController;
        playerStats.Initialize(characterData);
    }
}
