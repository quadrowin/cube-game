using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StartApp;

public class CheeseVideoButton : MonoBehaviour {

    private int CheeseReward = 10;

    public CheeseManager CheeseManager;
    public GameObject CheeseCountText;
    public StartAppAd StartApp;

    private class VideoListener : StartAppWrapper.VideoListener
    {

        private CheeseVideoButton owner;

        public VideoListener(CheeseVideoButton owner) {
            this.owner = owner;
        }

        public void onVideoCompleted()
        {
            owner.OnVideoCompleted();
        }

    }

    void OnMouseUpAsButton() {
        StartApp.PlayVideoWithReward(new VideoListener(this));
	}

    void OnVideoCompleted()
    {
        CheeseManager.CheeseIncrement(CheeseReward);
        CheeseCountText.GetComponent<Text>().text = "x " + CheeseManager.GetCheeseScores();
    }

}
