using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class SaveSlot : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] int slotId;
    public int SlotId => slotId;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI starsText;
    [SerializeField] GameObject chooseBtn;
    [SerializeField] GameObject deleteBtn;
    [SerializeField] GameObject createBtn;
    void Start()
    {
        CheckSlotExists();
    }
    void CheckSlotExists()
    {
        string dataPath = SaveDataHandler.SharedInstance?.DataPath;
        if (File.Exists($"{dataPath}saveData{slotId}"))
        {
            PopulateSlotById(FileHandler.ReadFromJSON<SaveData>($"saveData{slotId}"));
        }
        else
        {
            PopulateEmptySlot();
        }
    }
    void PopulateSlotById(SaveData saveData)
    {
        nameText.gameObject.SetActive(true);
        starsText.gameObject.SetActive(true);

        nameText.text = $"NOMBRE: {saveData.characterData.characterName}";
        int globalStars = saveData.starsData.starsCasaCount + saveData.starsData.starsEscuelaCount + saveData.starsData.starsPlazaCount;
        starsText.text = $"ESTRELLAS CONSEGUIDAS: {globalStars}";

        chooseBtn.SetActive(true);
        deleteBtn.SetActive(true);
    }
    void PopulateEmptySlot()
    {
        createBtn.SetActive(true);
    }
    public void ResetSlot()
    {
        nameText.gameObject.SetActive(false);
        starsText.gameObject.SetActive(false);

        nameText.text = "";
        starsText.text = "";

        chooseBtn.SetActive(false);
        deleteBtn.SetActive(false);

        PopulateEmptySlot();
    }
}
