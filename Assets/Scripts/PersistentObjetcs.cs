using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjetcs : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
