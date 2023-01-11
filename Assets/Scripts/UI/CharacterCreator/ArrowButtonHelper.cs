using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButtonHelper : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] string id;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { CharacterCreation.SharedInstance.Selector(value, id); });
    }
}
