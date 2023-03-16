using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public static DialogManager SharedInstance;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject nextDialogArrow;
    GameObject yesNoContainer;
    [SerializeField] Button yesButton;
    [SerializeField] float characterPerSecond = 30f;
    [SerializeField] TextMeshProUGUI characterNameText;
    string characterCurrentText;
    [SerializeField] TextMeshProUGUI dialogBoxText;
    string dialogCurrentText;
    Dialog currentDialog;
    int currentLine = 0;
    bool isWriting = false;
    void Awake()
    {
        SharedInstance = this;
        yesNoContainer = yesButton.transform.parent.gameObject;
    }

    public IEnumerator SetDialog(string characterName = "", string messege = "", Dialog dialog = null, bool hasMinigames = false, bool isOther = false)
    {
        if (!isWriting)
        {
            isWriting = true;

            characterNameText.text = "";
            dialogBoxText.text = "";
            DialogPanelSwitcher(true);
            yield return PrintCharacterName(isOther == true ? "" : $"{characterName} dice:");
            if (dialog != null && !hasMinigames)
            {
                currentLine = 0;
                currentDialog = dialog;
                yield return PrintDialog(dialog.Lines[0]);
                currentLine++;
                if (CheckForMoreLines())
                {
                    nextDialogArrow.SetActive(true);
                }
                else
                {
                    nextDialogArrow.SetActive(false);
                    DialogPanelSwitcher(false);
                }
            }
            else
            {
                yield return PrintDialog(messege);
                if (hasMinigames)
                {
                    yesNoContainer.SetActive(true);
                }
            }

            isWriting = false;
        }
    }
    bool CheckForMoreLines()
    {
        if (currentDialog.Lines.Count - 1 >= currentLine)
        {
            return true;
        }
        return false;
    }
    public void CheckForNextDialog()
    {
        dialogBoxText.text = "";
        StartCoroutine(NextDialog());
    }
    IEnumerator NextDialog()
    {
        nextDialogArrow.SetActive(false);
        yield return PrintDialog(currentDialog.Lines[currentLine]);
        yield return new WaitForSeconds(0.5f);
        currentLine++;
        if (!CheckForMoreLines())
        {
            yield return new WaitForSeconds(0.5f);
            DialogPanelSwitcher(false);
        }
        else
        {
            nextDialogArrow.SetActive(true);
        }
    }
    IEnumerator PrintCharacterName(string characterText)
    {
        foreach (var character in characterText)
        {
            characterNameText.text += character;
            yield return new WaitForSeconds(1 / characterPerSecond);
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator PrintDialog(string line)
    {
        foreach (var character in line)
        {
            dialogBoxText.text += character;
            yield return new WaitForSeconds(1 / characterPerSecond);
        }
        yield return new WaitForSeconds(0.5f);
    }
    public void DialogPanelSwitcher(bool state)
    {
        dialogPanel.SetActive(state);
        if (state)
        {
            ActionPanelManager.SharedInstance.panelOpened.Invoke();
        }
        else
        {
            ActionPanelManager.SharedInstance.panelClosed.Invoke();
        }
    }
    public void SetYesButtonForSceneChange(string scene)
    {
        yesButton.onClick.AddListener(delegate { ScenesChanger.SharedInstance?.SceneChange(scene); });
        yesButton.onClick.AddListener(delegate { LoadingPopupManager.SharedInstance?.LoadingPopupSwitcher(true); });
    }
}
