using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToBack : MonoBehaviour {

    private bool EscapeDown = false;

    public ShopScreen ShopScreen;

    void Update()
    {
        if (Input.touchCount > 1)
        {
            EscapeDown = false;
            return;
        }
        if (!Input.anyKey && EscapeDown)
        {
            // выход
            EscapeDown = false;
            ShopScreen.BackScreenAsItWas();
            return;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            EscapeDown = true;
            return;
        }
    }
}
