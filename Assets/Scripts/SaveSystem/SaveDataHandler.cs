using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    List<ObjectData> objectDatas;
    void Start()
    {
        ReadData();
    }
    void OnApplicationQuit()
    {
        SaveData();
    }
    void SaveData()
    {
        FileHandler.SaveToJSON<List<ObjectData>>(objectDatas, $"ObjectsData_{SceneCache.SharedInstance.currentScene}");
    }
    void ReadData()
    {
        if (File.Exists(Application.persistentDataPath + $"/ObjectsData_{SceneCache.SharedInstance.currentScene}"))
        {
            objectDatas = FileHandler.ReadListFromJSON<ObjectData>($"ObjectsData_{SceneCache.SharedInstance.currentScene}");
        }
    }
}
