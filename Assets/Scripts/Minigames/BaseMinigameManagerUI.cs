using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BaseMinigameManagerUI : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] protected TextMeshProUGUI scoreText;
    [SerializeField] protected TextMeshProUGUI livesText;
    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;
    [Header("Endgame Panel")]
    [SerializeField] GameObject endGamePanel;
    [SerializeField] protected TextMeshProUGUI titleText;
    [SerializeField] protected TextMeshProUGUI endScore;
    [SerializeField] protected TextMeshProUGUI endLives;
    [SerializeField] Button restartBtn, nextBtn;
    [Header("Confirm Exit Panel")]
    [SerializeField] GameObject confirmPanel;
    [SerializeField] TextMeshProUGUI confirmText;
    [SerializeField] Button acceptBtn;
    [SerializeField] Button cancelBtn;
    void Start()
    {
        restartBtn.onClick.AddListener(() => ScenesChanger.SharedInstance?.ReloadScene());
        nextBtn.onClick.AddListener(() => ScenesChanger.SharedInstance?.SceneChange(SceneCache.SharedInstance?.previousScene));
        LoadingPopupManager.SharedInstance?.LoadingPopupSwitcher(false);
    }
    public virtual void SwitchPausePanel(bool state)
    {
        pausePanel.SetActive(state);
    }
    public virtual void UpdateScore()
    {

    }
    public virtual void UpdateLives()
    {

    }
    public virtual void ActivateEndPanel()
    {

    }
    public virtual void SwitchEndgamePanel(bool state)
    {
        endGamePanel.SetActive(state);
    }
    public void BuildConfirmPanel(bool isForMainMenu)
    {
        confirmText.text = isForMainMenu ? "¿DESEA SALIR AL MENÚ PRINCIPAL?" : "¿DESEA SALIR DEL JUEGO?";

        cancelBtn.onClick.RemoveAllListeners();
        acceptBtn.onClick.RemoveAllListeners();

        if (isForMainMenu)
        {
            acceptBtn.onClick.AddListener(() => ScenesChanger.SharedInstance?.SceneChange(ConstManager.mainMenuSceneName));
            cancelBtn.onClick.AddListener(() => { SwitchConfirmPanel(false); SwitchEndgamePanel(true); });
        }
        else
        {
            acceptBtn.onClick.AddListener(Application.Quit);
            cancelBtn.onClick.AddListener(() => { SwitchConfirmPanel(false); SwitchPausePanel(true); });
        }

        SwitchConfirmPanel(true);
    }
    public void SwitchConfirmPanel(bool state)
    {
        confirmPanel.SetActive(state);
    }
}
