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
    public void SetTransparency(Color color)
    {
        _image.color = color;
    }
}
