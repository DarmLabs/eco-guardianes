using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerCustomatization : MonoBehaviour
{
    [SerializeField] GameObject[] hairStyles;
    [SerializeField] GameObject[] shirtStyles;
    [SerializeField] GameObject[] pantStyles;
    [SerializeField] GameObject[] shoeStyles;
    [SerializeField] SkinnedMeshRenderer[] bodyMeshes;
    [SerializeField] Material[] tones;
    CharacterData characterData;
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/characterPropieties"))
        {
            characterData = FileHandler.ReadFromJSON<CharacterData>("characterPropieties");
            CharacterLoad();
        }
        else
        {
            Debug.LogError("No hay guardado para cargar el personaje, crea uno primero");
        }
    }
    void CharacterLoad()
    {
        hairStyles[characterData.headIndex].SetActive(true);
        shirtStyles[characterData.shirtIndex].SetActive(true);
        pantStyles[characterData.pantsIndex].SetActive(true);
        shoeStyles[characterData.shoesIndex].SetActive(true);
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
