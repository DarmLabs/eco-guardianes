using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogManager : MonoBehaviour
{
    public static DialogManager SharedInstance;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject nextDialogArrow;
    [SerializeField] float characterPerSecond = 20f;
    [SerializeField] TextMeshProUGUI characterNameText;
    string characterCurrentText;
    [SerializeField] TextMeshProUGUI dialogBoxText;
    string dialogCurrentText;
    Dialog currentDialog;
    int currentLine = 0;
    void Awake()
    {
        SharedInstance = this;
    }

    public IEnumerator SetDialog(string characterName = "", string messege = "", Dialog dialog = null)
    {
        characterNameText.text = "";
        dialogBoxText.text = "";
        DialogPanelSwitcher(true);

        yield return PrintCharacterName(characterName == "" ? $"{PlayerCustomatization.SharedInstance.CharacterName} dice:" : $"{characterName} dice:");
        if (dialog != null)
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
            DialogPanelSwitcher(false);
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
            DialogPanelSwitcher(false);
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
    void DialogPanelSwitcher(bool state)
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
}
