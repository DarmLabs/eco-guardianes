using UnityEngine;
using UnityEngine.Events;
public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public bool isClose { get; private set; } //If player is close to this object
    [HideInInspector] public bool isClosedObject { get; private set; } //If closed object is true it refers to an object that cant be taken initially and must be opened example: A fridge // If its false its trash that can be colected at any time
    Outline outline;
    bool beingTargeted; //If this item is being collected or is a trash can the player is heading towards
    bool canInteract; //If the player can interact with this object
    public Transform optionalLookAt { get; set; }
    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
        if (gameObject.tag == "ClosedObject")
        {
            isClosedObject = true;
        }
    }
    public void SearchMode()
    {
        if (canInteract)
        {
            EnableActionPanel();
        }
    }
    void EnableActionPanel()
    {
        if (PlayerMovement.SharedInstance.isMoving)
        {
            return;
        }
        if (!ActionPanelManager.SharedInstance.isOpened)
        {
            ActionPanelManager.SharedInstance.EnableActionPanel();
            ActionPanelManager.SharedInstance.SetTravelListeners(this);
            beingTargeted = true;
            Glow(false);
        }
    }
    public virtual void InteractWithObject()
    {
        gameObject.SetActive(false);
        //Save object
    }
    //Triggers
    public void OnChildTriggerEnter(Collider target)
    {
        if (target.gameObject.TryGetComponent(out PlayerMovement pm))
        {

            isClose = true;
            pm.ClearNavMeshPath();
            if (beingTargeted)
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
    void Glow(bool state)
    {
        outline.enabled = state;
    }
    //Setters
    public void BeingTargeted(bool state)
    {
        beingTargeted = state;
    }
    public void CanInteract(bool state)
    {
        canInteract = state;
    }
}
