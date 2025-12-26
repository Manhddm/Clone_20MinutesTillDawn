using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrBack : MonoBehaviour
{
    public CharacterSelectUI characterSelectUI;
    
    public void OnClickPlayButton()
    {
        if (characterSelectUI.characterImageUI.sprite == null)
        {
            Debug.LogWarning("No character selected!");
            return;
        }

        GameManager.Instance.CharacterSlected = characterSelectUI.GetCharacterSelectButton().characterData;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
