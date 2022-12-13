using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesChanger : MonoBehaviour
{
    public static ScenesChanger SharedInstance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SharedInstance = this;
    }
    public void AddScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName, LoadSceneMode.Additive));
    }
    public void SceneChange(string sceneName)
    {
        if (SaveDataHandler.SharedInstance != null)
        {
            SaveDataHandler.SharedInstance.Save();
        }
        StartCoroutine(LoadAsyncScene(sceneName, LoadSceneMode.Single));
    }
    IEnumerator LoadAsyncScene(string scene, LoadSceneMode loadSceneMode)
    {
        SceneCache.SharedInstance.SetCurrentScene(scene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, loadSceneMode);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
