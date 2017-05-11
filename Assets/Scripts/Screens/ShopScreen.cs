using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour {

    /// <summary>
    /// Объекты становятся активными при переходе на экран
    /// </summary>
    public GameObject[] ObjectsActivate;

    /// <summary>
    /// Объекты, которые нужно скрыть при переходе на экран магазина
    /// </summary>
    public GameObject[] ObjectsDisable;

    public GameObject CheeseCountText;
    public CheeseManager CheeseManager;
    public CubeSkinManager SkinManager;
    public GameObject SkinsOptionsOwner;
    public GameObject SkinSelectFrame;

    private List<GameObject> sourceActive;

    public void ActivateScreen()
    {
        transform.localScale = Vector3.one;
        sourceActive = new List<GameObject>();
        foreach (GameObject go in ObjectsDisable)
        {
            if (go.activeSelf)
            {
                sourceActive.Add(go);
                go.SetActive(false);
            }
        }
        UpdateCheeseCount();

        foreach (GameObject go in ObjectsActivate)
        {
            go.SetActive(true);
        }

        var skinName = SkinManager.GetSelectedSkinName();
        var options = SkinsOptionsOwner.GetComponentsInChildren<CubeSkinOption>(true);
        CubeSkinOption activeSkin = SkinManager.GetSelectedSkinTpl();
        foreach (var skin in options)
        {
            skin.SetOpened(SkinManager.IsSkinOpened(skin.SkinName));
            if (skin.SkinName == skinName)
            {
                activeSkin = skin;
            }
        }

        SkinSelectFrame.GetComponent<CubeSkinFocus>().SelectSkinOf(activeSkin);
    }

    public void BackScreenAsItWas()
    {
        foreach (GameObject go in ObjectsActivate)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sourceActive)
        {
            go.SetActive(true);
        }
    }

    public void UpdateCheeseCount()
    {
        CheeseCountText.GetComponent<Text>().text = "x " + CheeseManager.GetCheeseScores();
    }

}
