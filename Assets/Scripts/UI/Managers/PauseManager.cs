using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager SharedInstance;
    PlayerInteraction playerInteraction;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject confirmExit;
    [SerializeField] GameObject confirmMainMenu;
    [SerializeField] GameObject tutoPanel;
    [SerializeField] GameObject configPanel;
    bool isOpened;
    public bool IsOpened => isOpened;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        playerInteraction = PointAndClickMovement.SharedInstance.gameObject.GetComponent<PlayerInteraction>();
    }
    public void PausePanelSwitcher(bool state)
    {
        pausePanel.SetActive(state);
        playerInteraction.enabled = !state;
        isOpened = state;
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(!state);
        MainButtonsManager.SharedInstance.onMainButtonClicked.Invoke();
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
            ScenesChanger.SharedInstance.SceneChange(ConstManager.mainMenuSceneName);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
