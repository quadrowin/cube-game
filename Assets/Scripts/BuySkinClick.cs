using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkinClick : MonoBehaviour {

    public CubeSkinFocus SkinFocus;
    public CubeSkinManager SkinManager;
    public CheeseManager CheeseManager;
    public ShopScreen ShopScreen;

    public GameObject DebugText;

    void OnMouseUpAsButton()
    {
        var skin = SkinFocus.GetFocusedSkin();
        if (skin == null) {
            print("Skin not focused");
            return;
        }
        if (CheeseManager.GetCheeseScores() < skin.SkinCost)
        {
            print("Not enough cheese");
            return;
        }
        print("Buy started OK");
        CheeseManager.CheeseDecrement(skin.SkinCost);
        SkinManager.OpenSkin(skin);
        ShopScreen.UpdateCheeseCount();
        SkinFocus.SelectSkinOf(skin);

        if (DebugText)
        {
            DebugText.GetComponent<Text>().text = "Buy finished OK";
        }
    }

}
