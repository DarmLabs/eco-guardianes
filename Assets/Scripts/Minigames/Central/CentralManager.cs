using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public static CentralManager SharedInstance;
    [SerializeField] LightFeedback badLight, goodLight;
    [SerializeField] Spawner spawner;
    public DraggingObject DraggingObject { get; set; }
    public Separator Separator { get; set; }
    int score;
    public int Score => score;
    int lives;
    public int Lives => lives;
    int objectsRePooled;
    [SerializeField] int maxLives;
    void Awake()
    {
        SharedInstance = this;
        lives = maxLives;
    }
    void Start()
    {
        CentralManagerUI.SharedInstance.UpdateLives();
    }
    public void CheckForObjectThrow()
    {
        if (Separator != null && DraggingObject != null)
        {
            //realease over separator
            CheckForTypes();
            Separator.DetectorExit();
        }
        else if (Separator == null && DraggingObject != null)
        {
            //release over air
            DraggingObject.ResetToPoint();
        }
    }
    void CheckForTypes()
    {
        objectsRePooled++;
        if (DraggingObject.Type == Separator.Type)
        {
            goodLight.GiveFeedback();
            score++;
            CheckScore();
        }
        else
        {
            BadFeedback();
        }
        DraggingObject.ResetToPool();
        DraggingObject.Spawned = false;
    }
    public void BadFeedback()
    {
        badLight.GiveFeedback();
        lives--;
        CheckLives();
    }
    void CheckScore()
    {
        CentralManagerUI.SharedInstance.UpdateScore();
        if (objectsRePooled == spawner.ResiduosInitialCount && spawner.FinishedSpawn)
        {
            CentralManagerUI.SharedInstance.ActivateEndPanel();
            SaveDataHandler.SharedInstance.SaveCentral(true);
        }
    }
    void CheckLives()
    {
        CentralManagerUI.SharedInstance.UpdateLives();
        if (lives == 0)
        {
            CentralManagerUI.SharedInstance.ActivateEndPanel();
        }
    }
}
