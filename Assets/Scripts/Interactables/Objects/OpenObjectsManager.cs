using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObjectsManager : MonoBehaviour
{
    public static OpenObjectsManager SharedInstance;
    InteractableObject[] interactableObjects;
    public InteractableObject[] InteractableObjects => interactableObjects;
    void Awake()
    {
        SharedInstance = this;
        interactableObjects = GetComponentsInChildren<InteractableObject>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && Application.isEditor)
        {
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                interactableObjects[i].InteractWithObject();
            }
        }
    }
}
