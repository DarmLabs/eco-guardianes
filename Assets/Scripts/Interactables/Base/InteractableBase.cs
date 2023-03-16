using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableBase : MonoBehaviour
{
    bool isClose; //If player is close to this object
    public bool IsClose => isClose;
    Outline outline;
    public Outline Outline => outline;
    [HideInInspector] public bool BeingTargeted { get; set; } //If this item is being collected or is a trash can the player is heading towards
    bool canInteract; //If the player can interact with this object
    public bool CanInteract
    {
        get => canInteract;
        set => canInteract = value;
    }
    bool overMainButtons;
    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
    }
    public virtual void SearchMode()
    {
        if (canInteract)
        {
            PointAndClickMovement.SharedInstance.ResetDestinationObject();
            PointAndClickMovement.SharedInstance.TravelToTarget(this);
            BeingTargeted = true;
            Glow(false);
        }
    }
    //Triggers
    public void OnChildTriggerEnter(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PointAndClickMovement pm))
        {
            isClose = true;
            if (BeingTargeted)
            {
                pm.DestinationReached();
                StartCoroutine(pm.LookAt(this));
            }
        }
    }
    public void OnChildTriggerExit(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PointAndClickMovement pm))
        {
            isClose = false;
        }
    }
    public virtual void TargetObject()
    {
        ActionPanelManager.SharedInstance.EnableActionPanel(this);
    }
    public virtual void InteractWithObject()
    {

    }
    //Mouse Detection
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        bool mustInteract = !ActionPanelManager.SharedInstance.IsOpened && !TrashPanelManager.SharedInstance.IsOpened &&
        !PauseManager.SharedInstance.IsOpened && canInteract && isActiveAndEnabled && !overMainButtons &&
        !TutorialManager.SharedInstance.IsTutorialRunning;

        if (mustInteract)
        {
            Glow(true);
            MainButtonsManager.SharedInstance.enterAnyMask.AddListener(OnMainMaskEnter);
        }
    }
    void OnMouseExit()
    {
        Glow(false);
    }
    public virtual void Glow(bool state)
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
    public void RemoveInteraction()
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
    public void EnableInteraction()
    {
        canInteract = true;
    }
    public void RemoveListeners()
    {
        ActionPanelManager.SharedInstance.panelOpened.RemoveListener(RemoveInteraction);
        ActionPanelManager.SharedInstance.panelClosed.RemoveListener(EnableInteraction);
    }
}
