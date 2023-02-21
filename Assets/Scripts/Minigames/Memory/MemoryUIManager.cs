using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MemoryUIManager : BaseMinigameManagerUI
{
    public static MemoryUIManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public override void UpdateScore()
    {
        scoreText.text = MemoryManager.SharedInstance.Score.ToString();
    }
    public override void UpdateLives()
    {
        livesText.text = MemoryManager.SharedInstance.Lives.ToString();
    }
    public override void ActivateEndPanel()
    {
        titleText.text = MemoryManager.SharedInstance.Lives != 0 ? "¡GANASTE!" : "VUELVE A INTENTARLO...";
        endScore.text = $"PARES CONSEGUIDOS: {scoreText.text}";
        endLives.text = $"VIDAS RESTANTES: {livesText.text}";
        SwitchEndgamePanel(true);
    }
}
