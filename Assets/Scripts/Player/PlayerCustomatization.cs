using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
public class PlayerCustomatization : MonoBehaviour
{
    public static PlayerCustomatization SharedInstance;
    string characterName;
    public string CharacterName => characterName;
    [SerializeField] GameObject[] hairStyles;
    [SerializeField] Material hairMat;
    [SerializeField] GameObject[] shirtStyles;
    [SerializeField] Material shirtMat;
    [SerializeField] GameObject[] pantStyles;
    [SerializeField] Material pantMat;
    [SerializeField] GameObject[] shoeStyles;
    [SerializeField] Material shoeMat;
    [SerializeField] GameObject wheelChair;
    [SerializeField] SkinnedMeshRenderer[] bodyMeshes;
    [SerializeField] Material[] tones;
    NavMeshAgent navMeshAgent;
    CharacterData characterData;
    void Awake()
    {
        SharedInstance = this;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        characterData = SaveDataHandler.SharedInstance?.LoadCharacterData();
        if (characterData != null)
        {
            CharacterLoad();
        }
    }
    void CharacterLoad()
    {
        characterName = characterData.characterName;
        hairStyles[characterData.headIndex].SetActive(true);
        shirtStyles[characterData.shirtIndex].SetActive(true);
        pantStyles[characterData.pantsIndex].SetActive(true);
        shoeStyles[characterData.shoesIndex].SetActive(true);
        ColorLoad();
        if (characterData.isOnWheelChair)
        {
            wheelChair.SetActive(true);
            GetComponent<Animator>().SetBool("isOnWheelChair", true);
            navMeshAgent.baseOffset = 0.35f;
        }
    }
    void ColorLoad()
    {
        Color hairColor, shirtColor, pantColor, shoeColor;
        ColorUtility.TryParseHtmlString($"#{characterData.hexPartsColor[0]}", out hairColor);
        ColorUtility.TryParseHtmlString($"#{characterData.hexPartsColor[1]}", out shirtColor);
        ColorUtility.TryParseHtmlString($"#{characterData.hexPartsColor[2]}", out pantColor);
        ColorUtility.TryParseHtmlString($"#{characterData.hexPartsColor[3]}", out shoeColor);

        hairMat.color = hairColor;
        shirtMat.color = shirtColor;
        pantMat.color = pantColor;
        shoeMat.color = shoeColor;
    }
    void ToneAsigner()
    {
        for (int i = 0; i < bodyMeshes.Length; i++)
        {
            if (bodyMeshes[i].sharedMaterials.Length != 2)
            {
                bodyMeshes[i].sharedMaterial = tones[characterData.tonesIndex];
            }
            else
            {
                Material[] mats;
                mats = bodyMeshes[i].sharedMaterials;
                mats[1] = tones[characterData.tonesIndex];
                bodyMeshes[i].sharedMaterials = mats;
            }
        }
    }
}
