using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterCreatorSceneManager : MonoBehaviour
{
    public static CharacterCreatorSceneManager SharedInstance;
    [SerializeField] string forwardScene;
    [SerializeField] Toggle wheelChairToggle;
    void Awake()
    {
        SharedInstance = this;
    }
    public void SetWheelChairToggle(bool state)
    {
        wheelChairToggle.isOn = state;
    }
    public void OnSaveButton()
    {
        if (ScenesChanger.SharedInstance != null)
        {
            ScenesChanger.SharedInstance.SceneChange(forwardScene);
        }
    }
}
