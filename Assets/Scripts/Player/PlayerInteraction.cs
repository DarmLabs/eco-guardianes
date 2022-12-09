using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    //[SerializeField] bool closeMode;
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
        InteractableObjectBase interactableObject = selectedGameObject.GetComponent<InteractableObjectBase>();
        if (interactableObject == null)
        {
            return;
        }
        else
        {
            CallMode(interactableObject);
        }
    }
    void CallMode(InteractableObjectBase interactableObject)
    {
        interactableObject.SearchMode();
        /*if (closeMode)
        {
            interactableObject.CloseMode();
        }
        else
        {
            
        }*/
    }
}
