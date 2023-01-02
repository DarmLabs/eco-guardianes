using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler_AnimController : Base_AnimController
{
    const string poseId = "Pose_0";
    [Range(1, 3)][SerializeField] int poseAnimStyle;
    [SerializeField] bool isPerformingAction;
    public override void Awake()
    {
        base.Awake();
        idleMaxIndex = 5;
        sitMaxIndex = 3;
        ChooseStartAnimation();
    }
    public override void ChooseStartAnimation()
    {
        if (isPerformingAction)
        {
            PlayAnimation($"{poseId}{poseAnimStyle}");
            return;
        }
        base.ChooseStartAnimation();
    }
}
