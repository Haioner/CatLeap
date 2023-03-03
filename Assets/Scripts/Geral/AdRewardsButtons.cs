using UnityEngine;

public class AdRewardsButtons : MonoBehaviour
{
    [SerializeField] private AdsManager adMan;
    [SerializeField] private SaveLastPositions saveLast;

    public void Continue_ADRewards()
    {
        adMan.RewardedAd(saveLast.PlayerContinue_ADRewards);
    }
}