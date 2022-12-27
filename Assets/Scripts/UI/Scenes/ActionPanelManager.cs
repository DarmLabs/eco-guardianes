using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class ActionPanelManager : MonoBehaviour
{
    [Header("Needed GameObjects & Others")]
    [SerializeField] GameObject actionPanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] Button[] travelButtons;
    [SerializeField] GameObject closedObjectsSection;
    [SerializeField] GameObject openObjectsSection;
    [SerializeField] GameObject trashCanSection;
    [SerializeField] GameObject searhIntoPanel;
    [SerializeField] TextMeshProUGUI searchIntoText;
    [SerializeField] GameObject searchIntoButtons;
    [HideInInspector] public InteractableObjectBase TargetObjectBase { get; private set; }
    InteractableObject targetObject;
    InteractableContainer targetConatiner;
    bool isOpened;
    public bool IsOpened => isOpened;
    public UnityEvent panelOpened;
    public UnityEvent panelClosed;
    public static ActionPanelManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetTravelListeners()
    {
        infoPanel.SetActive(false);

        for (int i = 0; i < travelButtons.Length; i++)
        {
            travelButtons[i].onClick.AddListener(delegate { PointAndClickMovement.SharedInstance.TravelToTrash(TargetObjectBase.transform); });
        }
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
        panelOpened.Invoke();
        TransitionsManager.SharedInstance.ViewAction(TargetObjectBase);
    }
    public void ViewInsideObject()
    {
        InteractableContainer container = TargetObjectBase.GetComponent<InteractableContainer>();
        panelOpened.Invoke();
        TransitionsManager.SharedInstance.ViewAction(container.InsideObject, true);
    }
    public void EnableActionPanel(InteractableObjectBase interactableObjectBase)
    {
        if (PointAndClickMovement.SharedInstance.isMoving)
        {
            PointAndClickMovement.SharedInstance.DestinationReached();
        }
        if (interactableObjectBase == null)
        {
            actionPanel.SetActive(true);
            isOpened = true;
            panelOpened.Invoke();
            return;
        }
        actionPanel.SetActive(true);
        isOpened = true;
        TargetObjectBase = interactableObjectBase;
        CheckObjectType(interactableObjectBase);
        SetTravelListeners();
        TargetObjectBase.BeingTargeted = true;
        panelOpened.Invoke();
        TargetObjectBase.Glow(false);
    }
    public void DisableActionPanel()
    {
        isOpened = false;
        actionPanel.SetActive(false);
        panelClosed.Invoke();
        closedObjectsSection.SetActive(false);
        openObjectsSection.SetActive(false);
        trashCanSection.SetActive(false);
    }
    [SerializeField]
    public void EnableInfo()
    {
        infoPanel.SetActive(true);
        Sprite objSprite = null;
        if (TargetObjectBase.ObjSprite != null)
        {
            objSprite = TargetObjectBase.ObjSprite;
        }
        string info = $"Esto es un texto de ejemplo, se abrio el objeto: {TargetObjectBase.name}";
        HintsPanelFiller.SharedInstance.FillInfo(objSprite, info);
    }
    public void DisableInfoPanel()
    {
        infoPanel.SetActive(false);
    }
    void CheckObjectType(InteractableObjectBase interactableObjectBase)
    {
        switch (interactableObjectBase.Type)
        {
            case ObjectType.Closed:
                targetConatiner = interactableObjectBase.GetComponent<InteractableContainer>();
                break;
            case ObjectType.Open:
                targetObject = interactableObjectBase.GetComponent<InteractableObject>();
                break;
        }
    }
    public void SearchIntoPanelSwitcher(bool state)
    {
        searhIntoPanel.SetActive(state);
        isOpened = state;
    }
    public void SearchIntoButtonsSwitcher(bool state)
    {
        searchIntoButtons.SetActive(state);
    }
    public void SetSearchIntoPanelData()
    {
        InteractableContainer container = TargetObjectBase.GetComponent<InteractableContainer>();
        targetObject = container.InsideObject;
        searchIntoText.text = $"Hay {targetObject.ObjectPhrase} dentro de la {TargetObjectBase.name}";
    }
    public void SearchIntoLeaveButton()
    {
        targetConatiner.BeingTargeted = false;
        SearchIntoButtonsSwitcher(false);
        SearchIntoPanelSwitcher(false);
        targetConatiner.Close();
        panelClosed.Invoke();
    }
    public void TakeInside() //Used on take button inside search into panel
    {
        searchIntoText.text = $"Juntaste {targetObject.ObjectPhrase}";
        targetObject.InteractWithObject();
        StartCoroutine(WaitForTakenMessage(1.5f));
    }
    public IEnumerator WaitForTakenMessage(float secs)
    {
        yield return new WaitForSecondsRealtime(secs);
        InteractableContainer container = TargetObjectBase.GetComponent<InteractableContainer>();
        container.Close();
        TargetObjectBase.RemoveListeners();
        TargetObjectBase = null;
        SearchIntoPanelSwitcher(false);
        panelClosed.Invoke();
    }
}
