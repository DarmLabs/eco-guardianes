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
    InteractableContainer targetContainer;
    bool isOpened;
    public bool IsOpened => isOpened;
    public UnityEvent panelOpened;
    public UnityEvent panelClosed;
    public static ActionPanelManager SharedInstance;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetTravelListeners(InteractableBase interactable)
    {
        infoPanel.SetActive(false);

        for (int i = 0; i < travelButtons.Length; i++)
        {
            travelButtons[i].onClick.RemoveAllListeners();
            travelButtons[i].onClick.AddListener(delegate { PointAndClickMovement.SharedInstance.TravelToTrash(interactable.transform); });
        }
        if (TargetObjectBase.Type == ObjectType.TrashCan)
        {
            trashCanSection.SetActive(true);
            return;
        }
        if (TargetObjectBase.Type == ObjectType.Closed)
        {
            closedObjectsSection.SetActive(true);
            return;
        }
        if (TargetObjectBase.Type == ObjectType.Open)
        {
            openObjectsSection.SetActive(true);
            return;
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
    public void EnableActionPanel(InteractableBase interactableBase)
    {
        TargetObjectBase = null;
        targetContainer = null;
        targetObject = null;

        if (PointAndClickMovement.SharedInstance.isMoving)
        {
            PointAndClickMovement.SharedInstance.DestinationReached();
        }
        if (interactableBase == null)
        {
            actionPanel.SetActive(true);
            isOpened = true;
            panelOpened.Invoke();
            return;
        }
        actionPanel.SetActive(true);
        isOpened = true;

        if (interactableBase.gameObject.TryGetComponent(out InteractableObjectBase ob))
        {
            TargetObjectBase = ob;
        }
        panelOpened.Invoke();
        interactableBase.BeingTargeted = true;
        interactableBase.Glow(false);
        SetTravelListeners(interactableBase);

        if (TargetObjectBase != null)
        {
            CheckObjectType(TargetObjectBase);
        }
    }
    public void DisableActionPanel()
    {
        actionPanel.SetActive(false);
        closedObjectsSection.SetActive(false);
        openObjectsSection.SetActive(false);
        trashCanSection.SetActive(false);

        isOpened = false;

        panelClosed.Invoke();
    }
    public void EnableInfo()
    {
        infoPanel.SetActive(true);
        Sprite objSprite = null;
        string info = "";
        objSprite = TargetObjectBase.ObjSprite;
        if (TargetObjectBase.ObjectInfo == "")
        {
            info = $"Esto es un texto de ejemplo, se abrio el objeto: {TargetObjectBase.name}";
        }
        else
        {
            info = TargetObjectBase.ObjectInfo;
        }
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
                targetContainer = interactableObjectBase.GetComponent<InteractableContainer>();
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
        targetObject = targetContainer.InsideObject;
        searchIntoText.text = $"Hay {targetObject.ObjectPhrase} dentro de la {TargetObjectBase.name}";
    }
    public void SearchIntoLeaveButton()
    {
        targetContainer.BeingTargeted = false;
        SearchIntoButtonsSwitcher(false);
        SearchIntoPanelSwitcher(false);
        targetContainer.Close();
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
        targetContainer.Close();
        TargetObjectBase.RemoveListeners();
        SearchIntoPanelSwitcher(false);
        panelClosed.Invoke();
    }
}
