using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class TutorialManager : MonoBehaviour
{
    TutorialData tutorialData;
    PlayerInteraction playerInteraction;
    [SerializeField] GameObject tutoPanel;
    [SerializeField] GameObject[] tutoItems;
    [SerializeField] GameObject cutoutPanel;
    [SerializeField] GameObject skipTutoButton;
    [SerializeField] Button nextStageButton;
    [SerializeField] RectTransform tutoTextBox;
    TextMeshProUGUI tutoText;
    [SerializeField] float characterPerSecond = 50f;
    [SerializeField] InteractableObject interactableObjectUsed;
    void Start()
    {
        tutorialData = SaveDataHandler.SharedInstance?.LoadTutoFirstTime();

        playerInteraction = PointAndClickMovement.SharedInstance.gameObject.GetComponent<PlayerInteraction>();
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
    }
    IEnumerator FirstStage()
    {
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
        nextStageButton.onClick.AddListener(delegate { StartCoroutine(ThirdStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator ThirdStage()
    {
        tutoItems[0].SetActive(false);
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

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(ForthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator ForthStage()
    {
        ResetTutoItems();

        Vector2 interactableCords = Camera.main.WorldToScreenPoint(interactableObjectUsed.transform.position);
        tutoItems[1].transform.position = interactableCords;
        cutoutPanel.transform.SetParent(tutoItems[1].transform);

        interactableObjectUsed.Outline.enabled = true;

        yield return PrintDialog(ConstManager.tuto_forthStageMessege);

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(FifthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }
    IEnumerator FifthStage()
    {
        ResetTutoItems();

        cutoutPanel.transform.SetParent(tutoPanel.transform);
        tutoItems[1].transform.position = MainButtonsManager.SharedInstance.TrashButton.transform.position;
        cutoutPanel.transform.SetParent(tutoItems[1].transform);

        yield return PrintDialog(ConstManager.tuto_fifthStageMessege);

        nextStageButton.onClick.AddListener(delegate { StartCoroutine(SixthStage()); });
        nextStageButton.gameObject.SetActive(true);
    }

    IEnumerator SixthStage()
    {
        yield return null;
    }
    IEnumerator PrintDialog(string line)
    {
        foreach (var character in line)
        {
            tutoText.text += character;
            yield return new WaitForSeconds(1 / characterPerSecond);
        }
        yield return new WaitForSeconds(0.5f);
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
