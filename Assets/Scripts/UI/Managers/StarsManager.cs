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
    [SerializeField] GameObject starExplosionPrefab;
    int starsAchived;
    [SerializeField] GameObject[] buttonsForTuto;
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
    public void TriggerForTutorial()
    {
        starsPanel.SetActive(true);
        foreach (var button in buttonsForTuto)
        {
            button.SetActive(false);
        }
        CheckCondition(6, true);
    }
    public void DisableForTutorial()
    {
        starsPanel.SetActive(false);
        foreach (var button in buttonsForTuto)
        {
            button.SetActive(true);
        }
    }
    void CheckCondition(int succeses, bool isSimulation = false)
    {
        maxObjects = OpenObjectsManager.SharedInstance.InteractableObjects.Length;
        if (succeses >= maxObjects - 6)
        {
            SetStars(succeses);
            if (!isSimulation)
            {
                SaveStars();
            }
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
        starsAchived = placedIndex;
        StartCoroutine(AnimateStars(placedIndex));
    }
    IEnumerator AnimateStars(int index)
    {
        for (int i = 0; i < index; i++)
        {
            stars[i].GetComponent<Animator>().Play("StarSet");
            Instantiate(starExplosionPrefab, stars[i].transform);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    public void ResetScene()
    {
        ScenesChanger.SharedInstance?.ReloadScene();
    }
    public void GoToMap(string scene)
    {
        ScenesChanger.SharedInstance?.SceneChange(scene);
    }
    void SaveStars()
    {
        if (SaveDataHandler.SharedInstance == null)
        {
            return;
        }
        StarsData starsData = SaveDataHandler.SharedInstance.LoadStarsData();
        if (starsData.starsCount < starsAchived)
        {
            starsData.starsCount = starsAchived;
            SaveDataHandler.SharedInstance.SaveStarsData(starsData, ConstManager.starsData);
        }
    }
}
