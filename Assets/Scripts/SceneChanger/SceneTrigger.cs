using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public static SceneTrigger SharedInstance;
    [SerializeField] GameObject achievementsManager;
    void Awake()
    {
        SharedInstance = this;
    }
    public void EnableManager()
    {
        achievementsManager.SetActive(true);
        Destroy(this);
    }
}
