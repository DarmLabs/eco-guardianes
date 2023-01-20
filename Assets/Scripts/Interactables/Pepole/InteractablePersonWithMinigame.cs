using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePersonWithMinigame : InteractableBase
{
    [SerializeField] string personName;
    [TextArea][SerializeField] string minigameMessege;
    [SerializeField] string sceneName;
    public override void SearchMode()
    {
        if (CanInteract)
        {
            PointAndClickMovement.SharedInstance.ResetDestinationObject();
            PointAndClickMovement.SharedInstance.TravelToTarget(this);
            BeingTargeted = true;
            Glow(false);
        }
    }
    public override void InteractWithObject()
    {
        StartCoroutine(DialogManager.SharedInstance.SetDialog(characterName: personName, messege: minigameMessege, hasMinigames: true));
        DialogManager.SharedInstance.SetYesButtonForSceneChange(sceneName);
    }

}
