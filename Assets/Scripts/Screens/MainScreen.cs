using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour {

    public GameObject[] StartEnabled;
    public GameObject[] StartDisabled;

	void Start () {
		foreach (var go in StartEnabled)
        {
            go.SetActive(true);
        }
        foreach (var go in StartDisabled)
        {
            go.SetActive(false);
        }
    }

}
