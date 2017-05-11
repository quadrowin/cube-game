using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarAnimation : MonoBehaviour {


    private Vector3 RotVelocity;
    private Vector3 XyzVelocity;
    private float StartTime;

    public float MaxSpeedX = 2f;
    public float MaxSpeedY = 2f;
    public float LiveTime = 5f;

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

        DestroyObject(this, LiveTime);
    }

	void Update () {
        var deltaTime = Time.time - StartTime;
        var mat = GetComponent<MeshRenderer>().material;
        var clr = mat.color;
        mat.color = new Color(clr.r, clr.g, clr.b, Mathf.Sin(deltaTime));
        transform.localPosition += XyzVelocity * Time.deltaTime;
        transform.Rotate(RotVelocity * Time.deltaTime * 10);
    }
}
