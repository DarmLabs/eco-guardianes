using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanButtonHelper : MonoBehaviour
{
    Image _image;
    [SerializeField] TrashCategory category;
    public TrashCategory Category => category;
    void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void SetButtonForTransparency()
    {
        TrashPanelManager.SharedInstance.PreviousButtonReset(this);
    }
    public void SetTransparency(bool isSelected)
    {
        Color selectedColor = isSelected ? TrashPanelManager.SharedInstance.CanButtonColorSelected : TrashPanelManager.SharedInstance.CanButtonColorsUnselected;
        _image.color = selectedColor;
    }
}
