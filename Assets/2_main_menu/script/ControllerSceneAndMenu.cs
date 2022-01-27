using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSceneAndMenu : MonoBehaviour
{
    public GameObject buttonsMenu;
    public GameObject buttonsExit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowExitButtons()
    {
        buttonsMenu.SetActive(false);
        buttonsExit.SetActive(true);
    }

    public void BackInMenu()
    {
        buttonsMenu.SetActive(true);
        buttonsExit.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGameLoadSceneSampleScene()
    {
        Application.LoadLevel("SampleScene");
    }
}
