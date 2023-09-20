using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DisableUntil : MonoBehaviour
{
    private InteractablePerson interactablePerson;
    [SerializeField][Range(2, 5)] int achievementId;
    void Start()
    {
        interactablePerson = GetComponent<InteractablePerson>();
        if (!SaveDataHandler.SharedInstance.LoadCentalData())
        {
            interactablePerson.enabled = false;
        }
    }
    public void EarnAchievement()
    {
        AchievementsManager.SharedInstance.AchievementEarned(achievementId);
    }
}
