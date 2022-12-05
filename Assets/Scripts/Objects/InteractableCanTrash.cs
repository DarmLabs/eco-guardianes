using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanTrash : InteractableObject
{
    Animator anim;
    void Start()
    {
        OptionalLookAt = transform.parent;
        anim = GetComponent<Animator>();
        IsTrashCan = true;
    }
    public override void InteractWithObject()
    {
        anim.Play("Open");
    }
    public void TrashPanelDisplay() //Used at the end of OpenTacho animation
    {
        /*TrashPanelManager.SharedInstance.TrashPanelSwitcher(true);
        TrashPanelManager.SharedInstance.ContainerButtonsSwitcher(true);
        TrashPanelManager.SharedInstance.CurrentCanCategory = Category;
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(false);
        MainButtonsManager.SharedInstance.SetTimeScale(0);*/
    }
}
