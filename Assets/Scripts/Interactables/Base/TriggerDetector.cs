using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    InteractableBase parentScript;
    void Start()
    {
        parentScript = transform.GetComponentInParent<InteractableBase>();
    }
    void OnTriggerEnter(Collider target)
    {
        if (parentScript != null)
        {
            parentScript.OnChildTriggerEnter(target);
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (parentScript != null)
        {
            parentScript.OnChildTriggerExit(target);
        }
    }
}
