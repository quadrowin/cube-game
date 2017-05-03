using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseEating : MonoBehaviour {

    public CubeJump CubeJumper;

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter " + other.name);
        if (other.name == "Cheese(Clone)")
        {
            other.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<Animation>().Play();
            Destroy(other.gameObject, 1);
            CubeJumper.OnCheeseTake();
        }
    }

}
