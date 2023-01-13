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
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        bool isCharacterCreated = false;
        if (SaveDataHandler.SharedInstance != null)
        {
            isCharacterCreated = SaveDataHandler.SharedInstance.LoadFirstTime();
        }
        mainMenuButton.SetActive(isCharacterCreated);
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
        ScenesChanger.SharedInstance?.SceneChange(forwardScene);
        if (!SaveDataHandler.SharedInstance.LoadFirstTime())
        {
            SaveDataHandler.SharedInstance?.SaveFirstTime();
        }
    }
    public void OnBackButton()
    {
        ScenesChanger.SharedInstance?.SceneChange(SceneCache.SharedInstance.previousScene);
    }
}
