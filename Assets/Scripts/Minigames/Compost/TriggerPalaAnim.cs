using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPalaAnim : MonoBehaviour
{
    Outline outline;
    Animator anim;
    bool hasTriggered = false;
    void Awake()
    {
        anim = GetComponent<Animator>();
        outline = GetComponent<Outline>();
    }
    void OnMouseDown()
    {
        if (outline.enabled == true)
        {
            TriggerAnim();
            Glow(false);
            hasTriggered = true;
        }
    }
    void OnMouseEnter()
    {
        if (!hasTriggered)
        {
            Glow(true);
        }
    }
    void OnMouseExit()
    {
        Glow(false);
    }
    void Glow(bool state)
    {
        outline.enabled = state;
    }
    void TriggerAnim()
    {
        anim.Play("Excavar");
    }
}
