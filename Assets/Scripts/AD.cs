using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AD : MonoBehaviour
{
    private InterstitialAd interstitial;

    void Start()
    {
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1215085077559999/3564479460";
        #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1215085077559999/5180813465";
        #endif

        // Initialize an InterstitialAd.

        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void Show()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
}
