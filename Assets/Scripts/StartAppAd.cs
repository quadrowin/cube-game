using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StartApp;
using System;

public class StartAppAd : MonoBehaviour {

    private bool inited = false;

    void Start () {
#if UNITY_ANDROID
        try
        {
            StartAppWrapper.init();
            StartAppWrapper.loadAd();
            inited = true;
        } catch (Exception) {
            inited = false;
        }
#endif
    }

    public class VideoListenerImplementation : StartAppWrapper.VideoListener {
        public void onVideoCompleted() {
            // Grant user with the reward
        }
    }

    public class AdEventListenerImplementation: StartAppWrapper.AdEventListener
    {
        public void onReceiveAd()
        {
            StartAppWrapper.showAd();
        }
        public void onFailedToReceiveAd()
        {

        }
    }

    void SetVideoListener()
    {
#if UNITY_ANDROID
        if (inited)
        {
            var videoListener = new VideoListenerImplementation();
            StartAppWrapper.setVideoListener(videoListener);
            StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO);
        }
#endif
    }

    public void PlayVideoWithReward(StartAppWrapper.VideoListener videoListener)
    {
#if UNITY_ANDROID
        if (inited)
        {
            StartAppWrapper.setVideoListener(videoListener);
            StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO, new AdEventListenerImplementation());
        }
#endif
    }

    public void ShowFullscreenAd()
    {
#if UNITY_ANDROID
        if (inited)
        {
            StartAppWrapper.showAd();
            StartAppWrapper.loadAd();
        }
#endif
    }

}
