using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReminder : MonoBehaviour {

    private GameObject lastFloor;

    public AudioSource collisionSound;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == Tags.FLOOR)
        {
            lastFloor = collision.gameObject;
            collisionSound.Play();
        }
	}

    public GameObject GetLastFloor()
    {
        return lastFloor;
    }
	
}
