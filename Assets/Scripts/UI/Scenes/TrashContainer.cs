using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrashContainer : MonoBehaviour
{
    [SerializeField] Image objSprite; //Trash picture
    public Image ObjSprite
    {
        get { return objSprite; }
        set { objSprite = value; }
    }
    Image container; //Container picture
    [SerializeField] Image colorIndicator;
    [SerializeField] Image correctMarker;
    TrashCategory correctCategory;
    public TrashCategory CorrectCategory
    {
        get { return correctCategory; }
        set { correctCategory = value; }
    }
    bool settled;
    public bool Settled => settled;
    void Awake()
    {
        container = GetComponent<Image>();
    }
    public void ObjectFound()
    {
        container.sprite = TrashPanelManager.SharedInstance.Found;
        objSprite.color = TrashPanelManager.SharedInstance.FoundColor;
        settled = true;
    }
    public void ObjectUnfound()
    {
        container.sprite = TrashPanelManager.SharedInstance.Unfound;
        objSprite.color = TrashPanelManager.SharedInstance.UnfoundColor;
        settled = true;
    }
    public void TrashCanColor(TrashCategory category)
    {
        switch (category)
        {
            case TrashCategory.Rec:
                colorIndicator.sprite = TrashPanelManager.SharedInstance.RecSprite;
                break;
            case TrashCategory.Trash:
                colorIndicator.sprite = TrashPanelManager.SharedInstance.TrashSprite;
                break;
            case TrashCategory.Organic:
                colorIndicator.sprite = TrashPanelManager.SharedInstance.OrganicSprite;
                break;
        }
        if (category == correctCategory)
        {
            GoodOrBad(true);
        }
        else
        {
            GoodOrBad(false);
        }
        colorIndicator.gameObject.SetActive(true);
        correctMarker.gameObject.SetActive(true);
    }
    void GoodOrBad(bool correct)
    {
        if (correct)
        {
            correctMarker.sprite = TrashPanelManager.SharedInstance.Good;
        }
        else
        {
            correctMarker.sprite = TrashPanelManager.SharedInstance.Bad;
        }
    }
}
