using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager SharedInstance;
    TutorialData tutorialData;
    public bool IsTutorialRunning { get; private set; }
    PlayerInteraction playerInteraction;
    [SerializeField] GameObject tutoPanel;
    [SerializeField] GameObject completePanel;
    [SerializeField] GameObject markerHole;
    [SerializeField] GameObject trashButton;
    [SerializeField] GameObject hand;
    Animator handAnim;
    [SerializeField] GameObject cutoutPanel;
    [SerializeField] GameObject skipTutoButton;
    [SerializeField] Button nextStageButton;
    [SerializeField] RectTransform tutoTextBox;
    TextMeshProUGUI tutoText;
    [SerializeField] InteractableObject interactableObjectUsed;
    Coroutine inWaitCoroutine;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        tutorialData = SaveDataHandler.SharedInstance?.LoadTutoFirstTime();

        playerInteraction = PointAndClickMovement.SharedInstance.gameObject.GetComponent<PlayerInteraction>();
        handAnim = hand.GetComponent<Animator>();
        tutoText = tutoTextBox.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(FirstStage());
    }

    public void TutoSwitcher(bool state)
    {
        tutoPanel.SetActive(state);
        playerInteraction.enabled = !state;
    }
    public void ActivateTuto()
    {
        IsTutorialRunning = true;
        if (!tutorialData.firstTimePassed)
        {
            StartCoroutine(FirstStage());
        }
        else
        {
            StartCoroutine(SecondStage());
        }
    }
    void ResetTutoItems()
    {
        nextStageButton.onClick.RemoveAllListeners();
        nextStageButton.gameObject.SetActive(false);
        tutoText.text = "";
        hand.SetActive(false);
    }
    IEnumerator FirstStage()
    {
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
        nextStageButton.onClick.AddListener(delegate { StartCoroutine(SecondStage()); });
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
            StartCoroutine(ForthStage());
            StopCoroutine(inWaitCoroutine);
            inWaitCoroutine = null;
        });
        nextStageButton.gameObject.SetActive(true);

        yield return TutorialHandWarp(new Vector3(366, 18, 0), 5);
        yield return TutorialHandMove(new Vector3(-487, 182, 0), 1, 1);
    }
    IEnumerator ForthStage()
    {

        ResetTutoItems();

        Vector2 interactableCords = Camera.main.WorldToScreenPoint(interactableObjectUsed.transform.position);
        markerHole.transform.position = interactableCords;
        cutoutPanel.transform.SetParent(markerHole.transform);

        interactableObjectUsed.Outline.enabled = true;

        hand.SetActive(true);
        yield return TutorialHandWarp(new Vector3(114, 329, 0));
        yield return PrintDialog(ConstManager.tuto_forthStageMessege);

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(FifthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator FifthStage()
    {
        ResetTutoItems();

        cutoutPanel.transform.SetParent(tutoPanel.transform);
        markerHole.transform.position = MainButtonsManager.SharedInstance.TrashButton.transform.position;
        cutoutPanel.transform.SetParent(markerHole.transform);

        hand.SetActive(true);
        yield return TutorialHandWarp(new Vector3(865, 212, 0), rotation: new Vector3(0, 0, -25));
        yield return PrintDialog(ConstManager.tuto_fifthStageMessege);

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(SixthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }

    IEnumerator SixthStage()
    {
        ResetTutoItems();
        ResetHandRotation();

        trashButton.SetActive(true);

        hand.SetActive(true);
        yield return TutorialHandMove(new Vector3(54, 127, 0), 1, 0.5f);
        yield return PrintDialog(ConstManager.tuto_sixthStageMessege);

        nextStageButton.onClick.AddListener(delegate { SeventhStage(); trashButton.SetActive(false); });
        nextStageButton.gameObject.SetActive(true);
    }
    void SeventhStage()
    {
        ResetTutoItems();


        tutoPanel.SetActive(false);
        TrashPanelManager.SharedInstance.CloseButton.SetActive(false);
        TrashPanelManager.SharedInstance.TrashPanel.SetActive(true);
        TrashPanelManager.SharedInstance.SwitchItemsForTutorial(false);

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(EighthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator EighthStage()
    {
        yield return null;
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
}

public class TutorialData
{
    public bool firstTimePassed;
    public TutorialData(bool firstTimePassed)
    {
        this.firstTimePassed = firstTimePassed;
    }
}
