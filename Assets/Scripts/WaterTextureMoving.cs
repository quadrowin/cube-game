using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTextureMoving : MonoBehaviour {

    public float SpeedX = 0.3f;
    public float SpeedY = 0.1f;

    public bool SinX = false;
    public bool SinY = true;

    private Material mat;
    private float offsetX;
    private float offsetY;
    private float startTime;

    // Use this for initialization
    void Start () {
        mat = GetComponent<MeshRenderer>().material;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (SinX)
        {
            offsetX = Mathf.Sin((Time.time - startTime) * SpeedX);
        } else
        {
            offsetX += Time.deltaTime * SpeedX;
        }
        if (SinY)
        {
            offsetY = Mathf.Sin((Time.time - startTime) * SpeedY);
        }
        else
        {
            offsetY += Time.deltaTime * SpeedY;
        }
        
        mat.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
	}
}
