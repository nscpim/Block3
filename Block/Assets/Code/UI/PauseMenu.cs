using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.PauseGame(true);
        }

        if (GameManager.pause)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        GameManager.PauseGame(false);
        pauseMenuUI.SetActive(false);
    }

    
}
