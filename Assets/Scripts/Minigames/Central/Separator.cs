using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeparatorType
{
    Negro,
    Amarillo,
    Azul,
    Blanco
}
[RequireComponent(typeof(BoxCollider))]
public class Separator : MonoBehaviour
{
    [SerializeField] SeparatorType type;
    public SeparatorType Type => type;
    Outline _outline;
    void Awake()
    {
        _outline = GetComponent<Outline>();
    }
    void Glow(bool state)
    {
        _outline.enabled = state;
    }
    public void DetectorEnter()
    {
        CentralManager.SharedInstance.Separator = this;
        Glow(true);
    }
    public void DetectorExit()
    {
        CentralManager.SharedInstance.Separator = null;
        Glow(false);
    }
}
