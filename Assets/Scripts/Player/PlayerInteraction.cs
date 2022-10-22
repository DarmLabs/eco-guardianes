using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] bool closeMode;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }
    void CurrentClickedGameObject(GameObject selectedGameObject)
    {
        InteractableObject interactableObject = selectedGameObject.GetComponent<InteractableObject>();
        if (interactableObject == null)
        {
            return;
        }
        else
        {
            CallMode(interactableObject);
        }
    }
    void CallMode(InteractableObject interactableObject)
    {
        if (closeMode)
        {
            interactableObject.CloseMode();
        }
        else
        {
            interactableObject.SearchMode();
        }
    }
}
