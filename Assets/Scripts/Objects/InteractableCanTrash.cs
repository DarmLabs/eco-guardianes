using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanTrash : InteractableObjectBase
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void InteractWithObject()
    {
        if (gameObject.transform.childCount > 1)
        {
            anim.Play("Open");
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
