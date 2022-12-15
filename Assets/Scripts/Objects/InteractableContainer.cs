using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : InteractableObjectBase
{
    [SerializeField] InteractableObject insideObject;
    [SerializeField] Transform viewPoint;
    public Transform ViewPoint => viewPoint;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (insideObject == null)
        {
            this.enabled = false;
            return;
        }
        LookAt = insideObject.transform;
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
        MainButtonsManager.SharedInstance.SetTimeScale(0);
        ActionPanelManager.SharedInstance.SetSearchIntoPanelData();
        ActionPanelManager.SharedInstance.SearchIntoPanelSwitcher(true);
        ActionPanelManager.SharedInstance.SearchIntoButtonsSwitcher(true);
    }
}
