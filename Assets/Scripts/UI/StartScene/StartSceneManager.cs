using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager SharedInstance;
    public float FadeTime { get; set; }
    [SerializeField] GameObject saveSlotsScreen;
    [SerializeField] GameObject logosParent;
    void Awake()
    {
        SharedInstance = this;
    }
    public void ShowNewLogo(GameObject nextLogo)
    {
        if (nextLogo == null)
        {
            saveSlotsScreen.SetActive(true);
        }
        else
        {
            nextLogo.SetActive(true);
        }
    }
    public void FadeIn(GameObject logo)
    {
        Image logoImage = logo.GetComponent<Image>();
        FadeTime += 0.01f;
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, FadeTime);
    }
    public void FadeOut(GameObject logo)
    {
        Image logoImage = logo.GetComponent<Image>();
        FadeTime -= 0.01f;
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, FadeTime);
    }
    public void SkipLogos()
    {
        logosParent.SetActive(false);
        saveSlotsScreen.SetActive(true);
    }
}
