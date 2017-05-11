using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStars : MonoBehaviour {

    public GameObject Star;

	// Use this for initialization
	void Start () {
        StartCoroutine(spawn());
    }
	
	// Update is called once per frame
	IEnumerator spawn () {
		while (true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(
                Random.Range(0, Screen.width),
                Random.Range(0, Screen.height),
                Camera.main.farClipPlane / 2
            ));
            var newStar = Instantiate(Star);
            newStar.transform.localPosition = pos;
            yield return new WaitForSeconds(5.01f);
        }
	}
}
