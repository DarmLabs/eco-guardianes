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
        parentScript.OnChildTriggerEnter(target);
    }
    void OnTriggerExit(Collider target)
    {
        parentScript.OnChildTriggerExit(target);
    }
}
