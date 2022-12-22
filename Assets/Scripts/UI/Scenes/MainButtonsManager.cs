using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainButtonsManager : MonoBehaviour
{
    public static MainButtonsManager SharedInstance;
    [SerializeField] GameObject pauseButton;
    MainButtonsMask pauseButtonMask;
    [SerializeField] GameObject trashButton;
    MainButtonsMask trashButtonMask;
    public UnityEvent enterAnyMask;
    public UnityEvent leaveAnyMask;
    public UnityEvent onMainButtonClicked;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        pauseButtonMask = pauseButton.GetComponent<MainButtonsMask>();
        trashButtonMask = trashButton.GetComponent<MainButtonsMask>();

        pauseButtonMask.enterMask.AddListener(EmitEnterMask);
        trashButtonMask.enterMask.AddListener(EmitEnterMask);
        pauseButtonMask.leaveMask.AddListener(EmitExitMask);
        trashButtonMask.leaveMask.AddListener(EmitExitMask);

        ActionPanelManager.SharedInstance.panelOpened.AddListener(DisableButtons);
        ActionPanelManager.SharedInstance.panelClosed.AddListener(EnableButtons);
    }
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
    public void MainButtonsSwitcher(bool state)
    {
        pauseButton.SetActive(state);
        trashButton.SetActive(state);
    }
    public void EmitEnterMask()
    {
        enterAnyMask.Invoke();
    }
    public void EmitExitMask()
    {
        leaveAnyMask.Invoke();
    }
    void EnableButtons()
    {
        MainButtonsSwitcher(true);
    }
    void DisableButtons()
    {
        MainButtonsSwitcher(false);
    }
}
