using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    StartSceneManager sceneManager;
    [SerializeField] GameObject nextLogo;
    bool fadeState = true;
    void Start()
    {
        sceneManager = StartSceneManager.SharedInstance;
    }
    void Update()
    {
        if (fadeState)
        {
            sceneManager.FadeIn(gameObject);
            if (sceneManager.FadeTime >= 1)
            {
                StartCoroutine(WaitForFadeOut(1.5f));
            }
        }
        if (!fadeState)
        {
            sceneManager.FadeOut(gameObject);
            if (sceneManager.FadeTime <= 0)
            {
                sceneManager.FadeTime = 0;
                gameObject.SetActive(false);
                sceneManager.ShowNewLogo(nextLogo);
            }
        }
        IEnumerator WaitForFadeOut(float secs)
        {
            yield return new WaitForSecondsRealtime(secs);
            fadeState = false;
        }
    }
}
