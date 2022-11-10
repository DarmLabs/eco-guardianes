using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public bool isClose { get; private set; } //If player is close to this object
    Outline outline;
    bool beingTargeted; //If this item is being collected or is a trash can the player is heading towards
    bool canInteract; //If the player can interact with this object
    public Transform optionalLookAt { get; set; }
    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
    }
    public void CloseMode()
    {
        if (/*isClose &&*/ canInteract)
        {
            EnableActionPanel(/*true*/);
        }
    }
    public void SearchMode()
    {
        if (canInteract)
        {
            EnableActionPanel(/*false*/);
        }
    }
    public void OnChildTriggerEnter(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {

            isClose = true;
            pm.ClearNavMeshPath();
            if (beingTargeted)
            {
                InteractWithObject();
            }
        }
    }
    public void OnChildTriggerExit(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {
            isClose = false;
        }
    }
    void OnMouseEnter()
    {
        if (!ActionPanelManager.SharedInstance.isOpened && canInteract)
        {
            Glow(true);
        }
    }
    void OnMouseExit()
    {
        Glow(false);
    }
    void Glow(bool state)
    {
        outline.enabled = state;
    }
    void EnableActionPanel(/*bool closeMode*/)
    {
        if (PlayerMovement.SharedInstance.isMoving)
        {
            return;
        }
        if (!ActionPanelManager.SharedInstance.isOpened)
        {
            ActionPanelManager.SharedInstance.gameObject.SetActive(true);
            ActionPanelManager.SharedInstance.SetListeners(transform, gameObject.tag/*, closeMode*/);
            Glow(false);
        }
    }
    public virtual void InteractWithObject()
    {
        gameObject.SetActive(false);
        //Save object
    }
    //Setters
    public void BeingTargeted(bool state)
    {
        beingTargeted = state;
    }
    public void CanInteract(bool state)
    {
        canInteract = state;
    }
}
