using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.LoadLevel(Levels.Game);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings() 
    {
    
    
    }
}
