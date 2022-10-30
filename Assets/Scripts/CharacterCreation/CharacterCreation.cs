using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
class CharacterCreation : MonoBehaviour
{
    [SerializeField] GameObject[] headPrefabs;
    int headIndex = 0;
    [SerializeField] GameObject[] clothesPrefabs;
    int clothesIndex = 0;
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
        clothesIndex = initialIndexes[1];
        tonesIndex = initialIndexes[2];
        headSelector(0);
        clothesSelector(0);
        toneSelector(0);
    }
    [SerializeField]
    void headSelector(int value)
    {
        headPrefabs[headIndex].SetActive(false);
        headIndex += value;
        if (headIndex > headPrefabs.Length - 1)
        {
            headIndex = 0;
        }
        if (headIndex == -1)
        {
            headIndex = headPrefabs.Length - 1;
        }
        headPrefabs[headIndex].SetActive(true);
    }
    [SerializeField]
    void clothesSelector(int value)
    {
        clothesPrefabs[clothesIndex].SetActive(false);
        clothesIndex += value;
        if (clothesIndex > clothesPrefabs.Length - 1)
        {
            clothesIndex = 0;
        }
        if (clothesIndex == -1)
        {
            clothesIndex = clothesPrefabs.Length - 1;
        }
        clothesPrefabs[clothesIndex].SetActive(true);
    }
    [SerializeField]
    void toneSelector(int value)
    {
        tonesIndex += value;
        if (tonesIndex > tones.Length - 1)
        {
            tonesIndex = 0;
        }
        if (tonesIndex == -1)
        {
            tonesIndex = tones.Length - 1;
        }
        playerMesh.material = tones[tonesIndex];
    }
    [SerializeField]
    void CreateListWithIndexes()
    {
        List<int> indexes = new List<int>();
        indexes.AddRange(new List<int>{
            headIndex,
            clothesIndex,
            tonesIndex,
        });
        FileHandler.SaveToJSON(indexes, "playerIndexes");
    }
}
