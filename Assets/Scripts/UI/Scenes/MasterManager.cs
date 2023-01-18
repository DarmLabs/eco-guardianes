using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager SharedInstance;
    [SerializeField] GameObject[] unnecesaryObjetctsOnPlantas;
    void Awake()
    {
        SharedInstance = this;
    }
    public void DestroyUnnecesaryObjects()
    {
        for (int i = 0; i < unnecesaryObjetctsOnPlantas.Length; i++)
        {
            Destroy(unnecesaryObjetctsOnPlantas[i]);
            Debug.Log("?");
        }
    }
}
