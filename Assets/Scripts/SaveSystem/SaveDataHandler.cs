using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    string dataPath;
    SaveData saveData;
    void Awake()
    {
        dataPath = $"{Application.persistentDataPath}/";
        SharedInstance = this;
        saveData = LoadSaveData();
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

    void SaveSaveData()
    {
        FileHandler.SaveToJSON<SaveData>(saveData, "saveData");
    }

    SaveData LoadSaveData()
    {
        if (File.Exists($"{dataPath}saveData"))
        {
            return FileHandler.ReadFromJSON<SaveData>("saveData");
        }
        return new SaveData(
        new CharacterData(0, 0, 0, 0, 0, "", new string[4]),
        new MainMenuData(false),
        new StarsData(0, 0, 0),
        new TutorialData(false)
        );
    }
    void OnApplicationQuit()
    {
        SaveSaveData();
    }
}
