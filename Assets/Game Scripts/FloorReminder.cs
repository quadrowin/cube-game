using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReminder : MonoBehaviour {

    private GameObject lastFloor;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "FloorBlock")
        {
            lastFloor = collision.gameObject;
        }
        if (collision.gameObject.name == "Floor(Clone)")
        {
            lastFloor = collision.gameObject;
        }
	}

    public GameObject GetLastFloor()
    {
        return lastFloor;
    }
	
}
