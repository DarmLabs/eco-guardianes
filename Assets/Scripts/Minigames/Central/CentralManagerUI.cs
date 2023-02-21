using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CentralManagerUI : BaseMinigameManagerUI
{
    public static CentralManagerUI SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public override void SwitchPausePanel(bool state)
    {
        base.SwitchPausePanel(state);
        Time.timeScale = state == true ? 0 : 1;
    }
    public override void SwitchEndgamePanel(bool state)
    {
        base.SwitchEndgamePanel(state);
        Time.timeScale = 0;
    }
    public override void UpdateScore()
    {
        scoreText.text = CentralManager.SharedInstance.Score.ToString();
    }
    public override void UpdateLives()
    {
        livesText.text = CentralManager.SharedInstance.Lives.ToString();
    }
    public override void ActivateEndPanel()
    {
        titleText.text = CentralManager.SharedInstance.Lives != 0 ? "¡GANASTE!" : "VUELVE A INTENTARLO...";
        endScore.text = $"ACIERTOS: {scoreText.text}";
        endLives.text = $"VIDAS RESTANTES: {livesText.text}";
        SwitchEndgamePanel(true);
    }
}
