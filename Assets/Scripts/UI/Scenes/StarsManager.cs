using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StarsManager : MonoBehaviour
{
    public static StarsManager SharedInstance;
    GameObject starsPanel;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI failsText;
    [SerializeField] TextMeshProUGUI succesesText;
    [SerializeField] GameObject[] stars;
    int maxObjects;
    void Awake()
    {
        SharedInstance = this;
        starsPanel = transform.GetChild(0).gameObject;
    }
    public void TriggerWinCondition()
    {
        starsPanel.SetActive(true);
        CheckCondition(TrashPanelManager.SharedInstance.Succeses);
    }
    void CheckCondition(int succeses)
    {
        maxObjects = OpenObjectsManager.SharedInstance.InteractableObjects.Length;
        if (succeses >= maxObjects - 6)
        {
            SetStars(succeses);
        }
        else
        {
            SetTexts(succeses, false);
            return;
        }
        SetTexts(succeses);
    }
    void SetTexts(int succeses, bool hasWon = true)
    {
        winText.text = hasWon ? "¡LO LOGRASTE!" : "INTENTALO DE NUEVO...";
        failsText.text = $"FALLOS: {maxObjects - succeses}";
        succesesText.text = $"ACIERTOS: {succeses}";
    }
    void SetStars(int succeses)
    {
        int placedIndex = 0;
        if (succeses == maxObjects)
        {
            placedIndex = 3;
        }
        else if (succeses >= maxObjects - 3)
        {
            placedIndex = 2;
        }
        else if (succeses >= maxObjects - 6)
        {
            placedIndex = 1;
        }
        for (int i = 0; i < placedIndex; i++)
        {
            stars[i].GetComponent<Animator>().Play("StarSet");
        }
    }
}