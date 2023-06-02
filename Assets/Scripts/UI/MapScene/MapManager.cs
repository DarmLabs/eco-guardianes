using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MapManager : MonoBehaviour
{
    StarsData starsData;
    AchievementsData achievementsData;
    int globalStars = 0;
    [SerializeField] TextMeshProUGUI currentStarsText;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject secondaryConfirmationPanel;
    [SerializeField] GameObject starsHUD;
    [SerializeField] TextMeshProUGUI sceneText;
    [SerializeField] GameObject newMapMark;
    string sceneForward;
    [SerializeField] RequieredStars[] sceneStars;
    void Awake()
    {
        starsData = SaveDataHandler.SharedInstance?.LoadStarsData();
        if (starsData != null)
        {
            globalStars = starsData.starsCasaCount + starsData.starsEscuelaCount + starsData.starsPlazaCount;
        }
    }
    void Start()
    {
        currentStarsText.text = $"¡Tienes {globalStars} estrellas!";

        for (int i = 0; i < sceneStars.Length; i++)
        {
            if (sceneStars[i].GetRequieredStars <= globalStars)
            {
                sceneStars[i]._Button.interactable = true;
                sceneStars[i]._Image.color = Color.white;
                sceneStars[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (starsData.starsCasaCount == 3 && !starsData.newMapMarkPassed)
        {
            newMapMark.SetActive(true);
        }
    }
    public void OnYesButton()
    {
        LoadingPopupManager.SharedInstance?.LoadingPopupSwitcher(true);
        ScenesChanger.SharedInstance?.SceneChange(sceneForward);
    }
    public void OnNoButton()
    {
        ConfirmsPanelsSwitcher(false, confirmationPanel);
    }
    public void OnBackButton()
    {
        ScenesChanger.SharedInstance?.SceneChange(ConstManager.mainMenuSceneName);
    }
    public void OnSceneButton(string scene)
    {
        ScenesChanger.SharedInstance?.SceneChange(scene);
    }
    public void CloseSecondaryConfirmPanel()
    {
        ConfirmsPanelsSwitcher(false, secondaryConfirmationPanel);
    }
    public void OnSceneButtonClicked(string sceneForward)
    {
        if (sceneForward == "Casa" && starsData?.starsCasaCount == 3)
        {
            ConfirmsPanelsSwitcher(true, secondaryConfirmationPanel);
            starsData.newMapMarkPassed = true;
            newMapMark.SetActive(false);
            SaveDataHandler.SharedInstance.SaveStarsData(starsData);
        }
        else
        {
            this.sceneForward = sceneForward;
            sceneText.text = $"¿Quieres ir a la {sceneForward}?";
            ConfirmsPanelsSwitcher(true, confirmationPanel);
        }

    }
    void ConfirmsPanelsSwitcher(bool state, GameObject confirmPanel)
    {
        starsHUD.SetActive(!state);
        confirmPanel.SetActive(state);
    }
}
