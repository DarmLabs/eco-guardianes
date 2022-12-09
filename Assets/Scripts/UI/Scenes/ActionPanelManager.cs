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
    [HideInInspector] public InteractableObjectBase TargetObjectBase { get; private set; }
    [HideInInspector] public bool isOpened { get; private set; }
    public static ActionPanelManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetTravelListeners()
    {
        isOpened = true;
        infoPanel.SetActive(false);
        for (int i = 0; i < travelButtons.Length; i++)
        {
            travelButtons[i].onClick.AddListener(delegate { PlayerMovement.SharedInstance.TravelToDestination(TargetObjectBase.transform); });
        }
        Debug.Log(TargetObjectBase);
        if (TargetObjectBase.Type == ObjectType.TrashCan)
        {
            trashCanSection.SetActive(true);
            return;
        }

        if (TargetObjectBase.Type == ObjectType.Closed)
        {
            closedObjectsSection.SetActive(true);
        }
        else if (TargetObjectBase.Type == ObjectType.Open)
        {
            openObjectsSection.SetActive(true);
        }
    }
    public void View()
    {
        TargetObjectBase.CanInteract(false);
        TransitionsManager.SharedInstance.ViewAction(TargetObjectBase.gameObject);
    }
    public void EnableActionPanel(InteractableObjectBase interactableObjectBase)
    {
        if (PlayerMovement.SharedInstance.isMoving)
        {
            return;
        }
        if (!isOpened && interactableObjectBase == null)
        {
            actionPanel.SetActive(true);
            return;
        }
        else if (!isOpened && interactableObjectBase != null)
        {
            actionPanel.SetActive(true);
            TargetObjectBase = interactableObjectBase;
            SetTravelListeners();
            TargetObjectBase.BeingTargeted = true;
            TargetObjectBase.Glow(false);
        }

    }
    [SerializeField]
    public void EnableInfo()
    {
        infoPanel.SetActive(true);
        Sprite objSprite = TargetObjectBase.GetComponent<InteractableObject>().ObjSprite;
        string info = $"Esto es un texto de ejemplo, se abrio el objeto: {TargetObjectBase.name}";
        HintsPanelFiller.SharedInstance.FillInfo(objSprite, info);
    }
    public void DisableActionPanel()
    {
        isOpened = false;
        actionPanel.SetActive(false);
        TargetObjectBase.CanInteract(true);
        closedObjectsSection.SetActive(false);
        openObjectsSection.SetActive(false);
        trashCanSection.SetActive(false);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
}
