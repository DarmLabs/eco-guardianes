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
            enabled = false;
        }
    }
    public override void TargetObject()
    {
        StartCoroutine(Dialogue());
    }
    IEnumerator Dialogue()
    {
        yield return DialogManager.SharedInstance.SetDialog(personName, dialog: dialog);
        if (GetComponent<DisableUntil>() != null)
        {
            DisableUntil disableUntil = GetComponent<DisableUntil>();
            disableUntil.EarnAchievement();
        }
    }
}
