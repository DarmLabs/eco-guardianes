using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager SharedInstance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Audio clips")]
    [SerializeField] AudioClip confirmButton;
    public AudioClip ConfirmButton => confirmButton;
    [SerializeField] AudioClip negativeButton;
    public AudioClip NegativeButton => negativeButton;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SharedInstance = this;
    }
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.enabled && clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource.enabled && clip != null)
        {
            sfxSource.clip = clip;
            sfxSource.Play();
        }
    }
    public void ToggleMusic(Toggle toggle)
    {
        musicSource.enabled = toggle.isOn;
        AssignNewAudioType(toggle.GetComponent<AudioToggleHelper>(), toggle.isOn);
    }
    public void ToggleSFX(Toggle toggle)
    {
        sfxSource.enabled = toggle.isOn;
        AssignNewAudioType(toggle.GetComponent<AudioToggleHelper>(), toggle.isOn);
    }
    public void AssignNewAudioType(AudioToggleHelper toggleHelper, bool isOn)
    {
        if (toggleHelper == null)
        {
            Debug.LogError("ERROR: no exitse un Toggle Helper en este GameObject");
            return;
        }
        if (isOn)
        {
            toggleHelper.Type = AudioButtonType.Negative;
        }
        else
        {
            toggleHelper.Type = AudioButtonType.Confirm;
        }
        toggleHelper.PlayOnToggle();
    }
}
