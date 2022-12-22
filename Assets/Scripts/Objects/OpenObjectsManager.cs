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
}
