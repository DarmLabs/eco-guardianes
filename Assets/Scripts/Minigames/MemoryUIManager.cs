using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MemoryUIManager : MonoBehaviour
{
    public static MemoryUIManager SharedInstance;
    [Header("HUD")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;
    [Header("Endgame Panel")]
    [SerializeField] GameObject endGamePanel;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI endScore;
    [SerializeField] TextMeshProUGUI endLives;
    [SerializeField] Button restartBtn, nextBtn;
    [Header("Confirm Exit Panel")]
    [SerializeField] GameObject confirmPanel;
    [SerializeField] TextMeshProUGUI confirmText;
    [SerializeField] Button acceptBtn;
    [SerializeField] Button cancelBtn;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        restartBtn.onClick.AddListener(() => ScenesChanger.SharedInstance?.ReloadScene());
        nextBtn.onClick.AddListener(() => ScenesChanger.SharedInstance?.SceneChange(SceneCache.SharedInstance?.previousScene));
        LoadingPopupManager.SharedInstance?.LoadingPopupSwitcher(false);
    }
    public void SwitchPausePanel(bool state)
    {
        pausePanel.SetActive(state);
    }
    public void UpdateScore()
    {
        scoreText.text = MemoryManager.SharedInstance.Score.ToString();
    }
    public void UpdateLives()
    {
        livesText.text = MemoryManager.SharedInstance.Lives.ToString();
    }
    public void ActivateEndPanel()
    {
        titleText.text = MemoryManager.SharedInstance.Lives != 0 ? "¡GANASTE!" : "VUELVE A INTENTARLO...";
        endScore.text = $"PARES CONSEGUIDOS: {scoreText.text}";
        endLives.text = $"VIDAS RESTANTES: {livesText.text}";
        SwitchEndgamePanel(true);
    }
    public void SwitchEndgamePanel(bool state)
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
