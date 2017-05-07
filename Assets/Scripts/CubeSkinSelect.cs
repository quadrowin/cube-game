using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinSelect : MonoBehaviour {

    public string SkinName;
    public CubeSkinFocus FocusHandler;

    void OnMouseUpAsButton()
    {
        FocusHandler.SelectSkinOf(this);
    }

}
