using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinOption : MonoBehaviour {

    public string SkinName;
    public string SkinTitle;
    public int SkinCost = 10;
    public CubeSkinFocus FocusHandler;

    private GameObject Padlock;

    void OnMouseUpAsButton()
    {
        FocusHandler.SelectSkinOf(this);
    }

    public void SetOpened(bool opened)
    {
        var padlock = GetPadlock();
        if (padlock != null)
        {
            padlock.SetActive(!opened);
        }
    }

    GameObject GetPadlock()
    {
        if (Padlock == null)
        {
            foreach (Transform tr in transform)
            {
                if (tr.tag == Tags.PADLOCK_CHEESE)
                {
                    Padlock = tr.gameObject;
                }
            }
        }
        return Padlock;
    }

    public string GetSkinTitle()
    {
        if (SkinTitle == "")
        {
            return "No skin";
        }
        return SkinTitle;
    }

}
