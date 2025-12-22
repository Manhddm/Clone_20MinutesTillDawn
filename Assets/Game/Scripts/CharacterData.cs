using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacter", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject 
{
    public string characterName;
    public int maxHealth;
    public float moveNormalSpeed;
    public float moveAttackSpeed;
    public float attackRange;
    public Sprite uiCharacterSprite;
    public Sprite characterSprite;
    public RuntimeAnimatorController AnimatorController;
    
}
