using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : InteractableObjectBase
{
    [SerializeField] InteractableObject insideObject;
    public InteractableObject InsideObject => insideObject;
    [SerializeField] Transform viewPoint;
    public Transform ViewPoint => viewPoint;
    Animator anim;
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        if (insideObject == null)
        {
            this.enabled = false;
            return;
        }
        LookAt = transform.GetChild(transform.childCount - 1);
    }
    public override void InteractWithObject()
    {
        if (gameObject.transform.childCount > 1)
        {
            anim.Play("Open");
        }
    }
    public void Opened()
    {
        ActionPanelManager.SharedInstance.SetSearchIntoPanelData();
        ActionPanelManager.SharedInstance.SearchIntoPanelSwitcher(true);
        ActionPanelManager.SharedInstance.panelOpened.Invoke();
        ActionPanelManager.SharedInstance.SearchIntoButtonsSwitcher(true);
    }
    public void Close()
    {
        if (gameObject.transform.childCount > 1)
        {
            anim.Play("Close");
        }
    }
}
