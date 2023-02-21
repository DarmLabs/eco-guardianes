using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SeparatorDetector : MonoBehaviour
{
    [SerializeField] LayerMask objsLayerMask;
    Separator currentSeparator;

    void Update()
    {
        CheckRayToSeparator();
    }
    void CheckRayToSeparator()
    {
        if (CentralManager.SharedInstance.DraggingObject != null)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f, ~objsLayerMask))
            {
                if (raycastHit.transform != null)
                {
                    raycastHit.transform.TryGetComponent(out Separator sp);
                    if (sp != null)
                    {
                        currentSeparator = sp;
                        currentSeparator.DetectorEnter();
                    }
                    else if (currentSeparator != null)
                    {
                        currentSeparator.DetectorExit();
                        currentSeparator = null;
                    }
                }
            }
        }
    }
}
