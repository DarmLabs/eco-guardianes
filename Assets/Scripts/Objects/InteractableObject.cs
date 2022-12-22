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
    [SerializeField] Vector3 viewOffset;
    public Vector3 ViewOffset => viewOffset;
    public string ObjectPhrase => objectPhrase;
    TrashContainer trashContainer;
    public TrashContainer TrashContainer
    {
        get => trashContainer;
        set => trashContainer = value;
    }
    public override void Start()
    {
        base.Start();
        if (transform.childCount == 2)
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
            TrashContainer.ObjectUnfound();
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
        TrashContainer.ObjectFound();
        TrashContainer.CorrectCategory = Category;
    }
}
