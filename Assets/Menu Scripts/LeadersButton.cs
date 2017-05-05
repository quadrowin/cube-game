using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadersButton : MonoBehaviour {

    void OnMouseUpAsButton()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        } else
        {
            print("localUser not authenticated");
        }
    }

}
