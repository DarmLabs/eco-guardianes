using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractableObject : MonoBehaviour
{
    BoxCollider closeColl;
    bool isClose;
    [SerializeField] PlayerMovement playerMovement;
    Outline outline;
    [SerializeField] ActionPanelManager actionPanel;
    [HideInInspector] public bool beingCollected { get; private set; }
    [HideInInspector] public bool canInteract { get; private set; }
    void Awake()
    {
        canInteract = true;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        outline = GetComponent<Outline>();
    }

    public void CloseMode()
    {
        if (isClose && canInteract)
        {
            EnableActionPanel(true);
        }
    }
    public void SearchMode()
    {
        if (canInteract)
        {
            EnableActionPanel(false);
        }
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {
            isClose = true;
            pm.ClearNavMeshPath();
            if (beingCollected)
            {
                gameObject.SetActive(false);
                //Save object
            }
        }
    }
    void OnTriggerExit(Collider target)
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
    void EnableActionPanel(bool closeMode)
    {
        if (playerMovement.isMoving)
        {
            return;
        }
        if (!actionPanel.isOpened)
        {
            actionPanel.gameObject.SetActive(true);
            actionPanel.SetListeners(transform, closeMode);
            Glow(false);
        }
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
