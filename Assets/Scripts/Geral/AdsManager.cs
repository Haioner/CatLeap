using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    string gameId = "5178565";
#else
    string gameId = "5178564";
#endif

    [HideInInspector] public UnityEvent unityEvent;
    public delegate void MyDelegate();

    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    public void PlayAd()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
    }

    public void RewardedAd(MyDelegate methodToAdd)
    {
        unityEvent.RemoveAllListeners();
        unityEvent.AddListener(() => methodToAdd());
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else
            Debug.Log("Rewarded ad is not ready!");
    }

    //Unity ads Interface
    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("ADS ARE READY!");
    }

    public void OnUnityAdsDidError(string message)
    {
        //Debug.Log("ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log("VIDEO STARTED!");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            unityEvent.Invoke();
        }
    }
}
