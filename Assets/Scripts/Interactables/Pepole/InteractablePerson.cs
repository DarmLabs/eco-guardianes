using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePerson : InteractableBase
{
    [SerializeField] string personName;
    [SerializeField] Dialog dialog;
    void Start()
    {
        if (dialog.Lines.Count == 0)
        {
            this.enabled = false;
        }
    }
    public override void InteractWithObject()
    {
        StartCoroutine(DialogManager.SharedInstance.SetDialog(personName, dialog: dialog));
    }
    public override void SearchMode()
    {
        if (CanInteract)
        {
            PointAndClickMovement.SharedInstance.TravelToTarget(this);
            BeingTargeted = true;
            Glow(false);
        }
    }
}
