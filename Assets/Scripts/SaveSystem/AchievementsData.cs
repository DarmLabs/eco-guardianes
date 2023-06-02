using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AchievementsData
{
    public bool compostAchievement;
    public bool yellowAchievement;
    public bool whiteAchievement;
    public bool grayAchievement;
    public bool blueAchievement;
    public AchievementsData(bool compostAchievement, bool yellowAchievement, bool whiteAchievement, bool grayAchievement, bool blueAchievement)
    {
        this.compostAchievement = compostAchievement;
        this.yellowAchievement = yellowAchievement;
        this.whiteAchievement = whiteAchievement;
        this.grayAchievement = grayAchievement;
        this.blueAchievement = blueAchievement;
    }
}
