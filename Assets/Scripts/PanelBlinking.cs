using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBlinking : MonoBehaviour {

    private float startTime;
    private Image componentImage;
    private Outline componentOutline;

    private Color imageFirstColor;
    private Color outlineFirstColor;

    public float Duration = 1f;
    public Color imageSecondColor = Color.white;
    public Color OutlineSecondColor = Color.white;

	void Start () {
        startTime = Time.time;
        componentImage = GetComponent<Image>();
        if (componentImage != null)
        {
            imageFirstColor = componentImage.color;
        }
        componentOutline = GetComponent<Outline>();
        if (componentOutline != null)
        {
            outlineFirstColor = componentOutline.effectColor;
        }
    }
	
	void Update () {
        float timeX = Time.time - startTime;
        float timeK = Mathf.Sin(timeX);
        if (componentImage != null)
        {
            componentImage.color = Color.Lerp(imageFirstColor, imageSecondColor, timeK);
        }
        if (componentOutline != null)
        {
            componentOutline.effectColor = Color.Lerp(outlineFirstColor, OutlineSecondColor, timeK);
        }
    }
}
