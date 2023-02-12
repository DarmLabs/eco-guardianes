using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class TrashPanelManager : MonoBehaviour
{
    public static TrashPanelManager SharedInstance;
    [Header("Main Items")]
    [SerializeField] GameObject trashPanel;
    public GameObject TrashPanel => trashPanel;
    [SerializeField] TrashContainer[] containers;
    [SerializeField] TextMeshProUGUI tutoQuestion;
    [SerializeField] GameObject closeButton;
    public GameObject CloseButton => closeButton;
    bool isOpened;
    public bool IsOpened => isOpened;
    public UnityEvent<bool> hasNewTrash;
    [Header("Trash Type Buttons Items")]
    [SerializeField] Color canButtonColorSelected;
    public Color CanButtonColorSelected => canButtonColorSelected;
    [SerializeField] Color canButtonColorUnselected;
    public Color CanButtonColorsUnselected => canButtonColorUnselected;
    [SerializeField] GameObject canButtons;
    CanButtonHelper previousButton;
    TrashCategory currentCanCategory;

    //For sprite changes
    [Space(10)]
    [Header("For sprite changes on TrashContainers")]
    [SerializeField] Sprite unfound;
    public Sprite Unfound => unfound;
    [SerializeField] Sprite found;
    public Sprite Found => found;
    [SerializeField] Color unfoundColor;
    public Color UnfoundColor => unfoundColor;
    [SerializeField] Color foundColor;
    public Color FoundColor => foundColor;
    [SerializeField] Sprite recSprite, trashSprite, organicSprite;
    public Sprite RecSprite => recSprite;
    public Sprite TrashSprite => trashSprite;
    public Sprite OrganicSprite => organicSprite;
    [SerializeField] Sprite good, bad;
    public Sprite Good => good;
    public Sprite Bad => bad;
    int succeses;
    public int Succeses
    {
        get => succeses;
        set => succeses = value;
    }
    int remainingObjects;

    [Space(10)]
    [Header("For sprite changes on TrashButton")]

    [SerializeField] Sprite exlamation;
    [SerializeField] Sprite questionMark;
    Image newTrashMark;
    Animator trashButtonAnim;
    void Awake()
    {
        SharedInstance = this;
        trashPanel.SetActive(true);
        hasNewTrash.AddListener(NewTrash);
    }
    void Start()
    {
        currentCanCategory = TrashCategory.Rec;
        trashButtonAnim = MainButtonsManager.SharedInstance.TrashButton?.GetComponent<Animator>();
        newTrashMark = MainButtonsManager.SharedInstance.TrashButton?.transform.GetChild(1).gameObject.GetComponent<Image>();
        if (OpenObjectsManager.SharedInstance?.InteractableObjects.Length == containers.Length && OpenObjectsManager.SharedInstance?.InteractableObjects.Length != 0)
        {
            for (int i = 0; i < OpenObjectsManager.SharedInstance.InteractableObjects.Length; i++)
            {
                OpenObjectsManager.SharedInstance.InteractableObjects[i].TrashContainer = containers[i];
                OpenObjectsManager.SharedInstance.InteractableObjects[i].TrashContainer.ObjectUnfound();
                containers[i].ObjSprite.sprite = OpenObjectsManager.SharedInstance.InteractableObjects[i].ObjSprite;
                containers[i].CorrectCategory = OpenObjectsManager.SharedInstance.InteractableObjects[i].Category;
            }
            remainingObjects = OpenObjectsManager.SharedInstance.InteractableObjects.Length;
        }
        StartCoroutine(WaitForFrame());

    }
    IEnumerator WaitForFrame()
    {
        yield return new WaitForEndOfFrame();
        previousButton = canButtons.transform.GetChild(0).GetComponent<CanButtonHelper>();
        previousButton.SetTransparency(true);
        SetTutoQuestion("recuperables");
        trashPanel.SetActive(false);

        if (LoadingPopupManager.SharedInstance != null)
        {
            StartCoroutine(LoadingPopupManager.SharedInstance.WaitForSwitchPopup(2f, false));
        }
    }
    public void TrashPanelSwitcher(bool state)
    {
        trashPanel.SetActive(state);
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(!state);
        MainButtonsManager.SharedInstance.onMainButtonClicked.Invoke();
        isOpened = state;
    }
    public void OnTrashPanel()
    {
        hasNewTrash.Invoke(false);
    }
    public void ContainerButtonsSwitcher(bool state)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            if (containers[i].IsFound && !containers[i].IsThrow)
            {
                containers[i].GetComponent<Button>().interactable = state;
            }
        }
    }
    public void ThrowTrash()
    {
        TrashContainer container = EventSystem.current.currentSelectedGameObject.GetComponent<TrashContainer>();
        container.TrashCanColor(currentCanCategory);
    }
    public void CheckRemainingObjects()
    {
        remainingObjects--;
        if (remainingObjects == 0)
        {
            trashPanel.SetActive(false);
            StarsManager.SharedInstance.TriggerWinCondition();
        }
    }
    const string tutoQuestionBase = "Que residuo quieres tirar en el tacho de ";
    public void SetTutoQuestion(string canType)
    {
        tutoQuestion.text = $"¿{tutoQuestionBase}{canType}?";
    }
    public void PreviousButtonReset(CanButtonHelper canButton)
    {
        previousButton.SetTransparency(false);
        previousButton = canButton;
        canButton.SetTransparency(true);
        currentCanCategory = canButton.Category;
    }
    public void NonCellsUI(bool state)
    {
        canButtons.SetActive(state);
        tutoQuestion.enabled = state;
    }
    public void SwitchItemsForTutorial(bool state)
    {
        for (int i = 0; i < canButtons.transform.childCount; i++)
        {
            Button canButton = canButtons.transform.GetChild(i).GetComponent<Button>();
            canButton.interactable = state;
        }
    }
    # region For button setters
    public void SetCanCateogry()
    {
        currentCanCategory = TrashCategory.None;
    }
    #endregion
    #region Events Calls
    void NewTrash(bool hasNew)
    {
        if (newTrashMark != null && trashButtonAnim != null)
        {
            newTrashMark.sprite = hasNew ? exlamation : questionMark;

            string animToPlay = hasNew ? "TrashButtonBlink" : "Idle";
            trashButtonAnim.Play(animToPlay);
        }
    }
    #endregion
}
