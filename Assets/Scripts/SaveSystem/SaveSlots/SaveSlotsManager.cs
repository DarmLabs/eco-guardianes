using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSlotsManager : MonoBehaviour
{
    [SerializeField] GameObject confirmPanel;
    SaveSlot saveSlotForDelete;
    public void OnCreateOrChooseButton(SaveSlot saveSlot) //Used on slots buttons
    {
        SaveDataHandler.SharedInstance?.SetSaveDataId(saveSlot.SlotId);
        ScenesChanger.SharedInstance?.SceneChange(ConstManager.mainMenuSceneName);
    }
    public void ConfirmFileDelete() //Used on confirm panel on button "Aceptar"
    {
        File.Delete($"{SaveDataHandler.SharedInstance?.DataPath}saveData{saveSlotForDelete.SlotId}");
        saveSlotForDelete.ResetSlot();
        saveSlotForDelete = null;
    }
    public void ConfirmPanelSwitcher(bool state)
    {
        confirmPanel.SetActive(state);
    }
    public void SetSlotIdForDelete(SaveSlot saveSlot)
    {
        saveSlotForDelete = saveSlot;
    }
}
