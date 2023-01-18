using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZoneManager : MonoBehaviour
{
    public static ZoneManager SharedInstance;
    [SerializeField] TextMeshProUGUI selectedText;
    [SerializeField] Material[] partMaterials;
    Material selectedMaterial;
    public bool zoneSelected { get; private set; }
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetColorFromPicker(Color color)
    {
        if (selectedMaterial != null)
        {
            selectedMaterial.color = color;
        }
        else
        {
            Debug.Log("selectedMaterial is null");
        }

    }
    public void SelectZone(string zoneName)
    {
        foreach (var partMaterial in partMaterials)
        {
            if (partMaterial.name == "mat_" + zoneName)
            {
                selectedMaterial = partMaterial;
            }
        }
    }
    public void ChangeSelectorText(string zoneName)
    {
        switch (zoneName)
        {
            case "HairZone":
                selectedText.text = "Color de cabello";
                break;
            case "PantZone":
                selectedText.text = "Color del pantalon";
                break;
            case "ShirtZone":
                selectedText.text = "Color de la remera";
                break;
            case "ShoeZone":
                selectedText.text = "Color de las zapatillas";
                break;
        }
    }
    public void IsZoneSelected(bool value)
    {
        zoneSelected = value;
    }
}
