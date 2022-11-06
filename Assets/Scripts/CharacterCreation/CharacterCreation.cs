using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
class CharacterCreation : MonoBehaviour
{
    [SerializeField] GameObject[] headPrefabs;
    int headIndex = 0;
    [SerializeField] GameObject[] shirtPrefabs;
    int shirtIndex = 0;
    [SerializeField] GameObject[] pantsPrefabs;
    int pantsIndex = 0;
    [SerializeField] GameObject[] shoesPrefabs;
    int shoesIndex = 0;
    [SerializeField] Material[] tones;
    int tonesIndex = 0;
    [SerializeField] MeshRenderer playerMesh;
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/playerIndexes"))
        {
            InitialLoad();
        }
    }
    void InitialLoad()
    {
        List<int> initialIndexes = new List<int>();
        initialIndexes = FileHandler.ReadListFromJSON<int>("playerIndexes");
        headIndex = initialIndexes[0];
        shirtIndex = initialIndexes[1];
        pantsIndex = initialIndexes[2];
        shoesIndex = initialIndexes[3];
        tonesIndex = initialIndexes[4];
        InitialCharacterLoad();
        toneSelector(0);
    }
    void InitialCharacterLoad()
    {
        string[] ids = { "head", "shirt", "pants", "shoes", "tones" };
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == "tones")
            {
                toneSelector(tonesIndex);
                break;
            }
            Selector(0, ids[i]);
        }
    }
    [SerializeField]
    void toneSelector(int value)
    {
        playerMesh.material = tones[value];
    }
    void Selector(int value, string id)
    {
        int index = IndexIdentifier(id);
        GameObject[] prefabs = PrefabIdentifier(id);
        index += value;
        if (index > prefabs.Length - 1)
        {
            AssignResult(0, id);
        }
        if (index == -1)
        {
            AssignResult(prefabs.Length - 1, id);
        }
        prefabs[index].SetActive(true);
    }
    GameObject[] PrefabIdentifier(string id)
    {
        switch (id)
        {
            case "head":
                return headPrefabs;
            case "shirt":
                return shirtPrefabs;
            case "pants":
                return pantsPrefabs;
            case "shoes":
                return shoesPrefabs;
        }
        return null;
    }
    int IndexIdentifier(string id)
    {
        switch (id)
        {
            case "head":
                return headIndex;
            case "shirt":
                return shirtIndex;
            case "pants":
                return pantsIndex;
            case "shoes":
                return shoesIndex;
        }
        return 0;
    }
    void AssignResult(int result, string id)
    {
        switch (id)
        {
            case "head":
                headIndex = result;
                break;
            case "shirt":
                shirtIndex = result;
                break;
            case "pants":
                pantsIndex = result;
                break;
            case "shoes":
                shoesIndex = result;
                break;
        }
    }
    [SerializeField]
    void CreateListWithIndexes()
    {
        List<int> indexes = new List<int>();
        indexes.AddRange(new List<int>{
            headIndex,
            shirtIndex,
            pantsIndex,
            shoesIndex,
            tonesIndex,
        });
        FileHandler.SaveToJSON(indexes, "playerIndexes");
    }
}
