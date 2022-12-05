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
    [SerializeField] Button[] travelButtons;
    [SerializeField] GameObject closedObjectsSection;
    [SerializeField] GameObject openObjectsSection;
    [SerializeField] GameObject trashCanSection;
    [HideInInspector] public InteractableObject targetObject { get; private set; }
    [HideInInspector] public bool isOpened { get; private set; }
    public static ActionPanelManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetTravelListeners(InteractableObject target)
    {
        isOpened = true;
        infoPanel.SetActive(false);
        targetObject = target;

        for (int i = 0; i < travelButtons.Length; i++)
        {
            travelButtons[i].onClick.AddListener(delegate { PlayerMovement.SharedInstance.TravelToDestination(target.transform); });
        }

        if (target.IsTrashCan)
        {
            trashCanSection.SetActive(true);
            return;
        }

        if (target.IsClosedObject)
        {
            closedObjectsSection.SetActive(true);
        }
        else
        {
            openObjectsSection.SetActive(true);
        }
    }
    public void View()
    {
        targetObject.GetComponent<InteractableObject>().CanInteract(false);
        TransitionsManager.SharedInstance.ViewAction(targetObject.gameObject);
    }
    public void EnableActionPanel()
    {
        actionPanel.SetActive(true);
    }
    [SerializeField]
    public void EnableInfo()
    {
        infoPanel.SetActive(true);
        Sprite objSprite = targetObject.ObjSprite;
        string info = $"Esto es un texto de ejemplo, se abrio el objeto: {targetObject.name}";
        HintsPanelFiller.SharedInstance.FillInfo(objSprite, info);
    }
    public void DisableActionPanel()
    {
        isOpened = false;
        actionPanel.SetActive(false);
        targetObject.GetComponent<InteractableObject>().CanInteract(true);
        closedObjectsSection.SetActive(false);
        openObjectsSection.SetActive(false);
        trashCanSection.SetActive(false);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
