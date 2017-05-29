using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAnimation : MonoBehaviour {

    private Vector3 StartPosition;

    public float MinAlpha = .3f;
    public float MaxAlpha = .6f;
    public float RotatePeriod = 30;
    public float BlindPeriod = 10;

    private float StartTimeOffset = 0;
    private float BlindTimeOffset = 0;

    public float CenterMinRadius = 1;
    public float CenterMaxRadius = 1;

    // Use this for initialization
    void Start () {
        StartPosition = Vector3.zero;

        StartTimeOffset = transform.GetSiblingIndex() * 20;
        BlindTimeOffset = transform.GetSiblingIndex() * 30;
    }
	
	// Update is called once per frame
	void Update () {
        var deltaTime = Time.time;
        var radius = CenterMinRadius + (CenterMaxRadius - CenterMinRadius) * (1 + Mathf.Sin(deltaTime / RotatePeriod + 2 + StartTimeOffset)) / 2;
        transform.localRotation = Quaternion.Euler(0, 0, 360f * Mathf.Sin(deltaTime / RotatePeriod));
        transform.localPosition = new Vector3(
            StartPosition.x + radius * Mathf.Sin(deltaTime / RotatePeriod / 2 + StartTimeOffset),
            StartPosition.y + radius * Mathf.Cos(deltaTime / RotatePeriod / 2 + StartTimeOffset),
            StartPosition.z
        );
        var c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, MinAlpha + (MaxAlpha - MinAlpha) * (1 + Mathf.Sin(deltaTime / BlindPeriod + BlindTimeOffset)) / 2) ;
	}
}
