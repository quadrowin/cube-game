using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour {

    const string PREFS_SOUND_ENABLED = "sound_enabled";

    private bool soundEnabled = false;

    public AudioListener audioListener;

	// Use this for initialization
	void Start () {
        soundEnabled = PlayerPrefs.GetInt(PREFS_SOUND_ENABLED, -1) != 0;
        transform.GetChild(0).gameObject.SetActive(!soundEnabled);
        audioListener.enabled = soundEnabled;
	}

    void OnMouseUpAsButton()
    {
        soundEnabled = !soundEnabled;
        if (soundEnabled)
        {
            PlayerPrefs.DeleteKey(PREFS_SOUND_ENABLED);
        } else
        {
            PlayerPrefs.SetInt(PREFS_SOUND_ENABLED, 0);
        }
        transform.GetChild(0).gameObject.SetActive(!soundEnabled);
        audioListener.enabled = soundEnabled;
    }

}
