using UnityEngine;
using UnityEngine.Events;
public enum TrashCategory
{
    Rec,
    Trash,
    Organic,
    None
}
public class InteractableObject : MonoBehaviour
{
    bool isClose; //If player is close to this object
    public bool IsClose => isClose;
    bool isClosedObject; //If closed object is true it refers to an object that cant be taken initially and must be opened example: A fridge // If its false its trash that can be colected at any time
    public bool IsClosedObject => isClosedObject;
    Outline outline;
    [HideInInspector] public bool BeingTargeted { get; set; } //If this item is being collected or is a trash can the player is heading towards
    bool canInteract; //If the player can interact with this object
    public Transform OptionalLookAt { get; set; }
    [SerializeField] Sprite objSprite;
    public Sprite ObjSprite => objSprite;
    public TrashContainer TrashContainer { get; set; }
    bool isFound; //true = is in player inventory // Variable being saved
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;

    void Awake()
    {
        outline = GetComponent<Outline>();
        canInteract = true;
        if (gameObject.tag == "ClosedObject")
        {
            isClosedObject = true;
        }
    }
    void Start()
    {
        if (!isFound)
        {
            //TrashContainer.ObjectUnfound();
        }
        else
        {
            TrashContainer.ObjectFound();
            TrashContainer.CorrectCategory = category;
            gameObject.SetActive(false);
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
            BeingTargeted = true;
            Glow(false);
        }
    }
    public virtual void InteractWithObject()
    {
        gameObject.SetActive(false);
        isFound = true;
        //TrashContainer.ObjectFound();
        //TrashContainer.CorrectCategory = category;
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
    void Glow(bool state)
    {
        outline.enabled = state;
    }
    public void CanInteract(bool state)
    {
        canInteract = state;
    }
}
