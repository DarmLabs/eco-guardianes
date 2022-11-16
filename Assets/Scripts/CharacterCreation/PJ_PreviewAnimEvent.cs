using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_PreviewAnimEvent : MonoBehaviour
{
    public static PJ_PreviewAnimEvent SharedInstance;
    Animator anim;
    [SerializeField] GameObject creator;
    [SerializeField] Transform pjPreview;
    void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StartAnim()
    {
        RestoreRotation();
        anim.Play("MinimizePJ_Preview");
    }
    public void AddScene()
    {
        ScenesChanger.SharedInstance.AddScene("MainMenu");
    }
    public void TurnOffCreator()
    {
        creator.SetActive(false);
    }
    public void RestoreRotation()
    {
        pjPreview.rotation = Quaternion.Euler(0, 180, 0);
    }
}
