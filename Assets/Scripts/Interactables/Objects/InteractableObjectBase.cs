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
public class InteractableObjectBase : InteractableBase
{
    public Transform LookAt { get; set; }
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;
    [SerializeField] ObjectType type;
    public ObjectType Type => type;
    [SerializeField] Sprite objSprite;
    public Sprite ObjSprite => objSprite;
    [TextArea][SerializeField] string objectInfo;
    public string ObjectInfo => objectInfo;

    public virtual void Start()
    {
        ActionPanelManager.SharedInstance.panelOpened.AddListener(RemoveInteraction);
        ActionPanelManager.SharedInstance.panelClosed.AddListener(EnableInteraction);
    }
}
