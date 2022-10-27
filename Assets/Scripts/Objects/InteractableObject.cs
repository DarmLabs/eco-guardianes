using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractableObject : MonoBehaviour
{
    BoxCollider closeColl;
    bool isClose;
    [SerializeField] GameObject player;
    PlayerMovement playerMovement;
    Outline outline;
    [SerializeField] ActionPanelManager actionPanel;
    [HideInInspector] public bool beingCollected;
    [HideInInspector] public bool canInteract = true;
    void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
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
        if(canInteract)
        {
            EnableActionPanel(false);
        }
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            isClose = true;
            playerMovement.ClearNavMeshPath();
            if(beingCollected)
            {
                gameObject.SetActive(false);
                //Save object
            }
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
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
    public void EnableActionPanel(bool closeMode)
    {
        if(playerMovement.isMoving)
        {
            return;
        }
        if (!actionPanel.isOpened)
        {
            actionPanel.gameObject.SetActive(true);
            actionPanel.SetListeners(transform, closeMode);
            Glow(false);
            actionPanel.isOpened = true;
        }
    }
}
