using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] Material mat;
    public void SetColorFromPicker(Color color)
    {
        mat.SetColor("_BaseColor", color);
    }
}
