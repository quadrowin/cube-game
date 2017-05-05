using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGames : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesClientConfiguration cfg = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(cfg);
        PlayGamesPlatform.Activate();

        SignIn();
	}

    void SignIn()
    {
        Social.localUser.Authenticate(success =>
        {
            GooglePlayButton.UpdateState();
        });
    }

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(id, 100, success => { });
        }
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
        }
    }

    public static void ShowAchievemntsUI()
    {
        Social.ShowAchievementsUI();
    }

    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardId, success => { });
        }
    }

    public static void ShowLoeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

}
