using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfCinta : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DraggingObject dObject))
        {
            dObject.ResetToPool();
            CentralManager.SharedInstance.BadFeedback();
        }
    }
}
