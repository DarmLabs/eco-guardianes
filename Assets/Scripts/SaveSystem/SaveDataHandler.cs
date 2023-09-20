using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    string dataPath;
    public string DataPath => dataPath;
    SaveData saveData;
    int saveDataId;
    void Awake()
    {
        dataPath = $"{Application.persistentDataPath}/";
        SharedInstance = this;
    }
    #region CharacterCreation
    public void SaveCharacterData(CharacterData characterData)
    {
        saveData.characterData = characterData;
    }
    public CharacterData LoadCharacterData()
    {
        return saveData.characterData;
    }
    #endregion
    #region MainMenuScene
    public void SaveMainMenuFirstTime(MainMenuData mainMenuData)
    {
        saveData.mainMenuData = mainMenuData;
    }
    public MainMenuData LoadMainMenuFirstTime()
    {
        return saveData.mainMenuData;
    }
    #endregion
    #region MapScene
    public void SaveStarsData(StarsData starsData)
    {
        saveData.starsData = starsData;
    }
    public StarsData LoadStarsData()
    {
        return saveData.starsData;
    }
    #endregion
    #region Tutorial
    public void SaveTutoFirstTime(TutorialData tutorialData)
    {
        saveData.tutorialData = tutorialData;
    }
    public TutorialData LoadTutoFirstTime()
    {
        return saveData.tutorialData;
    }
    #endregion
    #region  Achievements
    public void SaveAchievements(AchievementsData achievementsData)
    {
        saveData.achievementsData = achievementsData;
    }
    public AchievementsData LoadAchievementsData()
    {
        return saveData.achievementsData;
    }
    #endregion
    #region Central
    public void SaveCentral(bool state)
    {
        saveData.hasCompletedCental = state;
    }
    public bool LoadCentalData()
    {
        return saveData.hasCompletedCental;
    }
    #endregion

    void SaveSaveData()
    {
        if (saveDataId < 1 || saveDataId > 3)
        {
            return;
        }
        FileHandler.SaveToJSON(saveData, $"saveData{saveDataId}");
    }

    public void SetSaveDataId(int id)
    {
        saveDataId = id;
        saveData = LoadSaveData();
    }

    SaveData LoadSaveData()
    {
        if (File.Exists($"{dataPath}saveData{saveDataId}"))
        {
            return FileHandler.ReadFromJSON<SaveData>($"saveData{saveDataId}");
        }
        return new SaveData(
        new CharacterData(0, 0, 0, 0, 0, "", new string[4]),
        new MainMenuData(false),
        new StarsData(0, 0, 0, false),
        new TutorialData(false),
        new AchievementsData(false, false, false, false, false),
        false
        );
    }
    void OnApplicationQuit()
    {
        SaveSaveData();
    }
}
