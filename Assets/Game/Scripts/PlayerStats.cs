using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public float CurrentMoveSpeed { get; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    
    public void Initialize(CharacterData characterData)
    {
        MaxHealth = characterData.maxHealth;
        CurrentHealth = MaxHealth;
        CurrentMoveSpeed = characterData.moveNormalSpeed; 
    }
}
