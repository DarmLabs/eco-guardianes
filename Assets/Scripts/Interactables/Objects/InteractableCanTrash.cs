using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanTrash : InteractableObjectBase
{
    Animator anim;
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    public override void InteractWithObject()
    {
        if (anim != null)
        {
            anim.Play("Open");
        }
        else
        {
            TrashPanelDisplay();
        }
    }
    public void TrashPanelDisplay() //Used at the end of OpenTacho animation
    {
        TrashPanelManager.SharedInstance.TrashPanelSwitcher(true);
        TrashPanelManager.SharedInstance.ContainerButtonsSwitcher(true);
        TrashPanelManager.SharedInstance.CurrentCanCategory = Category;
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(false);
        MainButtonsManager.SharedInstance.SetTimeScale(0);
    }
}