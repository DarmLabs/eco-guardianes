using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementsManagerUI : MonoBehaviour
{
    public static AchievementsManagerUI SharedInstance;
    [SerializeField] GameObject logrosPanel;
    [SerializeField] Image logroYellow, logroWhite, logroGray, logroBlue, logroCompost;

    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        CheckLogros();
    }
    public void LogrosPanelSwitcher(bool state)
    {
        logrosPanel.SetActive(state);
    }
    public void CheckLogros()
    {
        AchievementsData data = AchievementsManager.SharedInstance?.AchievementsData;
        if (data != null)
        {
            TryEnableLogro(logroYellow, data.yellowAchievement);
            TryEnableLogro(logroWhite, data.whiteAchievement);
            TryEnableLogro(logroGray, data.grayAchievement);
            TryEnableLogro(logroBlue, data.blueAchievement);
            TryEnableLogro(logroCompost, data.compostAchievement);
        }
    }
    void TryEnableLogro(Image logroImg, bool mustEnable)
    {
        if (mustEnable)
        {
            logroImg.color = Color.white;
            if (logroImg.gameObject.transform.childCount == 2)
            {
                logroImg.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public void EndGame()
    {
        LogrosPanelSwitcher(true);
    }
}
