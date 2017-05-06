using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsButton : MonoBehaviour {

    void OnMouseUpAsButton()
    {
        if (Social.localUser.authenticated)
        {
            PlayGames.ShowAchievemntsUI();
        }
        else
        {
            print("localUser not authenticated");
        }
    }

}
