using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Image characterImageUI;
    private CharacterSelectButton characterSelectButton;
    public void SetCharacterImage(CharacterSelectButton characterSelectButton)
    {
        if (this.characterSelectButton != null)
        {
            this.characterSelectButton.Deselect();
        }
        this.characterSelectButton = characterSelectButton;
        this.characterSelectButton.Select();
        
        characterImageUI.sprite = this.characterSelectButton.characterData.uiCharacterSprite;
    }

    public CharacterSelectButton GetCharacterSelectButton()
    {
        return this.characterSelectButton;
    }
}
