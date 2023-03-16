using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePersonWithMinigame : InteractableBase
{
    [SerializeField] string personName;
    [TextArea][SerializeField] string minigameMessege;
    [SerializeField] string sceneName;
    [SerializeField] bool isOther;
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
    public override void TargetObject()
    {
        StartCoroutine(DialogManager.SharedInstance.SetDialog(characterName: personName, messege: minigameMessege, hasMinigames: true, isOther: isOther));
        DialogManager.SharedInstance.SetYesButtonForSceneChange(sceneName);
    }

}
