using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager SharedInstance;
    TutorialData tutorialData;
    public bool IsTutorialRunning { get; private set; }
    PlayerInteraction playerInteraction;
    Vector3 textBoxOriginalPos;
    Vector2 textBoxOriginalSize;
    [Header("Requiered items")]
    [SerializeField] GameObject tutoPanel;
    [SerializeField] GameObject completePanel;
    [SerializeField] RectTransform markerHole;
    [SerializeField] GameObject trashButton;
    [SerializeField] GameObject hand;
    Animator handAnim;
    [SerializeField] GameObject cutoutPanel;
    [SerializeField] GameObject skipTutoButton;
    [SerializeField] Button nextStageButton;
    [SerializeField] RectTransform tutoTextBox;
    TextMeshProUGUI tutoText;
    [SerializeField] InteractableObject interactableObjectUsed;
    [SerializeField] GameObject centerTrashCan;
    Coroutine inWaitCoroutine;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        tutorialData = SaveDataHandler.SharedInstance?.LoadTutoFirstTime();
        textBoxOriginalPos = tutoTextBox.transform.localPosition;
        textBoxOriginalSize = tutoTextBox.sizeDelta;

        playerInteraction = PointAndClickMovement.SharedInstance.gameObject.GetComponent<PlayerInteraction>();
        handAnim = hand.GetComponent<Animator>();
        tutoText = tutoTextBox.GetComponentInChildren<TextMeshProUGUI>();
        if (tutorialData == null)
        {
            tutorialData = new TutorialData(true);
        }
        if (!tutorialData.firstTimePassed)
        {
            ActivateTuto();
        }
    }

    public void TutoSwitcher(bool state)
    {
        tutoPanel.SetActive(state);
        playerInteraction.enabled = !state;
    }
    public void ActivateTuto()
    {
        PointAndClickMovement.SharedInstance.CurrentPosition = PointAndClickMovement.SharedInstance.transform.position;
        PointAndClickMovement.SharedInstance.transform.position = PointAndClickMovement.SharedInstance.InitialPosition;
        IsTutorialRunning = true;
        TutoSwitcher(true);
        if (!tutorialData.firstTimePassed)
        {
            inWaitCoroutine = StartCoroutine(FirstStage());
        }
        else
        {
            inWaitCoroutine = StartCoroutine(SecondStage());
        }
    }
    IEnumerator FirstStage()
    {
        skipTutoButton.SetActive(true);
        if (Application.isEditor)
        {
            IsTutorialRunning = true;
        }

        if (tutorialData != null)
        {
            SaveDataHandler.SharedInstance?.SaveTutoFirstTime();
            tutorialData.firstTimePassed = true;
        }

        ResetTutoItems();
        yield return PrintDialog(ConstManager.tuto_firstStageMessege);
        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(SecondStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator SecondStage()
    {
        ResetTutoItems();
        yield return PrintDialog(ConstManager.tuto_secondStageMessege);
        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(ThirdStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator ThirdStage()
    {
        completePanel.SetActive(false);
        ResetTutoItems();

        Sequence seq = DOTween.Sequence();
        seq.Append(tutoTextBox.DOLocalMoveY(-355, 1f));
        seq.Join(tutoTextBox.DOSizeDelta(new Vector2(1185, 300), 1f));
        yield return seq.Play();

#if (UNITY_STANDALONE || UNITY_EDITOR)
        yield return PrintDialog(ConstManager.tuto_thirdStageMessege_PC);
#elif (UNITY_ANDORID || UNITY_IOS)
        yield return PrintDialog(ConstManager.tuto_thirdStageMessege_MOBILE);
#endif
        hand.SetActive(true);

        nextStageButton.onClick.AddListener(delegate
        {
            StopCoroutine(inWaitCoroutine);
            inWaitCoroutine = StartCoroutine(ForthStage());
        });
        nextStageButton.gameObject.SetActive(true);

        yield return TutorialHandWarp(new Vector3(366, 18, 0), 5);
        yield return TutorialHandMove(new Vector3(-487, 182, 0), 1, 1);
    }
    IEnumerator ForthStage()
    {
        ResetTutoItems();

        Vector2 interactableCords = Camera.main.WorldToScreenPoint(interactableObjectUsed.transform.position);
        MarkerHoleHelper(interactableCords, new Vector2(140, 130));

        interactableObjectUsed.Outline.enabled = true;

        hand.SetActive(true);
        yield return TutorialHandWarp(new Vector3(114, 329, 0));
        yield return PrintDialog(ConstManager.tuto_forthStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(FifthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator FifthStage()
    {
        ResetTutoItems();
        MarkerHoleHelper(MainButtonsManager.SharedInstance.TrashButton.transform.position, new Vector2(140, 130));

        hand.SetActive(true);
        yield return TutorialHandWarp(new Vector3(865, 212, 0), rotation: new Vector3(0, 0, -25));
        yield return PrintDialog(ConstManager.tuto_fifthStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(SixthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }

    IEnumerator SixthStage()
    {
        ResetTutoItems(true);
        ResetHandRotation();

        trashButton.SetActive(true);

        hand.SetActive(true);
        yield return TutorialHandMove(new Vector3(54, 127, 0), 1, 0.5f);
        yield return PrintDialog(ConstManager.tuto_sixthStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(SeventhStage()); trashButton.SetActive(false); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator SeventhStage()
    {
        ResetTutoItems();
        MarkerHoleHelper(new Vector3(1025, 870, 0), new Vector2(805, 201));
        markerHole.gameObject.SetActive(true);

        TrashPanelManager.SharedInstance.CloseButton.SetActive(false);
        TrashPanelManager.SharedInstance.TrashPanel.SetActive(true);
        TrashPanelManager.SharedInstance.SwitchItemsForTutorial(false);

        Sequence seq = DOTween.Sequence();
        seq.Append(tutoTextBox.DOLocalMove(new Vector3(72, 75, 0), 1f));
        seq.Join(tutoTextBox.DOSizeDelta(new Vector2(890, 300), 1f));
        yield return seq.Play();
        yield return PrintDialog(ConstManager.tuto_seventhStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(EighthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator EighthStage()
    {
        ResetTutoItems();
        MarkerHoleHelper(new Vector3(1030, 500, 0), new Vector2(735, 555));

        Sequence seq = DOTween.Sequence();
        seq.Append(tutoTextBox.DOLocalMove(new Vector3(52, 380, 0), 1f));
        seq.Join(tutoTextBox.DOSizeDelta(new Vector2(1050, 265), 1f));
        yield return seq.Play();
        yield return PrintDialog(ConstManager.tuto_eighthStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(NinthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator NinthStage()
    {
        ResetTutoItems();
        MarkerHoleHelper(new Vector3(1014, 166, 0), new Vector2(702, 144));

        Sequence seq = DOTween.Sequence();
        seq.Append(tutoTextBox.DOLocalMoveY(-116, 1f));
        yield return seq.Play();
        yield return PrintDialog(ConstManager.tuto_eighthStageMessege);

        nextStageButton.onClick.AddListener(delegate
        {
            inWaitCoroutine = StartCoroutine(TenthStage());
            TrashPanelManager.SharedInstance.CloseButton.SetActive(true);
            TrashPanelManager.SharedInstance.TrashPanel.SetActive(false);
            TrashPanelManager.SharedInstance.SwitchItemsForTutorial(true);
        });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator TenthStage()
    {
        ResetTutoItems();

        MainButtonsManager.SharedInstance.MainButtonsSwitcher(false);
        tutoPanel.SetActive(false);

        TransitionsManager.SharedInstance.SetCameraPosAndLookAt(centerTrashCan.transform, centerTrashCan.transform.position + new Vector3(0, 8, -8));
        yield return TransitionsManager.SharedInstance.WaitForTransition(TransitionsManager.SharedInstance.ViewCamera, 11);

        tutoTextBox.transform.localPosition = new Vector3(0, -320, 0);
        tutoTextBox.sizeDelta = new Vector2(1215, 364);
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(true);

        Vector2 trashCanCords = Camera.main.WorldToScreenPoint(centerTrashCan.transform.position + new Vector3(0, 0.75f, 0));
        MarkerHoleHelper(trashCanCords, new Vector2(702, 250));
        tutoPanel.SetActive(true);

        yield return PrintDialog(ConstManager.tuto_tenthStageMessege);

        nextStageButton.onClick.AddListener(delegate { inWaitCoroutine = StartCoroutine(EndStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator EndStage()
    {
        ResetTutoItems();

        MainButtonsManager.SharedInstance.MainButtonsSwitcher(false);
        tutoPanel.SetActive(false);
        skipTutoButton.SetActive(false);
        completePanel.SetActive(true);
        cutoutPanel.SetActive(false);

        yield return TransitionsManager.SharedInstance.WaitForTransition(TransitionsManager.SharedInstance.ViewCamera, 9);

        tutoTextBox.transform.localPosition = textBoxOriginalPos;
        tutoTextBox.sizeDelta = textBoxOriginalSize;
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(true);
        tutoPanel.SetActive(true);

        yield return PrintDialog(ConstManager.tuto_endStageMessege);

        nextStageButton.onClick.AddListener(delegate
        {
            StopCoroutine(inWaitCoroutine);
            TutoSwitcher(false);
            PointAndClickMovement.SharedInstance.transform.position = PointAndClickMovement.SharedInstance.CurrentPosition;
            IsTutorialRunning = false;
            tutorialData.firstTimePassed = true;
        });
        nextStageButton.gameObject.SetActive(true);
    }
    public void SkipTutorialButton()
    {
        StartCoroutine(SkipTutorial());
    }
    IEnumerator SkipTutorial()
    {
        ResetTutoItems();

        completePanel.SetActive(true);
        skipTutoButton.SetActive(false);
        markerHole.gameObject.SetActive(false);
        trashButton.SetActive(false);
        cutoutPanel.SetActive(false);

        TrashPanelManager.SharedInstance.CloseButton.SetActive(true);
        TrashPanelManager.SharedInstance.TrashPanel.SetActive(false);
        TrashPanelManager.SharedInstance.SwitchItemsForTutorial(true);

        tutoTextBox.transform.localPosition = textBoxOriginalPos;
        tutoTextBox.sizeDelta = textBoxOriginalSize;

        yield return TransitionsManager.SharedInstance.WaitForTransition(TransitionsManager.SharedInstance.ViewCamera, 9, 0);
        yield return PrintDialog(ConstManager.tuto_skip);

        nextStageButton.onClick.AddListener(delegate
        {
            StopCoroutine(inWaitCoroutine);
            TutoSwitcher(false);
            PointAndClickMovement.SharedInstance.transform.position = PointAndClickMovement.SharedInstance.CurrentPosition;
            IsTutorialRunning = false;
            tutorialData.firstTimePassed = true;
        });
        nextStageButton.gameObject.SetActive(true);
    }
    void ResetTutoItems(bool withoutCutout = false)
    {
        nextStageButton.onClick.RemoveAllListeners();
        nextStageButton.gameObject.SetActive(false);
        tutoText.text = "";
        if (!withoutCutout)
        {
            cutoutPanel.transform.SetParent(tutoPanel.transform);
        }
        hand.SetActive(false);
    }
    IEnumerator PrintDialog(string line)
    {
        tutoText.text = line;
        /*foreach (var character in line)
        {
            tutoText.text += character;
            yield return new WaitForSeconds(1 / characterPerSecond);
        }*/
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator TutorialHandWarp(Vector3 target, float time = 0, Vector3? rotation = null)
    {
        hand.transform.localPosition = target;
        if (rotation != null)
        {
            hand.transform.Rotate((Vector3)rotation);
        }
        handAnim.Play("Mark");
        yield return new WaitForSeconds(time);
    }
    IEnumerator TutorialHandMove(Vector3 target, float moveDuration, float endAnimTime = 0)
    {
        handAnim.Play("Idle");
        hand.transform.DOLocalMove(target, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        handAnim.Play("Mark");
        yield return new WaitForSeconds(endAnimTime);
    }
    void ResetHandRotation()
    {
        hand.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void MarkerHoleHelper(Vector3 position, Vector2 sizeDelta)
    {
        markerHole.transform.position = position;
        markerHole.sizeDelta = sizeDelta;
        cutoutPanel.transform.SetParent(markerHole.transform);
    }
}
[Serializable]
public class TutorialData
{
    public bool firstTimePassed;
    public TutorialData(bool firstTimePassed)
    {
        this.firstTimePassed = firstTimePassed;
    }
}
