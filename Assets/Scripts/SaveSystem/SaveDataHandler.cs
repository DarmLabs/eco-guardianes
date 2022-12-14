using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    //int saveSlot; // Implementar
    [SerializeField] InteractableObject[] interactableObjects;
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
        if (interactableObjects.Length != 6)
        {
            return;
        }
        if (File.Exists($"ObjectsData_{SceneCache.SharedInstance.currentScene}"))
        {
            objectDatas = FileHandler.ReadListFromJSON<ObjectData>($"ObjectsData_{SceneCache.SharedInstance.currentScene}");
        }
        else
        {
            objectDatas = new List<ObjectData>(6);
        }
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            interactableObjects[i].ObjectData = objectDatas[i];
        }
    }
    void SaveObjectsData()
    {
        if (interactableObjects.Length != 6)
        {
            return;
        }
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            objectDatas[i] = interactableObjects[i].ObjectData;
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
