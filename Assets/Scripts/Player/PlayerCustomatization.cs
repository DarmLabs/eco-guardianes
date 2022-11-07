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
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/characterPropieties"))
        {
            List<int> indexes = FileHandler.ReadListFromJSON<int>("characterPropieties");
            hairStyles[indexes[0]].SetActive(true);
            shirtStyles[indexes[1]].SetActive(true);
            pantStyles[indexes[2]].SetActive(true);
            shoeStyles[indexes[3]].SetActive(true);
            for (int i = 0; i < bodyMeshes.Length; i++)
            {
                bodyMeshes[i].material = tones[indexes[4]];
            }
        }
    }
}
