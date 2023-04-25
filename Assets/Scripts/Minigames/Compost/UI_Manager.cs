using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] Button acceptButton;
    [SerializeField] TextMeshProUGUI confirmPanelText;

    public void PausePanelSwitcher(bool state)
    {
        pausePanel.SetActive(state);
    }

    public void ConfirmPanelSwithcer(bool state)
    {
        confirmPanel.SetActive(state);
    }

    public void ConfirmPanelForMainMenu()
    {
        confirmPanelText.text = "¿Estas seguro que quieres ir al Menú Principal?";
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(delegate { ScenesChanger.SharedInstance?.SceneChange(ConstManager.mainMenuSceneName); });
    }
    public void ConfirmPanelForExit()
    {
        confirmPanelText.text = "¿Estas seguro que quieres salir del juego?";
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(Application.Quit);
    }
}
