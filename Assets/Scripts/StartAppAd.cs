using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StartApp;

public class StartAppAd : MonoBehaviour {

	void Start () {
        #if UNITY_ANDROID
            StartAppWrapper.init();
            StartAppWrapper.loadAd();
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
            var videoListener = new VideoListenerImplementation();
            StartAppWrapper.setVideoListener(videoListener);
            StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO);
        #endif
    }

    public void PlayVideoWithReward()
    {
        #if UNITY_ANDROID
            StartAppWrapper.showAd();
            StartAppWrapper.loadAd(StartAppWrapper.AdMode.REWARDED_VIDEO);
        #endif
    }

    public void ShowFullscreenAd()
    {
        #if UNITY_ANDROID
            StartAppWrapper.showAd();
            StartAppWrapper.loadAd();
        #endif
    }

}
