using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace setting
{
    public class AdsSetting : MonoBehaviour
    {
        public bool isTest;
        private Dictionary<AdType, RewardedAd> _rewardedAds;

        public string interstitialAdId; // 전면 광고
        public string rewardIdPopularity; // 평판 보상형 광고
        public string rewardIdMoney; // 돈 보상형 광고

        public enum AdType
        {
            Interstitial,
            Popularity,
            Money
        }

        public void Start()
        {
            _rewardedAds = new Dictionary<AdType, RewardedAd>();
            if (isTest)
            {
                interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
                rewardIdPopularity = "ca-app-pub-3940256099942544/5354046379";
                rewardIdMoney = "ca-app-pub-3940256099942544/5354046379";
            }
            else
            {
                interstitialAdId = "";
                rewardIdPopularity = "";
                rewardIdMoney = "";
                // interstitialAdId = "ca-app-pub-2695254421179115/4481823101";
                // rewardIdPopularity = "ca-app-pub-2695254421179115/3892442381";
                // rewardIdMoney = "ca-app-pub-2695254421179115/1920621830";
            }

            // 모바일 광고 SDK를 초기화함.
            MobileAds.Initialize(initStatus => { });

            LoadAll();
        }

        private void LoadAll()
        {
            Debug.Log("LOADING ALL ADS");
            foreach (AdType type in Enum.GetValues(typeof(AdType)))
            {
                LoadRewardedAd(type);
            }
        }

        private void LoadRewardedAd(AdType type) //광고 로드 하기
        {
            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();
            // Get the correct adUnitId based on AdType
            string adUnitId = GetAdUnitId(type);

            // send the request to load the ad.
            RewardedAd.Load(adUnitId, adRequest,
                (ad, error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                    _rewardedAds[type] = ad;
                    RegisterReloadHandler(ad, type);
                });
        }

        private string GetAdUnitId(AdType type)
        {
            return type switch
            {
                AdType.Interstitial => interstitialAdId,
                AdType.Popularity => rewardIdPopularity,
                AdType.Money => rewardIdMoney,
                _ => null
            };
        }

        private void ShowAd(AdType type, float? value = null)
        {
            if (!_rewardedAds.ContainsKey(type) || !_rewardedAds[type].CanShowAd())
            {
                Debug.LogWarning("Ad of type " + type + " is not loaded yet. Trying to load...");
                LoadRewardedAd(type);
                return;
            }

            const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            _rewardedAds[type].Show(reward =>
            {
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                // 여기다가 보상 리스폰스를 해결
            });
        }


        private void RegisterReloadHandler(RewardedAd ad, AdType type)
        {
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded Ad of type " + type + " full screen content closed.");
                LoadRewardedAd(type);
            };

            ad.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError("Rewarded ad of type " + type + " failed to open full screen content with error : " +
                               error);
                LoadRewardedAd(type);
            };
        }
    }
}