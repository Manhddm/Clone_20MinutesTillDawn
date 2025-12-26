using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Image characterImageUI;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text characterHP;
    [SerializeField] private TMP_Text characterDescription;
    
    private CharacterSelectButton characterSelectButton;
    public void SetCharacterImage(CharacterSelectButton btn)
    {
        var oldBtn = GetCharacterSelectButton();
        if (oldBtn != null && oldBtn != btn)
        {
            oldBtn.Deselect();
        }
        btn.Select();
        characterSelectButton = btn;
        GameManager.Instance.CharacterSelected = characterSelectButton.characterData;
        UpdateUI();
    }

    private void UpdateUI()
    {
                
        characterImageUI.sprite = this.characterSelectButton.characterData.uiCharacterSprite;
        characterName.SetText(characterSelectButton.characterData.name);
        characterHP.text = characterSelectButton.characterData.maxHealth.ToString();
        // Debug.Log(characterSelectButton.characterData.maxHealth);
        //characterDescription.SetText(characterSelectButton.characterData.description); 
    }
    public CharacterSelectButton GetCharacterSelectButton()
    {
        return this.characterSelectButton;
    }
}
