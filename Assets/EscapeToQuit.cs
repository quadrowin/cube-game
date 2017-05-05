using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToQuit : MonoBehaviour {

    private bool escapeUp = false;
    private float previousEscape = 0;

    private float doublePressTime = 2f;

	void Update () {
        if (Input.touchCount > 1) {
            previousEscape = 0;
            return;
        }
        if (!Input.anyKey)
        {
            escapeUp = true;
            return;
        }
        if (!Input.GetKey(KeyCode.Escape))
        {
            previousEscape = 0;
            return;
        }
        if (previousEscape > 0)
        {
            if (escapeUp && Time.time - previousEscape < doublePressTime)
            {
                print("ESC: Application.Quit");
                Application.Quit();
            }
            return;
        }
        escapeUp = false;
        previousEscape = Time.time;
        print("Press ESC one more time to Quit");
    }
}
