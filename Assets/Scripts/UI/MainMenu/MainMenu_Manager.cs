using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] previewPj;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject creditsPanel;
    void Start()
    {
        if (SceneCache.SharedInstance.previousScene == "CharacterCreation")
        {
            PJ_PreviewAnimEvent.SharedInstance.transform.SetParent(mainMenuPanel.transform);
        }
        else
        {
            foreach (var item in previewPj)
            {
                item.SetActive(true);
            }
        }
    }
    public void EnterGame(string sceneName)
    {
        ScenesChanger.SharedInstance.SceneChange(sceneName);
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
