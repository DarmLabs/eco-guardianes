using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MapManager : MonoBehaviour
{
    StarsData starsData;
    [SerializeField] TextMeshProUGUI currentStarsText;
    [SerializeField] GameObject confirmationPanel;
    [SerializeField] GameObject starsHUD;
    [SerializeField] TextMeshProUGUI sceneText;
    string sceneForward;
    [SerializeField] RequieredStars[] sceneStars;
    void Awake()
    {
        if (SaveDataHandler.SharedInstance != null)
        {
            starsData = SaveDataHandler.SharedInstance.LoadStarsData();
        }
        else
        {
            starsData = new StarsData(1);
        }
    }
    void Start()
    {
        if (starsData != null)
        {
            currentStarsText.text = $"¡Tienes {starsData.starsCount} estrellas!";
        }
        for (int i = 0; i < sceneStars.Length; i++)
        {
            if (sceneStars[i].GetRequieredStars <= starsData.starsCount)
            {
                sceneStars[i]._Button.interactable = true;
                sceneStars[i]._Image.color = Color.white;
                sceneStars[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void OnYesButton()
    {
        ScenesChanger.SharedInstance.SceneChange(sceneForward);
    }
    public void OnNoButton()
    {
        ConfirmScene(false);
    }
    public void OnSceneButtonClicked(string sceneForward)
    {
        this.sceneForward = sceneForward;
        sceneText.text = $"¿Quieres ir a la {sceneForward}?";
        ConfirmScene(true);
    }
    void ConfirmScene(bool state)
    {
        starsHUD.SetActive(!state);
        confirmationPanel.SetActive(state);
    }
}
