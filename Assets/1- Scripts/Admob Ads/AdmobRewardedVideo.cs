using GoogleMobileAds.Api;
using System;
using UnityEngine.UI; 
using UnityEngine;
using GoogleMobileAds.Sample;
using UnityEngine.SceneManagement;
public class AdmobRewardedVideo : MonoBehaviour
{
    public static AdmobRewardedVideo Instance;

    public int Index;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #region Give Reward

    public void RewardAfterAd()
    {
        if (Index == 0)
        {
            DataManager.NumberOfArrows += 5;
            if (MissionManager.Instance)
            {
                MissionManager.Instance.TotalArrows = DataManager.NumberOfArrows;
                MissionManager.Instance.RewardPanel.SetActive(true);
            }
            if (BowController.instance)
            {
                BowController.instance.GetComponent<RectTransform>().rotation = Quaternion.identity;
                print("Name of bowcontroller "+ BowController.instance.transform.name);
                print("rotation of bowcontroller " + BowController.instance.GetComponent<RectTransform>().rotation);

             }
        }     
    }
    #endregion
    public void ShowRewardedVideo()
    {
        if (Adsmanager.Instance)
            Adsmanager.Instance.ShowRewardedVideoAd();
    }
    public void Show_RewardedInterstitial_Video()
    {
        if (Adsmanager.Instance)
            Adsmanager.Instance.ShowRewardedVideoAd();
    }
}