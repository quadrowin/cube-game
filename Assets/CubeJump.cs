using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour {

    public GameObject mainCube;
    public float minScratch = 0.4f;
    public float scratchSpeed = 0.5f;
    public float jumpAcceleration = 3f;
    public bool active = false;

    private bool animate;

    public void FixedUpdate()
    {
        if (!active)
        {
            return;
        }

        if (animate && mainCube.transform.localScale.y > minScratch)
        {
            scratchCube(-scratchSpeed);
        } else if (mainCube.transform.localScale.y < 1)
        {
            scratchCube(jumpAcceleration * scratchSpeed);
        } else if (mainCube.transform.localScale.y > 1)
        {
            mainCube.transform.localScale = Vector3.one;
        }

        Rigidbody rb = mainCube.GetComponent<Rigidbody>();
        if (mainCube.transform.localPosition.x > 2.5f && rb.velocity.x > 0)
        {
            rb.AddRelativeForce(Mathf.Min(0, -rb.velocity.x * 100), 0, 0);
        } else if (mainCube.transform.localPosition.x < -2.5f && rb.velocity.x < 0)
        {
            rb.AddRelativeForce(Mathf.Max(0, -rb.velocity.x * 100), 0, 0);
        }

        mainCube.transform.rotation = Quaternion.identity;
    }

    public void OnMouseDown()
    {
        if (mainCube.GetComponent<Rigidbody>())
        {
            animate = true;
        }
    }

    public void OnMouseUp()
    {
        animate = false;
        if (mainCube.transform.localScale.y < 1)
        {
            float force = (1 - mainCube.transform.localScale.y) * 1000;
            mainCube.GetComponent<Rigidbody>().AddRelativeForce(force, force, 0);
        }
    }

    void scratchCube(float delta)
    {
        mainCube.transform.localPosition += new Vector3(0, delta * Time.deltaTime, 0);
        mainCube.transform.localScale += new Vector3(0, delta * Time.deltaTime, 0);
    }

}
