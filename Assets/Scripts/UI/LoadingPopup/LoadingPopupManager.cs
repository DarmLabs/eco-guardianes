using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPopupManager : MonoBehaviour
{
    public static LoadingPopupManager SharedInstance;
    GameObject loadingPopup;
    void Awake()
    {
        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        loadingPopup = transform.GetChild(0).gameObject;
    }
    public void LoadingPopupSwitcher(bool state)
    {
        loadingPopup.SetActive(state);
    }
}
