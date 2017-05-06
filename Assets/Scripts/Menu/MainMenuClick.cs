using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuClick : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown () {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}
	
	// Update is called once per frame
	void OnMouseUp () {
        transform.localScale = Vector3.one;
    }
}
