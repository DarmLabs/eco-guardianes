using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerCustomatization : MonoBehaviour
{
    [SerializeField] GameObject headContainer;
    [SerializeField] GameObject clothesContainer;
    [SerializeField] MeshRenderer playerMesh;
    [SerializeField] Material[] tones;
    void Start()
    {
        if(File.Exists(Application.persistentDataPath+"/playerIndexes")){
            List<int> indexes = FileHandler.ReadListFromJSON<int>("playerIndexes");
            headContainer.transform.GetChild(indexes[0]).gameObject.SetActive(true);
            clothesContainer.transform.GetChild(indexes[1]).gameObject.SetActive(true);
            playerMesh.material = tones[indexes[2]];
        }
    }
}
