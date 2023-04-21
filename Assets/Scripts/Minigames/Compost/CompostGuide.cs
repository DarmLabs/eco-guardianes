using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class CompostGuide : MonoBehaviour
{
    public static CompostGuide SharedInstance;
    [SerializeField] TextMeshProUGUI guideText;
    RectTransform guideTextRTransform;
    [SerializeField] Button wetButton;
    [SerializeField] Button dryButton;
    void Awake()
    {
        SharedInstance = this;
        guideTextRTransform = guideText.GetComponent<RectTransform>();
    }
    void Start()
    {
        guideText.text = ConstManager.compostSteps_firstStep;
    }
    void SwitchTrashButtons(bool state)
    {
        wetButton.gameObject.SetActive(state);
        dryButton.gameObject.SetActive(state);
    }
    void ResetButtonsListeners()
    {
        wetButton.onClick.RemoveAllListeners();
        dryButton.onClick.RemoveAllListeners();
    }
    public IEnumerator StepTwo()
    {
        wetButton.onClick.AddListener(WrongStepThree);
        dryButton.onClick.AddListener(StepThree);
        guideText.text = ConstManager.compostSteps_secondStep;
        Sequence seq = DOTween.Sequence();
        seq.Append(guideTextRTransform.DOLocalMoveY(238, 0.5f));
        yield return seq.Play();
        yield return new WaitForSeconds(0.5f);
        SwitchTrashButtons(true);
    }
    void StepThree()
    {
        ResetButtonsListeners();
        guideText.text = ConstManager.compostSteps_thirdStep;
        dryButton.interactable = false;
        wetButton.onClick.AddListener(StepFour);
    }
    void WrongStepThree()
    {
        guideText.text = ConstManager.compostSteps_thirdStepWrong;
    }
    void StepFour()
    {
        SwitchTrashButtons(false);
        guideText.text = ConstManager.compostSteps_forthStep;
    }
}
