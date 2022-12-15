using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : InteractableObjectBase
{
    ObjectData objectData;
    public ObjectData ObjectData
    {
        get => objectData;
        set => objectData = value;
    }
    [SerializeField] string objectPhrase;
    public string ObjectPhrase => objectPhrase;
    void Start()
    {
        if (transform.GetChild(1) != null)
        {
            LookAt = transform.GetChild(1);
        }
        else
        {
            LookAt = transform;
        }
        objectData = new ObjectData(false);
        if (!objectData.isFound)
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
        objectData.isFound = true;
        /*TrashContainer.ObjectFound();
        TrashContainer.CorrectCategory = Category;*/
    }
}
