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
    [SerializeField] Button regarButton;
    [SerializeField] Button waitButton;
    [SerializeField] Button removerButton;
    [SerializeField] Button finishButton;
    [SerializeField] GameObject waitingScreen;
    void Awake()
    {
        SharedInstance = this;
        guideTextRTransform = guideText.GetComponent<RectTransform>();
    }
    void Start()
    {
        if (LoadingPopupManager.SharedInstance != null)
        {
            StartCoroutine(LoadingPopupManager.SharedInstance.WaitForSwitchPopup(2f, false));
        }
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
        regarButton.onClick.AddListener(StepFive);
        regarButton.gameObject.SetActive(true);
        guideText.text = ConstManager.compostSteps_forthStep;
    }
    void StepFive()
    {
        regarButton.gameObject.SetActive(false);
        waitButton.onClick.AddListener(StepSixP1);
        waitButton.gameObject.SetActive(true);
        guideText.text = ConstManager.compostSteps_fifthStep;
    }
    void StepSixP1()
    {
        StartCoroutine(StartWaitingScreen());
        waitButton.gameObject.SetActive(false);
        removerButton.gameObject.SetActive(true);
        removerButton.onClick.AddListener(StepSixP2);
        guideText.text = ConstManager.compostSteps_sixthStepP1;
    }
    void StepSixP2()
    {
        removerButton.gameObject.SetActive(false);
        dryButton.transform.position = removerButton.transform.position;
        guideText.text = ConstManager.compostSteps_sixthStepP2;
        dryButton.interactable = true;
        dryButton.gameObject.SetActive(true);
        dryButton.onClick.AddListener(StepSeven);
    }
    void StepSeven()
    {
        dryButton.gameObject.SetActive(false);
        waitButton.onClick.RemoveAllListeners();
        waitButton.onClick.AddListener(StepEight);
        waitButton.gameObject.SetActive(true);
        guideText.text = ConstManager.compostSteps_sevenStep;
    }
    void StepEight()
    {
        StartCoroutine(StartWaitingScreen());
        waitButton.gameObject.SetActive(false);
        guideText.text = ConstManager.compostSteps_eightStep;
        wetButton.onClick.RemoveAllListeners();
        wetButton.onClick.AddListener(FinalStep);
        wetButton.transform.position = dryButton.transform.position;
        wetButton.gameObject.SetActive(true);
    }
    void FinalStep()
    {
        wetButton.gameObject.SetActive(false);
        finishButton.onClick.AddListener(delegate { ScenesChanger.SharedInstance?.SceneChange(ConstManager.mapSceneName); AchievementsManager.SharedInstance?.AchievementEarned(1); });
        guideText.text = ConstManager.compostSteps_finalStep;
        finishButton.gameObject.SetActive(true);
    }
    IEnumerator StartWaitingScreen()
    {
        waitingScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        waitingScreen.SetActive(false);
    }
}
