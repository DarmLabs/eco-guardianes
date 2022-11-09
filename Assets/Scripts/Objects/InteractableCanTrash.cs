using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanTrash : InteractableObject
{
    Animator anim;
    void Start()
    {
        optionalLookAt = transform.parent;
        anim = GetComponent<Animator>();
    }
    public override void InteractWithObject()
    {
        anim.Play("Open");
    }
}
