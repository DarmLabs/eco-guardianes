using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AudioButtonType
{
    None,
    Confirm,
    Negative
}
public class AudioButtonHelper : MonoBehaviour
{
    [SerializeField] AudioButtonType type = AudioButtonType.None;
    Button btn;
    void Awake()
    {
        btn = GetComponent<Button>();
    }
    void Start()
    {
        switch (type)
        {
            case AudioButtonType.Confirm:
                AssignConfirm();
                break;
            case AudioButtonType.Negative:
                AssignNegative();
                break;
        }
    }
    public void AssignConfirm()
    {
        btn.onClick.AddListener(delegate { AudioManager.SharedInstance?.PlaySFX(AudioManager.SharedInstance.ConfirmButton); });
    }
    public void AssignNegative()
    {
        btn.onClick.AddListener(delegate { AudioManager.SharedInstance?.PlaySFX(AudioManager.SharedInstance.NegativeButton); });
    }
}
