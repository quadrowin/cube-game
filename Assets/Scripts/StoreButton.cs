using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour {

    public ShopScreen ShopScreen;

    void OnMouseUpAsButton()
    {
        transform.localScale = Vector3.one;
        ShopScreen.ActivateScreen();
    }

}
