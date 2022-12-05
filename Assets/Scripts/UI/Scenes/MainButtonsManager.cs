using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonsManager : MonoBehaviour
{
    public static MainButtonsManager SharedInstance;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject trashButton;
    void Awake()
    {
        SharedInstance = this;
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
}
