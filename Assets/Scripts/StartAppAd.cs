﻿using System.Collections;
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

#if UNITY_ANDROID
    public class VideoListenerImplementation : StartAppWrapper.VideoListener {
        public void onVideoCompleted() {
            // Grant user with the reward
        }
    }
#endif

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

    public void PlayVideoWithReward()
    {
#if UNITY_ANDROID
        if (inited)
        {
            StartAppWrapper.showAd();
            StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO);
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
