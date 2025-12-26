using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public CharacterData characterData;
    public CharacterSelectUI characterSelectUI;
    public SpriteRenderer characterSpriteRenderer;
    public Animator animator;
    public Button selectButton;
    // Start is called before the first frame update
    void Start()
    {
        characterSpriteRenderer.sprite = characterData.characterSprite;
        animator.runtimeAnimatorController = characterData.AnimatorController;
        characterSelectUI = FindObjectOfType<CharacterSelectUI>();
        selectButton.onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
        characterSelectUI.SetCharacterImage(this);
        animator.SetBool("Moving", true);
        Debug.Log("Selected character: " + characterData.characterName);
    }
    public void Select()
    {
        animator.SetBool("Moving", true);
        selectButton.interactable = false;
    }
    public void Deselect()
    {
        animator.SetBool("Moving", false);
        Debug.Log("Deselected character: " + characterData.characterName);
        selectButton.interactable = true;
    }
}
