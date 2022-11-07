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
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetColorFromPicker(Color color)
    {
        if (selectedMaterial != null)
        {
            Debug.Log(selectedMaterial);
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
                selectedText.text = "Eligiendo color del pelo...";
                break;
            case "PantZone":
                selectedText.text = "Eligiendo color del pantalon...";
                break;
            case "ShirtZone":
                selectedText.text = "Eligiendo color de la remera...";
                break;
            case "ShoeZone":
                selectedText.text = "Eligiendo color de las zapatillas...";
                break;
        }
    }
}
