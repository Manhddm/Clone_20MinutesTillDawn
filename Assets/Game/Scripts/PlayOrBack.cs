using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrBack : MonoBehaviour
{
    
    public void OnClickPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneLoad.GamePlay);
    }
}
