using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayButton : MonoBehaviour {

    public GameObject cross;

    private static List<GooglePlayButton> instances = new List<GooglePlayButton>();

	// Use this for initialization
	void Start () {
        instances.Add(this);
        UpdateSelfState();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateSelfState()
    {
        cross.SetActive(!Social.localUser.authenticated);
    }

    public static void UpdateState()
    {
        foreach (var ins in instances)
        {
            ins.UpdateSelfState();
        }
    }

}
