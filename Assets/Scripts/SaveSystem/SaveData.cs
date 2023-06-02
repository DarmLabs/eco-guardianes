using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#nullable enable
[Serializable]
public class SaveData
{
    public CharacterData characterData;
    public MainMenuData mainMenuData;
    public StarsData starsData;
    public TutorialData tutorialData;
    public AchievementsData achievementsData;
    public SaveData(CharacterData characterData, MainMenuData mainMenuData, StarsData starsData, TutorialData tutorialData, AchievementsData achievementsData)
    {
        this.characterData = characterData;
        this.mainMenuData = mainMenuData;
        this.starsData = starsData;
        this.tutorialData = tutorialData;
        this.achievementsData = achievementsData;
    }
}
