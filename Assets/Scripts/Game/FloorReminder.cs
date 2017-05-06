using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReminder : MonoBehaviour {

    private GameObject lastFloor;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == Tags.FLOOR)
        {
            lastFloor = collision.gameObject;
        }
	}

    public GameObject GetLastFloor()
    {
        return lastFloor;
    }
	
}
