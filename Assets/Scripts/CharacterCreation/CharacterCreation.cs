using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class CharacterCreation : MonoBehaviour
{
    public static CharacterCreation SharedInstance;
    CharacterData characterData;
    [SerializeField] GameObject[] headPrefabs;
    [SerializeField] GameObject[] shirtPrefabs;
    [SerializeField] GameObject[] pantsPrefabs;
    [SerializeField] GameObject[] shoesPrefabs;
    [SerializeField] GameObject wheelChair;
    [SerializeField] Material[] tones;
    [SerializeField] SkinnedMeshRenderer[] bodyMesh;
    [SerializeField] Material[] partsMaterials;
    [SerializeField] TMP_InputField inputName;

    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/characterPropieties"))
        {
            characterData = FileHandler.ReadFromJSON<CharacterData>("characterPropieties");
            InitialLoad();
        }
        else
        {
            characterData = new CharacterData(0, 0, 0, 0, 0, "", new string[partsMaterials.Length]);
        }
    }
    void InitialLoad()
    {
        inputName.text = characterData.characterName;
        InitialCharacterLoad();
        toneSelector(characterData.tonesIndex);
    }
    void InitialCharacterLoad()
    {
        string[] ids = { "head", "shirt", "pants", "shoes", "tones" };
        for (int i = 0; i < partsMaterials.Length; i++)
        {
            Color colorFromSave;
            ColorUtility.TryParseHtmlString("#" + characterData.hexPartsColor[i], out colorFromSave);
            partsMaterials[i].color = colorFromSave;
        }
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == "tones")
            {
                toneSelector(characterData.tonesIndex);
                break;
            }
            Selector(0, ids[i]);
        }
    }
    public void toneSelector(int value)
    {
        for (int i = 0; i < bodyMesh.Length; i++)
        {
            if (bodyMesh[i].sharedMaterials.Length != 2)
            {
                bodyMesh[i].sharedMaterial = tones[value];
            }
            else
            {
                Material[] mats;
                mats = bodyMesh[i].sharedMaterials;
                mats[1] = tones[value];
                bodyMesh[i].sharedMaterials = mats;
            }
        }
    }
    public void Selector(int value, string id)
    {
        int index = IndexIdentifier(id);
        GameObject[] prefabs = PrefabIdentifier(id);
        index += value;
        if (index > prefabs.Length - 1)
        {
            index = 0;
            AssignResult(0, id);
        }
        else if (index == -1)
        {
            index = prefabs.Length - 1;
            AssignResult(prefabs.Length - 1, id);
        }
        else
        {
            AssignResult(index, id);
        }
        SetAllOff(prefabs);
        prefabs[index].SetActive(true);
    }
    public void AssignResult(int result, string id)
    {
        switch (id)
        {
            case "head":
                characterData.headIndex = result;
                break;
            case "shirt":
                characterData.shirtIndex = result;
                break;
            case "pants":
                characterData.pantsIndex = result;
                break;
            case "shoes":
                characterData.shoesIndex = result;
                break;
        }
    }
    public void InsertName(string characterName)
    {
        Debug.Log(characterName);
        characterData.characterName = characterName;
    }
    void SetAllOff(GameObject[] prefabs)
    {
        foreach (var item in prefabs)
        {
            item.SetActive(false);
        }
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
                return characterData.headIndex;
            case "shirt":
                return characterData.shirtIndex;
            case "pants":
                return characterData.pantsIndex;
            case "shoes":
                return characterData.shoesIndex;
        }
        return 0;
    }
    void RetriveMaterialHex()
    {
        for (int i = 0; i < partsMaterials.Length; i++)
        {
            characterData.hexPartsColor[i] = ColorUtility.ToHtmlStringRGB(partsMaterials[i].color);
        }
    }
    public void CanUseLegs(bool state)
    {
        characterData.isOnWheelChair = state;
        wheelChair.SetActive(state);
        wheelChair.GetComponentInParent<Animator>().SetBool("isOnWheelChair", state);
    }
    public void SaveCharacterData()
    {
        RetriveMaterialHex();
        FileHandler.SaveToJSON(characterData, "characterPropieties");
    }
}
