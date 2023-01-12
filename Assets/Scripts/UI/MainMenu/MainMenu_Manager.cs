using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Manager : MonoBehaviour
{
    const string mapSelectorSceneName = "MapScene";
    const string characterCreationSceneName = "CharacterCreation";
    bool isCharacterCreated;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject characterCreatorButton;
    void Start()
    {
        isCharacterCreated = SaveDataHandler.SharedInstance.LoadFirstTime();
        characterCreatorButton.SetActive(isCharacterCreated);
    }
    public void EnterGame()
    {
        if (ScenesChanger.SharedInstance != null)
        {
            string sceneName = isCharacterCreated ? mapSelectorSceneName : characterCreationSceneName;
            ScenesChanger.SharedInstance.SceneChange(sceneName);
        }
    }
    public void EnterCharacterCreation()
    {
        ScenesChanger.SharedInstance.SceneChange(characterCreationSceneName);
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
