using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ZonePicker : MonoBehaviour, IPointerDownHandler
{
    Animator anim;
    GameObject pjZones;
    Animator[] allAnim;
    void Start()
    {
        pjZones = transform.parent.gameObject;
        anim = GetComponent<Animator>();
        allAnim = pjZones.transform.GetComponentsInChildren<Animator>();
    }
    public void OnPointerDown(PointerEventData data)
    {
        foreach (var anim in allAnim)
        {
            anim.Play("Empty");
        }
        anim.Play("ZoneAnim");
        ZoneManager.SharedInstance.IsZoneSelected(true);
        ZoneManager.SharedInstance.SelectZone(name);
        ZoneManager.SharedInstance.ChangeSelectorText(name);
    }
}
