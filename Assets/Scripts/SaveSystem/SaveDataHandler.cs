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
            return;
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
}
