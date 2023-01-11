using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainButtonsMask : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent enterMask;
    public UnityEvent leaveMask;
    public void OnPointerEnter(PointerEventData data)
    {
        enterMask.Invoke();
    }
    public void OnPointerExit(PointerEventData data)
    {
        leaveMask.Invoke();
    }
}
