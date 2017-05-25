using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAnimation : MonoBehaviour {

    private float StartTime;
    private Vector3 StartPosition;

    public float StartTimeOffset = 0;
    public float MinAlpha = .3f;
    public float MaxAlpha = .6f;
    public float RotatePeriod = 30;
    public float BlindPeriod = 10;

    public float CenterMinRadius = 1;
    public float CenterMaxRadius = 1;

    // Use this for initialization
    void Start () {
        StartTime = Time.time;
        StartPosition = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        var deltaTime = Time.time - StartTime;
        var radius = CenterMinRadius + (CenterMaxRadius - CenterMinRadius) * Mathf.Sin(deltaTime / RotatePeriod + 2 + StartTimeOffset);
        transform.localRotation = Quaternion.Euler(0, 0, 360f * Mathf.Sin(deltaTime / RotatePeriod));
        transform.localPosition = new Vector3(
            StartPosition.x + radius * Mathf.Sin(deltaTime / RotatePeriod / 2 + StartTimeOffset),
            StartPosition.y + radius * Mathf.Cos(deltaTime / RotatePeriod / 2 + StartTimeOffset),
            StartPosition.z
        );
        var c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, MinAlpha + (MaxAlpha - MinAlpha) * Mathf.Sin(deltaTime / BlindPeriod));
	}
}
