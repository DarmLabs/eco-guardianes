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
    void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        outline = GetComponent<Outline>();
    }

    public void CloseMode()
    {
        if (isClose)
        {
            EnableActionPanel(true);
        }
    }
    public void SearchMode()
    {
        EnableActionPanel(false);
    }
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            isClose = true;
            playerMovement.ClearNavMeshPath();
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
        if (!actionPanel.isOpened)
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
            actionPanel.ClampToWindow(Input.mousePosition, actionPanel.GetComponent<RectTransform>(), actionPanel.gameObject.transform.parent.gameObject.GetComponent<RectTransform>());
            actionPanel.gameObject.SetActive(true);
            actionPanel.SetListeners(transform, closeMode);
            Glow(false);
            actionPanel.isOpened = true;
        }
    }
    public void DisableActionPanel(){
        actionPanel.isOpened = false;
        actionPanel.gameObject.SetActive(false);
    }
}
