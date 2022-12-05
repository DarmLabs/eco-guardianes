using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TrashPanelManager : MonoBehaviour
{
    public static TrashPanelManager SharedInstance;
    [SerializeField] InteractableObject[] interactableObjects;
    [SerializeField] TrashContainer[] containers;
    [SerializeField] GameObject trashPanel;
    TrashCategory currentCanCategory;
    public TrashCategory CurrentCanCategory
    {
        get { return currentCanCategory; }
        set { currentCanCategory = value; }
    }

    //For sprite changes
    [SerializeField] Sprite unfound;
    public Sprite Unfound => unfound;
    [SerializeField] Sprite found;
    public Sprite Found => found;
    [SerializeField] Color unfoundColor;
    public Color UnfoundColor => unfoundColor;
    [SerializeField] Color foundColor;
    public Color FoundColor => foundColor;
    [SerializeField] Sprite recSprite, trashSprite, organicSprite;
    public Sprite RecSprite => recSprite;
    public Sprite TrashSprite => trashSprite;
    public Sprite OrganicSprite => organicSprite;
    void Start()
    {
        if (interactableObjects.Length == containers.Length && interactableObjects.Length != 0)
        {
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                interactableObjects[i].TrashContainer = containers[i];
                containers[i].ObjSprite.sprite = interactableObjects[i].ObjSprite;
            }
        }
    }
    public void TrashPanelSwitcher(bool state)
    {
        trashPanel.SetActive(state);
    }
    public void ContainerButtonsSwitcher(bool state)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i].GetComponent<Button>().interactable = state;
        }
    }
    public void ThrowTrash()
    {
        TrashContainer container = EventSystem.current.currentSelectedGameObject.GetComponent<TrashContainer>();
        container.TrashCanColor(currentCanCategory);
    }
    //For button setters
    public void SetCanCateogry()
    {
        currentCanCategory = TrashCategory.None;
    }
}
