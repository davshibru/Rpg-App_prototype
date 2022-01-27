using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSceneAndPouseMenu : MonoBehaviour
{

    public GameObject gameElements;
    public GameObject pauseElements;

    public void ShowPouseButtons()
    {
        gameElements.SetActive(false);
        pauseElements.SetActive(true);
    }

    public void BackInGame()
    {
        gameElements.SetActive(true);
        pauseElements.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenuLoadSceneSampleScene()
    {
        Application.LoadLevel("Main_menu");
    }
}
