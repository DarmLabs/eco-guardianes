using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    void Awake()
    {
        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SaveStarsData(StarsData starsData, string filename)
    {
        FileHandler.SaveToJSON<StarsData>(starsData, $"{filename}_{SceneCache.SharedInstance.currentScene}");
    }
    public StarsData LoadStarsData()
    {
        if (File.Exists($"{Application.persistentDataPath}/starsData_{SceneCache.SharedInstance.currentScene}"))
        {
            FileHandler.ReadFromJSON<StarsData>($"starsData_{SceneCache.SharedInstance.currentScene}");
        }
        return new StarsData(0);
    }
    public int LoadGlobalStars()
    {
        string[] scenes = { "Casa", "Escuela", "Plaza" };
        int globalStars = 0;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (File.Exists($"{Application.persistentDataPath}/starsData_{scenes[i]}"))
            {
                StarsData starsData = FileHandler.ReadFromJSON<StarsData>($"starsData_{scenes[i]}");
                globalStars += starsData.starsCount;
            }
        }
        return globalStars;
    }
}
