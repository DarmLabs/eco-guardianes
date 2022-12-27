using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    //[SerializeField] bool closeMode;
    bool canInteract = true;
    void Start()
    {
        ActionPanelManager.SharedInstance.panelOpened.AddListener(OnPanelOpened);
        ActionPanelManager.SharedInstance.panelClosed.AddListener(OnPanelClosed);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canInteract)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                if (raycastHit.transform != null)
                {
                    if (raycastHit.transform.gameObject.tag != "Map")
                    {
                        CurrentClickedGameObject(raycastHit.transform.gameObject);
                    }
                    else
                    {
                        CurrentClickedPosition(raycastHit.point);
                    }
                }
            }
        }
    }
    void CurrentClickedGameObject(GameObject selectedGameObject)
    {
        InteractableObjectBase interactableObject = selectedGameObject.GetComponent<InteractableObjectBase>();
        if (interactableObject == null || !interactableObject.isActiveAndEnabled)
        {
            return;
        }
        else
        {
            interactableObject.SearchMode();
        }
    }
    void CurrentClickedPosition(Vector3 position)
    {
        PointAndClickMovement.SharedInstance.TravelToPoint(position);
    }
    void OnPanelOpened()
    {
        canInteract = false;
    }
    void OnPanelClosed()
    {
        canInteract = true;
    }
}
