using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinFocus : MonoBehaviour {

    public CubeSkinManager SkinManager;

	public void SelectSkinOf(CubeSkinSelect skin)
    {
        RectTransform rt = skin.GetComponent<RectTransform>();
        if (rt != null)
        {
            GetComponent<RectTransform>().localPosition = rt.localPosition;
            SkinManager.SetSelectedSkin(skin.SkinName);
        }
    }

}
