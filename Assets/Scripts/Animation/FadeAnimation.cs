using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour {


    public float Duration = 0.7f;
    public bool DisableAfterFadeOut = true;
    public float MinAlpha = 0;
    public float MaxAlpha = 1;

    private float startTime;
    private float startAlpha;
    private bool isFadeIn;

    void Start()
    {
        if (0 == startTime)
        {
            // запускать необходимо через FadeIn() / FadeOut()
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float timeDelta = Time.time - startTime;
        var clr = gameObject.GetComponent<Image>().color;
        if (timeDelta >= Duration)
        {
            startTime = 0;
            if (isFadeIn)
            {
                gameObject.GetComponent<Image>().color = new Color(clr.r, clr.g, clr.b, MaxAlpha);
            }
            else
            {
                if (DisableAfterFadeOut)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.GetComponent<Image>().color = new Color(clr.r, clr.g, clr.b, MinAlpha);
                }
            }
            enabled = false;
            return;
        }
        var targetAlpha = isFadeIn ? MaxAlpha : MinAlpha;
        gameObject.GetComponent<Image>().color = new Color(clr.r, clr.g, clr.b, startAlpha + (targetAlpha - startAlpha) * timeDelta / Duration);
    }

    public void FadeIn()
    {
        if (!gameObject.activeSelf)
        {
            var clr = gameObject.GetComponent<Image>().color;
            gameObject.GetComponent<Image>().color = new Color(clr.r, clr.g, clr.b, MinAlpha);
            gameObject.SetActive(true);
        }
        startFade(true);
    }

    public void FadeOut()
    {
        startFade(false);
    }

    private void startFade(bool fadeIn)
    {
        isFadeIn = fadeIn;
        startAlpha = gameObject.GetComponent<Image>().color.a;
        startTime = Time.time;
        enabled = true;
    }

}
