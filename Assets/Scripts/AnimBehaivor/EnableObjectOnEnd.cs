using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnEnd : MonoBehaviour
{
    [SerializeField] GameObject gObject;
    public void OnAnimExit()
    {
        gObject.SetActive(true);
    }
}
