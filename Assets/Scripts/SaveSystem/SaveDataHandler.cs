using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    string dataPath;
    void Awake()
    {
        dataPath = $"{Application.persistentDataPath}/";
        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    #region MainMenuScene
    public void SaveMainMenuFirstTime()
    {
        MainMenuData mainMenuData = new MainMenuData(true);
        FileHandler.SaveToJSON<MainMenuData>(mainMenuData, ConstManager.mainMenuFlags);
    }
    public bool LoadMainMenuFirstTime()
    {
        if (File.Exists($"{dataPath}{ConstManager.mainMenuFlags}"))
        {
            MainMenuData mainMenuData = FileHandler.ReadFromJSON<MainMenuData>(ConstManager.mainMenuFlags);
            return mainMenuData.isCharacterCreated;
        }
        return false;
    }
    #endregion
    #region MapScene
    public void SaveStarsData(StarsData starsData, string filename)
    {
        FileHandler.SaveToJSON<StarsData>(starsData, $"{filename}{SceneCache.SharedInstance.currentScene}");
    }
    public StarsData LoadStarsData()
    {
        if (File.Exists($"{dataPath}{ConstManager.starsData}{SceneCache.SharedInstance.currentScene}"))
        {
            return FileHandler.ReadFromJSON<StarsData>($"{ConstManager.starsData}{SceneCache.SharedInstance.currentScene}");
        }
        return new StarsData(0);
    }
    public int LoadGlobalStars()
    {
        string[] scenes = { ConstManager.cocinaSceneName, ConstManager.escuelaSceneName, ConstManager.plazaSceneName };
        int globalStars = 0;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (File.Exists($"{dataPath}starsData_{scenes[i]}"))
            {
                StarsData starsData = FileHandler.ReadFromJSON<StarsData>($"starsData_{scenes[i]}");
                globalStars += starsData.starsCount;
            }
        }
        return globalStars;
    }
    #endregion
    #region Tutorial
    public void SaveTutoFirstTime()
    {
        TutorialData tutoData = new TutorialData(true);
        FileHandler.SaveToJSON<TutorialData>(tutoData, ConstManager.tutoFlags);
    }
    public TutorialData LoadTutoFirstTime()
    {
        if (File.Exists($"{dataPath}{ConstManager.tutoFlags}"))
        {
            TutorialData tutorialData = FileHandler.ReadFromJSON<TutorialData>(ConstManager.tutoFlags);
            return tutorialData;
        }
        return new TutorialData(false);
    }
    #endregion
}
