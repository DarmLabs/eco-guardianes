using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    //int saveSlot; // Implementar
    List<ObjectData> objectDatas;
    void OnApplicationQuit()
    {
        SaveObjectsData();
    }
    public void Save()
    {
        SaveObjectsData();
    }
    public void Load()
    {
        LoadObjectsData();
    }
    void LoadObjectsData()
    {
        if (OpenObjectsManager.SharedInstance.InteractableObjects.Length != 9)
        {
            return;
        }
        if (File.Exists($"ObjectsData_{SceneCache.SharedInstance.currentScene}"))
        {
            objectDatas = FileHandler.ReadListFromJSON<ObjectData>($"ObjectsData_{SceneCache.SharedInstance.currentScene}");
        }
        else
        {
            objectDatas = new List<ObjectData>(9);
        }
        for (int i = 0; i < OpenObjectsManager.SharedInstance.InteractableObjects.Length; i++)
        {
            OpenObjectsManager.SharedInstance.InteractableObjects[i].ObjectData = objectDatas[i];
        }
    }
    void SaveObjectsData()
    {
        if (OpenObjectsManager.SharedInstance.InteractableObjects.Length != 9)
        {
            return;
        }
        for (int i = 0; i < OpenObjectsManager.SharedInstance.InteractableObjects.Length; i++)
        {
            objectDatas[i] = OpenObjectsManager.SharedInstance.InteractableObjects[i].ObjectData;
        }
        FileHandler.SaveToJSON<List<ObjectData>>(objectDatas, $"ObjectsData_{SceneCache.SharedInstance.currentScene}");
    }
    public void SaveStarsData(StarsData starsData, string filename)
    {
        FileHandler.SaveToJSON<StarsData>(starsData, filename);
    }
    public StarsData LoadStarsData()
    {
        if (File.Exists($"{Application.persistentDataPath}/starsData"))
        {
            FileHandler.ReadFromJSON<StarsData>("starsData");
        }
        return new StarsData(0);
    }
}
