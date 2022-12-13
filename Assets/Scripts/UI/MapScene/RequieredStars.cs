using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RequieredStars : MonoBehaviour
{
    Image _image;
    public Image _Image => _image;
    Button _button;
    public Button _Button => _button;
    [SerializeField] int requieredStars;
    public int GetRequieredStars => requieredStars;
    [SerializeField] TextMeshProUGUI requieredStarsText;
    void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }
    void Start()
    {
        if (requieredStars != 0)
        {
            requieredStarsText.text = $"¡Necesitas {requieredStars} estrellas!";
        }
    }
}
