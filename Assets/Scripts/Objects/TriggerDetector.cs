using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    InteractableObject parentScript;
    void Start()
    {
        parentScript = transform.GetComponentInParent<InteractableObject>();
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
