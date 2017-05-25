using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarAnimation : MonoBehaviour {

    private Vector3 RotVelocity;
    private Vector3 XyzVelocity;
    private float StartTime;
    private float ScaleHalf;

    public float MaxSpeedX = 2f;
    public float MaxSpeedY = 2f;

    void Start () {
        StartTime = Time.time;
        XyzVelocity = new Vector3(
            Random.Range(-MaxSpeedX, MaxSpeedX),
            Random.Range(-MaxSpeedY, MaxSpeedY),
            Random.Range(-1, 1)
        );
        RotVelocity = new Vector3(
            Random.Range(-2, 2),
            Random.Range(-2, 2),
            Random.Range(-2, 2)
        );
        ScaleHalf = transform.localScale.x / 2;
    }

	void Update () {
        var deltaTime = Time.time - StartTime;
        var timeSin = Mathf.Sin(deltaTime);
        var mat = GetComponent<MeshRenderer>().material;
        var clr = mat.color;
        var scale = ScaleHalf * (1 + timeSin);
        mat.color = new Color(clr.r, clr.g, clr.b, Mathf.Sin(deltaTime));
        transform.localPosition += XyzVelocity * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.Rotate(RotVelocity * Time.deltaTime * 10);
    }
}
