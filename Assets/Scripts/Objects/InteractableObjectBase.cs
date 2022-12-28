using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrashCategory
{
    Rec,
    Trash,
    Organic,
    None
}
public enum ObjectType
{
    Closed,
    Open,
    TrashCan
}
public class InteractableObjectBase : MonoBehaviour
{
    bool isClose; //If player is close to this object
    public bool IsClose => isClose;
    Outline outline;
    [HideInInspector] public bool BeingTargeted { get; set; } //If this item is being collected or is a trash can the player is heading towards
    bool canInteract; //If the player can interact with this object
    public bool CanInteract
    {
        get => canInteract;
        set => canInteract = value;
    }
    bool overMainButtons;
    public Transform LookAt { get; set; }
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;
    [SerializeField] ObjectType type;
    public ObjectType Type => type;
    [SerializeField] Sprite objSprite;
    public Sprite ObjSprite => objSprite;
    [TextArea][SerializeField] string objectInfo;
    public string ObjectInfo => objectInfo;
    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
    }
    public virtual void Start()
    {
        ActionPanelManager.SharedInstance.panelOpened.AddListener(RemoveInteraction);
        ActionPanelManager.SharedInstance.panelClosed.AddListener(EnableInteraction);
    }
    public void SearchMode()
    {
        if (canInteract)
        {
            ActionPanelManager.SharedInstance.EnableActionPanel(this);
        }
    }
    public virtual void InteractWithObject()
    {
        gameObject.SetActive(false);
    }
    //Triggers
    public void OnChildTriggerEnter(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PointAndClickMovement pm) && BeingTargeted)
        {
            isClose = true;
            pm.DestinationReached();
            InteractWithObject();
            BeingTargeted = false;
        }
    }
    public void OnChildTriggerExit(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PointAndClickMovement pm))
        {
            isClose = false;
        }
    }

    //Mouse Detection
    void OnMouseEnter()
    {
        if (!ActionPanelManager.SharedInstance.IsOpened && !TrashPanelManager.SharedInstance.IsOpened && !PauseManager.SharedInstance.IsOpened && canInteract && isActiveAndEnabled && !overMainButtons)
        {
            Glow(true);
            MainButtonsManager.SharedInstance.enterAnyMask.AddListener(OnMainMaskEnter);
        }
    }
    void OnMouseExit()
    {
        Glow(false);
    }
    public void Glow(bool state)
    {
        outline.enabled = state;
    }
    void OnMainMaskEnter()
    {
        MainButtonsManager.SharedInstance.enterAnyMask.RemoveListener(OnMainMaskEnter);
        MainButtonsManager.SharedInstance.leaveAnyMask.AddListener(OnMainMaskExit);
        MainButtonsManager.SharedInstance.onMainButtonClicked.AddListener(OnMainButtonClicked);
        overMainButtons = true;
        Glow(false);
    }
    void OnMainMaskExit()
    {
        overMainButtons = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == gameObject.name && !TrashPanelManager.SharedInstance.IsOpened && !PauseManager.SharedInstance.IsOpened)
            {
                Glow(true);
                MainButtonsManager.SharedInstance.enterAnyMask.AddListener(OnMainMaskEnter);
            }
        }
        MainButtonsManager.SharedInstance.leaveAnyMask.RemoveListener(OnMainMaskExit);
        MainButtonsManager.SharedInstance.onMainButtonClicked.RemoveListener(OnMainMaskExit);
    }
    void OnMainButtonClicked()
    {
        overMainButtons = false;
        MainButtonsManager.SharedInstance.leaveAnyMask.RemoveListener(OnMainMaskExit);
        MainButtonsManager.SharedInstance.onMainButtonClicked.RemoveListener(OnMainButtonClicked);
    }
    void RemoveInteraction()
    {
        canInteract = false;
        if (outline == null)
        {
            return;
        }
        if (outline.enabled)
        {
            Glow(false);
        }
    }
    void EnableInteraction()
    {
        canInteract = true;
    }
    public void RemoveListeners()
    {
        ActionPanelManager.SharedInstance.panelOpened.RemoveListener(RemoveInteraction);
        ActionPanelManager.SharedInstance.panelClosed.RemoveListener(EnableInteraction);
    }
}
