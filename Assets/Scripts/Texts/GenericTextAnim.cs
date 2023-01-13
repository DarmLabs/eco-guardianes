using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GenericTextAnim : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] List<string> animationSteps;
    [SerializeField] float waitingBetweenAnimSteps;
    int currentStep = 0;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        InvokeRepeating("AnimationStep", 0, waitingBetweenAnimSteps);
    }
    void AnimationStep()
    {
        if (currentStep > animationSteps.Count - 1)
        {
            currentStep = 0;
        }
        text.text = animationSteps[currentStep];
        currentStep++;
    }
}
