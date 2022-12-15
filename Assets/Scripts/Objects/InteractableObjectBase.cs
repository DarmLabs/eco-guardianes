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
    public Transform LookAt { get; set; }
    [SerializeField] TrashContainer trashContainer;
    public TrashContainer TrashContainer
    {
        get => trashContainer;
        set => trashContainer = value;
    }
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;
    [SerializeField] ObjectType type;
    public ObjectType Type => type;
    [SerializeField] Sprite objSprite;
    public Sprite ObjSprite => objSprite;

    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
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
        if (target.gameObject.TryGetComponent(out PlayerMovement pm) && BeingTargeted)
        {
            isClose = true;
            pm.ClearNavMeshPath();
            InteractWithObject();
            BeingTargeted = false;
        }
    }
    public void OnChildTriggerExit(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {
            isClose = false;
        }
    }

    //Mouse Detection
    void OnMouseEnter()
    {
        if (!ActionPanelManager.SharedInstance.IsOpened && canInteract && isActiveAndEnabled)
        {
            Glow(true);
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
    public void CanInteract(bool state)
    {
        canInteract = state;
    }
}
