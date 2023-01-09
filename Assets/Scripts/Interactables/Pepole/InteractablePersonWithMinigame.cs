using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePersonWithMinigame : InteractableBase
{
    [SerializeField] string personName;
    [TextArea][SerializeField] string minigameMessege;
    [SerializeField] string sceneName;
    public override void InteractWithObject()
    {
        StartCoroutine(DialogManager.SharedInstance.SetDialog(characterName: personName, messege: minigameMessege, hasMinigames: true));
        DialogManager.SharedInstance.SetYesButtonForSceneChange(sceneName);
    }

}
