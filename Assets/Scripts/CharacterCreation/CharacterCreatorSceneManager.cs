using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterCreatorSceneManager : MonoBehaviour
{
    public static CharacterCreatorSceneManager SharedInstance;
    [SerializeField] string forwardScene;
    [SerializeField] Button saveButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] Toggle wheelChairToggle;
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject confirmExitPanel;
    MainMenuData mainMenuData;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        mainMenuData = SaveDataHandler.SharedInstance?.LoadMainMenuFirstTime();
        mainMenuButton.SetActive(mainMenuData.isCharacterCreated);
    }
    public void ConfirmPanelSwitcher(bool state)
    {
        mainPage.SetActive(!state);
        confirmExitPanel.SetActive(state);
    }
    public void SetWheelChairToggle(bool state)
    {
        wheelChairToggle.isOn = state;
    }
    public void CanPressSaveButton(bool state)
    {
        saveButton.interactable = state;
    }
    public void OnSaveButton()
    {
        if (!mainMenuData.isCharacterCreated)
        {
            mainMenuData.isCharacterCreated = true;
            SaveDataHandler.SharedInstance?.SaveMainMenuFirstTime(mainMenuData);
        }
        ScenesChanger.SharedInstance?.SceneChange(forwardScene);
    }
    public void OnBackButton()
    {
        ScenesChanger.SharedInstance?.SceneChange(SceneCache.SharedInstance.previousScene);
    }
}
