using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFeedback : MonoBehaviour
{
    [SerializeField] GameObject _light;
    public void GiveFeedback()
    {
        StartCoroutine(GiveFeedbackCr());
    }
    IEnumerator GiveFeedbackCr()
    {
        SwitchOwnLight(true);
        yield return new WaitForSeconds(0.5f);
        SwitchOwnLight(false);
    }

    void SwitchOwnLight(bool state)
    {
        _light.SetActive(state);
    }
}
