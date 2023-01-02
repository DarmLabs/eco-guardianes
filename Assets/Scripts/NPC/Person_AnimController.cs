using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_AnimController : Base_AnimController
{
    public override void Awake()
    {
        base.Awake();
        ChooseStartAnimation();
    }
}
