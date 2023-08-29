using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Rendering;

public class Admob : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

    bool loadingAd;
    string nextSceneName = ""; 
    private RewardedAd rewardedAd;

    public void Start()
    {
        loadingAd = false;
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }


    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {

                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                ShowRewardedAd();
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        //StartCoroutine(showInterstitial());

        //IEnumerator showInterstitial()
        //{
        //    while(rewardedAd == null)
        //    {
        //        yield return new WaitForSeconds(0.2f);
        //    }
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                SceneManager.LoadScene(nextSceneName);
            });
        }
        //}
    }

    public void showAdmob(string sceneName)
    {
        // 중복 로드 막는거. 어차피 씬을 새로 오픈하는거라 다른 곳에서 false해 줄 필요 없음
        if (!loadingAd)
        {
            nextSceneName = sceneName;
            loadingAd = true;
            LoadRewardedAd();
            return;
        }
    }
}
