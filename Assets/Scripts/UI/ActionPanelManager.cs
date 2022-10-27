using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionPanelManager : MonoBehaviour
{
    [HideInInspector] public GameObject targetObject;
    public Button[] actionButtons;
    PlayerMovement playerMovement;
    [HideInInspector] public bool isOpened;
    [SerializeField] GameObject infoPanel;
    TransitionsManager transitionsManager;
    void Awake()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        transitionsManager = GameObject.FindObjectOfType<TransitionsManager>();
    }
    public void SetListeners(Transform target, bool mode)
    {
        infoPanel.SetActive(false);
        targetObject = target.gameObject;
        foreach (var actionButton in actionButtons)
        {
            actionButton.onClick.RemoveAllListeners();
            if (!mode && actionButton.name == "Take")
            {
                actionButton.onClick.AddListener(delegate { playerMovement.TravelToDestination(target); });
            }
            actionButton.onClick.AddListener(DisableActionPanel);
        }
    }
    public void View()
    {
        targetObject.GetComponent<InteractableObject>().canInteract = false;
        transitionsManager.ViewAction(targetObject);
    }
    public void Take(){
        targetObject.GetComponent<InteractableObject>().canInteract = false;
        targetObject.GetComponent<InteractableObject>().beingCollected = true;
    }
    public void EnableInfo()
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Esto es un texto de ejemplo, se abrio el objeto: " + targetObject.name;
    }
    public void DisableActionPanel(){
        isOpened = false;
        gameObject.SetActive(false);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
