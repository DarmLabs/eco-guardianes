using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : InteractableObjectBase
{
    bool isFound; //true = is in player inventory // Variable being saved
    [SerializeField] string objectPhrase;
    public string ObjectPhrase => objectPhrase;
    void Start()
    {
        if (!isFound)
        {
            //TrashContainer.ObjectUnfound();
        }
        else
        {
            TrashContainer.ObjectFound();
            TrashContainer.CorrectCategory = Category;
            gameObject.SetActive(false);
        }
    }
    public override void InteractWithObject()
    {
        base.InteractWithObject();
        isFound = true;
        //TrashContainer.ObjectFound();
        //TrashContainer.CorrectCategory = category;
    }
}
