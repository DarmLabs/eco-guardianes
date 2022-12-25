using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    InteractableObjectBase parentScript;
    void Start()
    {
        parentScript = transform.GetComponentInParent<InteractableObjectBase>();
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
