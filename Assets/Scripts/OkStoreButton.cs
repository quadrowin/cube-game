using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkStoreButton : MonoBehaviour
{

    public StoreButton StoreButton;

    void OnMouseUpAsButton()
    {
        StoreButton.BackAsItWas();
    }

}
