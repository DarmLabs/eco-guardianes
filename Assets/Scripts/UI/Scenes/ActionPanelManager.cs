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
    [SerializeField] GameObject searhIntoPanel;
    [SerializeField] TextMeshProUGUI searchIntoText;
    [SerializeField] GameObject searchIntoButtons;
    [HideInInspector] public InteractableObjectBase TargetObjectBase { get; private set; }
    InteractableObject targetObject;
    bool isOpened;
    public bool IsOpened => isOpened;
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
            travelButtons[i].onClick.AddListener(delegate { PlayerMovement.SharedInstance.TravelToDestination(TargetObjectBase.transform); });
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
        TargetObjectBase.CanInteract(false);
        TransitionsManager.SharedInstance.ViewAction(TargetObjectBase);
    }
    public void EnableActionPanel(InteractableObjectBase interactableObjectBase)
    {
        if (PlayerMovement.SharedInstance.isMoving)
        {
            return;
        }
        if (interactableObjectBase == null)
        {
            actionPanel.SetActive(true);
            isOpened = true;
            return;
        }
        else if (interactableObjectBase != null)
        {
            actionPanel.SetActive(true);
            isOpened = true;
            TargetObjectBase = interactableObjectBase;
            SetTravelListeners();
            TargetObjectBase.BeingTargeted = true;
            TargetObjectBase.Glow(false);
        }
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
    public void SearchIntoPanelSwitcher(bool state)
    {
        searhIntoPanel.SetActive(state);
    }
    public void SearchIntoButtonsSwitcher(bool state)
    {
        searchIntoButtons.SetActive(state);
    }
    public void SetSearchIntoPanelData()
    {
        targetObject = TargetObjectBase.GetComponent<InteractableObject>();
        searchIntoText.text = $"Hay {targetObject.ObjectPhrase} dentro de la {TargetObjectBase.name}";
    }
    public void TakeInside() //Used on take button inside search into panel
    {
        searchIntoText.text = $"Juntaste {targetObject.ObjectPhrase}";
        targetObject.InteractWithObject();
        StartCoroutine(WaitForTakenMessage(1.5f));
    }
    public IEnumerator WaitForTakenMessage(float secs)
    {
        yield return new WaitForSeconds(secs);
        SearchIntoPanelSwitcher(false);
        MainButtonsManager.SharedInstance.SetTimeScale(1);
    }
}
