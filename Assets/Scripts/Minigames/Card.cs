using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Card : MonoBehaviour
{
    public Sprite CardSprite { get; set; }
    RectTransform _rectTransform;
    Image _image;
    Button _button;
    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();

        _button.onClick.AddListener(() => StartCoroutine(RotateCardToTop()));
    }
    void Start()
    {
        MemoryManager.SharedInstance.isCardFlipping.AddListener(SwitchButtonInteraction);
    }
    IEnumerator RotateCardToTop()
    {
        MemoryManager.SharedInstance.isCardFlipping.Invoke(true);
        _rectTransform.DORotate(new Vector3(0, 90, 0), 0.25f);
        yield return new WaitForSeconds(0.25f);

        _image.sprite = CardSprite;

        _rectTransform.DORotate(new Vector3(0, 0, 0), 0.25f);
        yield return new WaitForSeconds(0.25f);

        MemoryManager.SharedInstance.cardFlipped.Invoke(this);
        MemoryManager.SharedInstance.isCardFlipping.Invoke(false);
    }
    public IEnumerator RotateToBack()
    {
        MemoryManager.SharedInstance.isCardFlipping.Invoke(true);
        _rectTransform.DORotate(new Vector3(0, 90, 0), 0.25f);
        yield return new WaitForSeconds(0.25f);

        _image.sprite = MemoryManager.SharedInstance.BackSprite;

        _rectTransform.DORotate(new Vector3(0, 0, 0), 0.25f);
        yield return new WaitForSeconds(0.25f);
        MemoryManager.SharedInstance.isCardFlipping.Invoke(false);
    }
    void SwitchButtonInteraction(bool state)
    {
        _button.interactable = !state;
    }
}
