using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForChangeScene(1f));
    }
    IEnumerator WaitForChangeScene(float secs)
    {
        yield return new WaitForSeconds(secs);
        ScenesChanger.SharedInstance?.SceneChange(ConstManager.mainMenuSceneName);
    }
}
