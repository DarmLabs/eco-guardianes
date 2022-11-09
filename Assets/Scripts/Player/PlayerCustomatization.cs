using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
public class PlayerCustomatization : MonoBehaviour
{
    [SerializeField] GameObject[] hairStyles;
    [SerializeField] GameObject[] shirtStyles;
    [SerializeField] GameObject[] pantStyles;
    [SerializeField] GameObject[] shoeStyles;
    [SerializeField] GameObject wheelChair;
    [SerializeField] SkinnedMeshRenderer[] bodyMeshes;
    [SerializeField] Material[] tones;
    NavMeshAgent navMeshAgent;
    CharacterData characterData;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
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
        if (characterData.isOnWheelChair)
        {
            wheelChair.SetActive(true);
            GetComponent<Animator>().SetBool("isOnWheelChair", true);
            navMeshAgent.baseOffset = 0.35f;
        }
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
