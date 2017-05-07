using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour {

    public GameObject[] OtherSpaces;
    public GameObject ShopSpace;
    public GameObject CheeseCountText;
    public CheeseManager CheeseManager;

    private List<GameObject> sourceActive;

    void OnMouseUpAsButton()
    {
        sourceActive = new List<GameObject>();
        foreach (GameObject go in OtherSpaces)
        {
            if (go.activeSelf)
            {
                sourceActive.Add(go);
                go.SetActive(false);
            }
        }
        CheeseCountText.GetComponent<Text>().text = "x " + CheeseManager.GetCheeseScores();
        ShopSpace.SetActive(true);
    }

    public void BackAsItWas()
    {
        ShopSpace.SetActive(false);
        foreach (GameObject go in sourceActive)
        {
            go.SetActive(true);
        }
    }

}
