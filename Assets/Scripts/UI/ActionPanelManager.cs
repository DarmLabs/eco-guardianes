using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionPanelManager : MonoBehaviour
{
    [Header("Imported Scripts")]
    [SerializeField] TransitionsManager transitionsManager;
    [SerializeField] PlayerMovement playerMovement;
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject infoPanel;
    [SerializeField] Button[] actionButtons;
    [HideInInspector] public GameObject targetObject { get; private set; }
    [HideInInspector] public bool isOpened { get; private set; }
    public void SetListeners(Transform target, bool mode)
    {
        isOpened = true;
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
    [SerializeField]
    void View()
    {
        targetObject.GetComponent<InteractableObject>().CanInteract(false);
        transitionsManager.ViewAction(targetObject);
    }
    [SerializeField]
    void Take()
    {
        targetObject.GetComponent<InteractableObject>().CanInteract(false);
        targetObject.GetComponent<InteractableObject>().BeingCollected(true);
    }
    [SerializeField]
    void EnableInfo()
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Esto es un texto de ejemplo, se abrio el objeto: " + targetObject.name;
    }
    public void DisableActionPanel()
    {
        isOpened = false;
        gameObject.SetActive(false);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
