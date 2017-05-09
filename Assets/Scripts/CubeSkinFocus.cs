using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSkinFocus : MonoBehaviour {

    public CubeSkinManager SkinManager;
    public GameObject OpenedSkinTitle;
    public GameObject ClosedSkinTitle;

    public GameObject OpenedInfo;
    public GameObject ClosedInfo;

    private CubeSkinOption focusedSkin;

    public CubeSkinOption GetFocusedSkin()
    {
        return focusedSkin;
    }

    public void SelectSkinOf(CubeSkinOption skin)
    {
        focusedSkin = skin;
        RectTransform rt = skin.GetComponent<RectTransform>();
        if (rt != null)
        {
            GetComponent<RectTransform>().localPosition = rt.localPosition;
            ClosedSkinTitle.GetComponent<Text>().text = skin.GetSkinTitle();
            if (SkinManager.IsSkinOpened(skin.SkinName))
            {
                // Скин доступен
                SkinManager.SetSelectedSkin(skin.SkinName);
                OpenedSkinTitle.GetComponent<Text>().text = skin.GetSkinTitle();
                OpenedInfo.SetActive(true);
                ClosedInfo.SetActive(false);
            }
            else
            {
                // предлагаем купить
                ClosedSkinTitle.GetComponent<Text>().text = skin.GetSkinTitle();
                OpenedInfo.SetActive(false);
                ClosedInfo.SetActive(true);
            }
            
        }
    }

}
