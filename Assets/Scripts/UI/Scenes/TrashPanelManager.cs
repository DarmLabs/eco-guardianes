using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class TrashPanelManager : MonoBehaviour
{
    public static TrashPanelManager SharedInstance;
    [SerializeField] TrashContainer[] containers;
    [SerializeField] GameObject trashPanel;
    [SerializeField] TextMeshProUGUI tutoQuestion;
    [SerializeField] Color canButtonColorSelected;
    public Color CanButtonColorSelected => canButtonColorSelected;
    [SerializeField] Color canButtonColorUnselected;
    public Color CanButtonColorsUnselected => canButtonColorUnselected;
    [SerializeField] CanButtonHelper canButtonRec;
    CanButtonHelper previousButton;
    TrashCategory currentCanCategory;
    public TrashCategory CurrentCanCategory
    {
        get { return currentCanCategory; }
        set { currentCanCategory = value; }
    }
    bool isOpened;
    public bool IsOpened => isOpened;
    //For sprite changes
    [Space(10)]
    [Header("For sprite changes")]
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
    void Awake()
    {
        SharedInstance = this;
        trashPanel.SetActive(true);
    }
    void Start()
    {
        if (OpenObjectsManager.SharedInstance?.InteractableObjects.Length == containers.Length && OpenObjectsManager.SharedInstance?.InteractableObjects.Length != 0)
        {
            for (int i = 0; i < OpenObjectsManager.SharedInstance.InteractableObjects.Length; i++)
            {
                OpenObjectsManager.SharedInstance.InteractableObjects[i].TrashContainer = containers[i];
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
        previousButton = canButtonRec;
        previousButton.SetTransparency(true);
        SetTutoQuestion("recuperables");
        trashPanel.SetActive(false);
    }
    public void TrashPanelSwitcher(bool state)
    {
        trashPanel.SetActive(state);
        MainButtonsManager.SharedInstance.MainButtonsSwitcher(!state);
        MainButtonsManager.SharedInstance.onMainButtonClicked.Invoke();
        isOpened = state;
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
    //For button setters
    public void SetCanCateogry()
    {
        currentCanCategory = TrashCategory.None;
    }
}
