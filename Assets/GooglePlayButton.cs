using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayButton : MonoBehaviour {

    public GameObject cross;

	// Use this for initialization
	void Start () {
        cross.enabled = !Social.localUser.authenticated;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
