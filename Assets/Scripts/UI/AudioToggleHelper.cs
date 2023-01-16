using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioToggleHelper : MonoBehaviour
{
    [SerializeField] bool isMusicToggle = false;
    [SerializeField] bool isSfxToggle = false;
    AudioButtonType type = AudioButtonType.Negative;
    public AudioButtonType Type
    {
        get { return type; }
        set { type = value; }
    }
    Toggle toggle;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    void Start()
    {
        if (isMusicToggle)
        {
            toggle.onValueChanged.AddListener(delegate { AudioManager.SharedInstance?.ToggleMusic(toggle); });
        }
        if (isSfxToggle)
        {
            toggle.onValueChanged.AddListener(delegate { AudioManager.SharedInstance?.ToggleSFX(toggle); });
        }
        PlayOnToggle();
    }
    public void PlayOnToggle()
    {
        AudioClip clip = null;
        switch (type)
        {
            case AudioButtonType.Confirm:
                clip = AudioManager.SharedInstance?.ConfirmButton;
                break;
            case AudioButtonType.Negative:
                clip = AudioManager.SharedInstance?.NegativeButton;
                break;
        }
        if (clip != null)
        {
            toggle.onValueChanged.AddListener(delegate { AudioManager.SharedInstance?.PlaySFX(clip); });
        }
    }
}
