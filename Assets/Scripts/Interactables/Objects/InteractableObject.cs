using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : InteractableObjectBase
{
    [TextArea][SerializeField] string objectPhrase;
    public string ObjectPhrase => objectPhrase;
    [SerializeField] Vector3 viewOffset;
    public Vector3 ViewOffset => viewOffset;
    [TextArea][SerializeField] string objectActionText;
    public string ObjectActionText => objectActionText;
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
    }
    public override void InteractWithObject()
    {
        gameObject.SetActive(false);
        TrashContainer.ObjectFound();
        TrashContainer.CorrectCategory = Category;
        TrashPanelManager.SharedInstance.hasNewTrash.Invoke(true);
    }
}
