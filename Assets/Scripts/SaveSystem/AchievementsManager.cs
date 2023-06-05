using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager SharedInstance;
    AchievementsData achievementsData;
    public AchievementsData AchievementsData => achievementsData;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        achievementsData = SaveDataHandler.SharedInstance?.LoadAchievementsData();
    }
    public void AchievementEarned(int id)
    {
        switch (id)
        {
            case 1:
                achievementsData.compostAchievement = true;
                break;
            case 2:
                achievementsData.yellowAchievement = true;
                break;
            case 3:
                achievementsData.whiteAchievement = true;
                break;
            case 4:
                achievementsData.grayAchievement = true;
                break;
            case 5:
                achievementsData.blueAchievement = true;
                break;
        }
        SaveDataHandler.SharedInstance?.SaveAchievements(achievementsData);
    }
}
