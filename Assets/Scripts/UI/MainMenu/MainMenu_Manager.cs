using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Manager : MonoBehaviour
{
    bool isCharacterCreated;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject characterCreatorButton;
    void Start()
    {
        isCharacterCreated = SaveDataHandler.SharedInstance.LoadMainMenuFirstTime();
        characterCreatorButton.SetActive(isCharacterCreated);
    }
    public void EnterGame()
    {
        if (ScenesChanger.SharedInstance != null)
        {
            string sceneName = isCharacterCreated ? ConstManager.mapSceneName : ConstManager.characterCreationSceneName;
            ScenesChanger.SharedInstance.SceneChange(sceneName);
        }
    }
    public void EnterCharacterCreation()
    {
        ScenesChanger.SharedInstance.SceneChange(ConstManager.characterCreationSceneName);
    }
    public void MainMenuPanel(bool state)
    {
        mainMenuPanel.SetActive(state);
    }
    public void CreditsPanel(bool state)
    {
        creditsPanel.SetActive(state);
    }
    public void ConfirmationPanel(bool state)
    {
        confirmationPanel.SetActive(state);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
