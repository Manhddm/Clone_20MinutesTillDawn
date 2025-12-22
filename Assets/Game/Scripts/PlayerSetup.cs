using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public CharacterData characterData;

    // Replace serialized private fields with serialized auto-properties (private setter)
    public string PlayerName { get; private set; }
    public int MaxHealth { get; private set; }

    public float NormalSpeed { get; private set; }
    public float AttackSpeed { get; private set; }
    public float AttackRange { get; private set; }

    private void Awake()
    {
        // Initialize player attributes from character data
        if (characterData != null)
        {
            PlayerName = characterData.characterName;
            MaxHealth = characterData.maxHealth;
            NormalSpeed = characterData.moveNormalSpeed;
            AttackSpeed = characterData.moveAttackSpeed;
            AttackRange = characterData.attackRange;
        }
        else
        {
            Debug.LogError("CharacterData is not assigned in PlayerSetup.");
        }
    }

    private void Start()
    {
        // Set up player based on character data
        GetComponent<SpriteRenderer>().sprite = characterData.characterSprite;
        GetComponent<Animator>().runtimeAnimatorController = characterData.AnimatorController;
        // Additional setup like health, speed, etc. can be done here
    }
}
