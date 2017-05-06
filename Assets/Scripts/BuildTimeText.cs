using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTimeText : MonoBehaviour {

    public string buildTime = "2017-05-06";

    public string buildVersion = "10001";

    void Start () {
        UpdateText();
	}

    public void UpdateText()
    {
        GetComponent<Text>().text = "Build " + buildTime + " / " + buildVersion;
    }

}
