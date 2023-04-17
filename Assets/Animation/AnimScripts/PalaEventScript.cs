using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaEventScript : MonoBehaviour
{
    [SerializeField] GameObject forEnableObject;

    public void EnableObject()
    {
        forEnableObject.SetActive(true);
    }
}
