using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionPanelManager : MonoBehaviour
{
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Button[] actionButtons;
    [HideInInspector] public GameObject targetObject { get; private set; }
    [HideInInspector] public bool isOpened { get; private set; }
    [SerializeField] GameObject activeActionButton;
    public static ActionPanelManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public string SearchButtonName(string type)
    {
        string searchedName = "";
        if (type == "InteractableObject")
        {
            searchedName = "Take";
        }
        if (type == "TrashCan")
        {
            searchedName = "Throw";
        }
        return searchedName;
    }
    public void SetListeners(Transform target, string type/*, bool mode*/)
    {
        isOpened = true;
        infoPanel.SetActive(false);
        targetObject = target.gameObject;
        string targetName = SearchButtonName(type);
        foreach (var actionButton in actionButtons)
        {
            if (actionButton.name == targetName)
            {
                actionButton.gameObject.SetActive(true);
                activeActionButton = actionButton.gameObject;
            }
            actionButton.onClick.RemoveAllListeners();
            if (/*!mode && */(actionButton.name == "Take" || actionButton.name == "Throw") && actionButton.gameObject.activeSelf)
            {
                actionButton.onClick.AddListener(delegate { PlayerMovement.SharedInstance.TravelToDestination(target); });
            }
            actionButton.onClick.AddListener(DisableActionPanel);
        }
    }
    [SerializeField]
    void View()
    {
        targetObject.GetComponent<InteractableObject>().CanInteract(false);
        TransitionsManager.SharedInstance.ViewAction(targetObject);
    }
    public void TakeObjOrHeadTrashCan()
    {
        targetObject.GetComponent<InteractableObject>().CanInteract(false);
        targetObject.GetComponent<InteractableObject>().BeingTargeted(true);
    }
    public void EnableActionPanel()
    {
        actionPanel.SetActive(true);
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
        activeActionButton.SetActive(false);
        actionPanel.SetActive(false);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
