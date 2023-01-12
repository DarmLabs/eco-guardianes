using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanTrash : InteractableObjectBase
{
    public override void InteractWithObject()
    {
        TrashPanelDisplay();
    }
    public override void Glow(bool state)
    {
        TrashCansManager.SharedInstance?.GlowAllCans(state);
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
