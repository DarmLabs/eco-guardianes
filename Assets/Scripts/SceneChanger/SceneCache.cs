using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCache : MonoBehaviour
{
    public static SceneCache SharedInstance;
    public string previousScene { get; private set; }
    public string currentScene { get; private set; }
    void Awake()
    {
        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void SetCurrentScene(string currentScene)
    {
        previousScene = this.currentScene;
        this.currentScene = currentScene;
    }
}
