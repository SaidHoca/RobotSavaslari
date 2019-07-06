using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;


public class RewardedManager : MonoBehaviour
{
    private RewardBasedVideoAd rAd;

    public string id = "";

    public Button rewardButon;

    private void Start()
    {
        rewardButon.interactable = false;

        rAd = RewardBasedVideoAd.Instance;

        rAd.OnAdRewarded += VideoRewarded; // reklamımız izlendiğinde videorewarded isminde fonksiyonu çağırır

        rAd.OnAdClosed += VideoClosed; // reklam kapandığında yine yeni bir reklam hazırlamak için..

        this.RequestAds();
    }

    private void RequestAds()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.rAd.LoadAd(request, id);
    }

    private void VideoRewarded(object sender, EventArgs e)
    {
        Reward(); // ödül verecek olan fonksiyon..
    }

    private void VideoClosed(object sender, EventArgs e)
    {
        RequestAds();
    }

    public void ShowAds()
    {
        this.rAd.Show();
    }

    public void Reward()
    {
        Player.instance.ParaGuncelle(Player.instance.id);
        rewardButon.interactable = false;
    }

    private void Update()
    {
        if (rAd.IsLoaded())
        {
            rewardButon.interactable = true;
        }
        else
        {
            rewardButon.interactable = false;
        }
    }

}
