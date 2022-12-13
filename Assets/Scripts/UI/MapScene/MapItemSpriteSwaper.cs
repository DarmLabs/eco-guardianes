using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MapItemSpriteSwaper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Sprite normalSprite;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite highlightedSprite;
    RequieredStars requieredStars;

    bool canInteract;
    void Start()
    {
        requieredStars = GetComponent<RequieredStars>();
        canInteract = requieredStars._Button.interactable;
        normalSprite = requieredStars._Image.sprite;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        CheckStatus(highlightedSprite);
    }
    public void OnPointerExit(PointerEventData data)
    {
        CheckStatus(normalSprite);
    }
    public void OnPointerClick(PointerEventData data)
    {
        CheckStatus(selectedSprite);
    }
    void CheckStatus(Sprite spirte)
    {
        if (canInteract)
        {
            requieredStars._Image.sprite = spirte;
        }
    }
}
