using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdsController : MonoBehaviour
{
    private RewardedAd rewardAD;
    public string rewardAdNumber;
    public static int adCounter = 0;
    string rewardAdID;
    [SerializeField] Text healthText;
    [SerializeField] GameObject warningPanel;
    public Button healthJokerButton;
    private void Start()
    {
        requestRewardAd();
    }

    void requestRewardAd()
    {
#if UNITY_ANDROID
        rewardAdID = rewardAdNumber;
#else
                    adID = "Unkown Platform";
#endif

        rewardAD = new RewardedAd(rewardAdID);

        rewardAD.OnAdLoaded += isloaded;
        rewardAD.OnAdOpening += open;
        rewardAD.OnAdFailedToShow += isopen;
        rewardAD.OnUserEarnedReward += earnedReward;
        rewardAD.OnAdClosed += close;

        AdRequest request = new AdRequest.Builder().Build();
        rewardAD.LoadAd(request);
    }

    IEnumerator Warning(){
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        healthJokerButton.interactable = false;
        warningPanel.SetActive(false);
    }
    public void ResetAdsCounter(){
        adCounter = 0;
        healthJokerButton.interactable = true;
    }
    public void rewardAdShow()
    {
        adCounter++;
        if (adCounter < 2)
        {
            if (rewardAD.IsLoaded())
            {
                rewardAD.Show();
            }
            else
            {
                requestRewardAd();
            }
        }else
        {
            StartCoroutine(Warning());
        }

    }

    public void isloaded(object sender, EventArgs args)
    {
        Debug.Log("ad loaded\n");
    }
    public void wrongisloaded(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("a\n");
        requestRewardAd();
    }
    public void open(object sender, EventArgs args)
    {
        Debug.Log("Reklam opened\n");
    }
    public void close(object sender, EventArgs args)
    {
        Debug.Log("ad closed\n");
    }
    public void isopen(object sender, AdErrorEventArgs args)
    {
        Debug.Log("ad don't open\n");
    }
    public void earnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        QuestionController.health++;
        healthText.text = QuestionController.health.ToString();
        requestRewardAd();
    }
}