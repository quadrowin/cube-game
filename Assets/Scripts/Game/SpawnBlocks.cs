using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBlocks : MonoBehaviour {

    public GameObject gameSpace;
    public GameObject block;
    public GameObject cheese;
    public Vector3 zeroPosition = new Vector3(3, -9, 0);

    private GameObject blockInst;
    private GameObject cheeseInst;
    private float spawnStartTime;
    private float spawnDuration = 1;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start () {
        SpawnNewBlock();
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnStartTime <= 0)
        {
            return;
        }
        float timeDelta = Time.fixedTime - spawnStartTime;
        if (timeDelta >= spawnDuration)
        {
            spawnStartTime = 0;
            blockInst.transform.localPosition = targetPosition;
            UpdateCheesePosition();
            return;
        }
        blockInst.transform.localPosition = Vector3.Lerp(zeroPosition, targetPosition, timeDelta / spawnDuration);
        UpdateCheesePosition();
    }

    public GameObject SpawnNewBlock()
    {
        
        blockInst = Instantiate(block, gameSpace.transform) as GameObject;
        blockInst.transform.localPosition = zeroPosition;
        blockInst.transform.localRotation = Quaternion.identity;
        var front = blockInst.transform.Find("Front");
        front.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(-Random.Range(1, 100) / 100f, 0));
        targetPosition = new Vector3(Random.Range(1, 3), Random.Range(-2, 0), 0);
        UpdateCheesePosition();
        cheeseInst = Instantiate(cheese, blockInst.transform.parent) as GameObject;
        
        spawnStartTime = Time.fixedTime;
        return blockInst;
    }

    void UpdateCheesePosition()
    {
        if (cheeseInst == null)
        {
            return;
        }
        cheeseInst.transform.localPosition = new Vector3(
            blockInst.transform.localPosition.x,
            blockInst.transform.localPosition.y + 1,
            blockInst.transform.localPosition.z
        );
        cheeseInst.transform.localRotation = blockInst.transform.localRotation;
    }

}
