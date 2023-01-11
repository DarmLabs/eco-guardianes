using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : InteractableObjectBase
{
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
        TrashContainer.ObjectUnfound();
    }
    public override void InteractWithObject()
    {
        gameObject.SetActive(false);
        TrashContainer.ObjectFound();
        TrashContainer.CorrectCategory = Category;
    }
}
