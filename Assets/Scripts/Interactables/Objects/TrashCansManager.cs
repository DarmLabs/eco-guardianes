using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCansManager : MonoBehaviour
{
    public static TrashCansManager SharedInstance;
    InteractableCanTrash[] trashCans;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        trashCans = GetComponentsInChildren<InteractableCanTrash>();
    }
    public void GlowAllCans(bool state)
    {
        foreach (var can in trashCans)
        {
            can.Outline.enabled = state;
        }
    }
}
