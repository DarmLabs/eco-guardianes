using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogManager : MonoBehaviour
{
    public static DialogManager SharedInstance;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] float delay = 0.01f;
    [SerializeField] TextMeshProUGUI characterNameText;
    string characterCurrentText;
    [SerializeField] TextMeshProUGUI dialogBoxText;
    string dialogCurrentText;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetDialog(string dialogText)
    {
        dialogPanel.SetActive(true);
        dialogBoxText.text = "";
        ActionPanelManager.SharedInstance.panelOpened.Invoke();
        string characterText = $"{PlayerCustomatization.SharedInstance.CharacterName} dice:";
        StartCoroutine(PrintDialog(characterText, dialogText));
    }
    IEnumerator PrintDialog(string characterText, string dialogText)
    {
        for (int i = 0; i < characterText.Length + 1; i++)
        {
            characterCurrentText = characterText.Substring(0, i);
            characterNameText.text = characterCurrentText;
            yield return new WaitForSeconds(delay);
        }
        for (int i = 0; i < dialogText.Length + 1; i++)
        {
            dialogCurrentText = dialogText.Substring(0, i);
            dialogBoxText.text = dialogCurrentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        dialogPanel.SetActive(false);
        ActionPanelManager.SharedInstance.panelClosed.Invoke();
    }
}
