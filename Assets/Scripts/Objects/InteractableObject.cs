using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public bool isClose; //If player is close to this object
    [SerializeField] PlayerMovement playerMovement;
    Outline outline;
    [SerializeField] ActionPanelManager actionPanel;
    bool beingCollected; //If this item is being collected
    bool canInteract; //If the player can interact with this object
    void Awake()
    {
        canInteract = true;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        outline = GetComponent<Outline>();
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
            if (beingCollected)
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
        if (!actionPanel.isOpened && canInteract)
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
        if (playerMovement.isMoving)
        {
            return;
        }
        if (!actionPanel.isOpened)
        {
            actionPanel.gameObject.SetActive(true);
            actionPanel.SetListeners(transform, gameObject.tag/*, closeMode*/);
            Glow(false);
        }
    }
    public virtual void InteractWithObject()
    {
        gameObject.SetActive(false);
        //Save object
    }
    //Setters
    public void BeingCollected(bool state)
    {
        beingCollected = state;
    }
    public void CanInteract(bool state)
    {
        canInteract = state;
    }
}
