using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkStoreButton : MonoBehaviour
{

    public ShopScreen ShopScreen;

    void OnMouseUpAsButton()
    {
        transform.localScale = Vector3.one;
        ShopScreen.BackScreenAsItWas();
    }

}
