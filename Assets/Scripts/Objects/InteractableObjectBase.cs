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
    public Transform OptionalLookAt { get; set; }
    public TrashContainer TrashContainer { get; set; }
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;
    [SerializeField] ObjectType type;
    public ObjectType Type => type;

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
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {

            isClose = true;
            pm.ClearNavMeshPath();
            if (BeingTargeted)
            {
                InteractWithObject();
            }
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
        if (!ActionPanelManager.SharedInstance.isOpened && canInteract)
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
