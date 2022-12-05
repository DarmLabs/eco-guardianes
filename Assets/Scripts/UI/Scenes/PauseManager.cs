using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject confirmExit;
    [SerializeField] GameObject confirmMainMenu;
    [SerializeField] GameObject tutoPanel;
    [SerializeField] GameObject configPanel;
    public void PausePanelSwitcher(bool state)
    {
        pausePanel.SetActive(state);
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(!state);
    }
    public void ConfirmExitSwitcher(bool state)
    {
        confirmExit.SetActive(state);
    }
    public void ConfirmMainMenuSwitcher(bool state)
    {
        confirmMainMenu.SetActive(state);
    }
    public void TutorialPanelSwitcher(bool state)
    {
        tutoPanel.SetActive(state);
    }
    public void ConfigPanelSwitcher(bool state)
    {
        configPanel.SetActive(state);
    }
    public void MainMenuButton()
    {
        if (ScenesChanger.SharedInstance != null)
        {
            ScenesChanger.SharedInstance.SceneChange("MainMenu");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
