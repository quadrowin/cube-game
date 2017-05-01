using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartClick : MonoBehaviour {
	
	// Update is called once per frame
	void OnMouseUp () {
        SceneManager.LoadScene("MainScene");
	}

}
