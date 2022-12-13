using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class MapManager : MonoBehaviour
{
    StarsData starsData;
    [SerializeField] TextMeshProUGUI currentStarsText;
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
    public void ChangeScene(string scene) //Used on map buttons
    {
        if (ScenesChanger.SharedInstance != null)
        {
            ScenesChanger.SharedInstance.SceneChange(scene);
        }
    }
}
