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
    [SerializeField] GameObject good, bad;
    TrashCategory correctCategory;
    public TrashCategory CorrectCategory
    {
        get { return correctCategory; }
        set { correctCategory = value; }
    }
    void Awake()
    {
        container = GetComponent<Image>();
    }
    public void ObjectFound()
    {
        container.sprite = TrashPanelManager.SharedInstance.Found;
        objSprite.color = TrashPanelManager.SharedInstance.FoundColor;
    }
    public void ObjectUnfound()
    {
        container.sprite = TrashPanelManager.SharedInstance.Unfound;
        objSprite.color = TrashPanelManager.SharedInstance.UnfoundColor;
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
    }
    void GoodOrBad(bool correct)
    {
        if (correct)
        {
            good.SetActive(true);
        }
        else
        {
            bad.SetActive(false);
        }
    }
}