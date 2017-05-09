using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinOption : MonoBehaviour {

    public string SkinName;
    public string SkinTitle;
    public int SkinCost = 10;
    public CubeSkinFocus FocusHandler;

    private GameObject Padlock;

    private void Start()
    {
        foreach (Transform tr in transform)
        {
            if (tr.tag == Tags.PADLOCK_CHEESE)
            {
                Padlock = tr.gameObject;
            }
        }
    }

    void OnMouseUpAsButton()
    {
        FocusHandler.SelectSkinOf(this);
    }

    public void SetOpened(bool opened)
    {
        if (Padlock != null)
        {
            Padlock.SetActive(!opened);
        }
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
