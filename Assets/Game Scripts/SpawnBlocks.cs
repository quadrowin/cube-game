using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour {

    public GameObject gameSpace;
    public GameObject block;
    private GameObject blockInst;
    private float spawnStartTime;
    private float spawnDuration = 1;
    private Vector3 targetPosition;
    private Vector3 zeroPosition;

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
            blockInst.transform.position = targetPosition;
            return;
        }
        blockInst.transform.position = Vector3.Lerp(zeroPosition, targetPosition, timeDelta / spawnDuration);
    }

    public GameObject SpawnNewBlock()
    {
        zeroPosition = new Vector3(3, -9, 0);
        blockInst = Instantiate(block, zeroPosition, Quaternion.identity) as GameObject;
        blockInst.transform.SetParent(gameSpace.transform);
        targetPosition = new Vector3(Random.Range(1, 3), Random.Range(-2, 0), 0);
        spawnStartTime = Time.fixedTime;
        return blockInst;
    }

}
