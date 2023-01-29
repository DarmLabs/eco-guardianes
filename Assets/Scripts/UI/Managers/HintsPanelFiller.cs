using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HintsPanelFiller : MonoBehaviour
{
    public static HintsPanelFiller SharedInstance;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] Image objImage;
    void Awake()
    {
        SharedInstance = this;
    }
    public void FillInfo(Sprite sprite, string info)
    {
        if (sprite == null)
        {
            objImage.enabled = false;
        }
        else
        {
            objImage.enabled = true;
            objImage.sprite = sprite;
        }
        infoText.text = info;
    }
}
