using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour {

    const int STATE_NONE = 0;
    const int STATE_SCRATCHING = 1;

    const int STATE_JUMPING = 3;
    const int STATE_REINIT = 4;

    public bool active = false;
    public GameObject mainCube;
    /// <summary>
    /// Пол в момент начала прыжка
    /// </summary>
    private GameObject startFloor;
    public float minScratch = 0.4f;
    public float scratchSpeed = 0.5f;
    public float jumpAcceleration = 3f;
    public float reinitDuration = 0.5f;
    public float reinitStartTime = 0;
    public float reinitDeltaX = 0;

    private int state = STATE_NONE;

    // like values in TapToPlay
    private Vector3 removeFloorPosition = new Vector3(-7, 11, 0);


    public void FixedUpdate()
    {
        if (!active || state == STATE_NONE)
        {
            return;
        }

        if (state == STATE_SCRATCHING && mainCube.transform.localScale.y > minScratch)
        {
            scratchCube(-scratchSpeed);
            return;
        }

        Rigidbody rb = mainCube.GetComponent<Rigidbody>();

        if (mainCube.transform.localPosition.y < -5f)
        {
            // death
            active = false;
            state = STATE_NONE;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            return;
        }

        if (mainCube.transform.localScale.y < 1)
        {
            scratchCube(jumpAcceleration * scratchSpeed);
        }
        else if (mainCube.transform.localScale.y > 1)
        {
            mainCube.transform.localScale = Vector3.one;
        }


        if (mainCube.transform.localPosition.x > 2.5f && rb.velocity.x > 0)
        {
            rb.AddRelativeForce(Mathf.Min(0, -rb.velocity.x * 100), 0, 0);
        }
        else if (mainCube.transform.localPosition.x < -2.5f && rb.velocity.x < 0)
        {
            rb.AddRelativeForce(Mathf.Max(0, -rb.velocity.x * 100), 0, 0);
        }

        if (state == STATE_JUMPING && mainCube.GetComponent<Rigidbody>().velocity.sqrMagnitude < 0.00001f)
        {
            mainCube.transform.rotation = Quaternion.identity;
            reinitStartTime = Time.fixedTime;
            state = STATE_REINIT;
            reinitDeltaX = mainCube.GetComponent<FloorReminder>().GetLastFloor().transform.localPosition.x - mainCube.transform.localPosition.x;
            return;
        }

        if (state == STATE_REINIT)
        {
            GameObject currentFloor = mainCube.GetComponent<FloorReminder>().GetLastFloor();
            if (currentFloor == startFloor)
            {
                // блок не изменился, не было перепрыгивания на следующий
                state = STATE_NONE;
                return;
            }
            // move to start position

            float timeDelta = (Time.fixedTime - reinitStartTime) / reinitDuration;
            startFloor.transform.localPosition = Vector3.Lerp(startFloor.transform.localPosition, removeFloorPosition, timeDelta);
            GetComponent<TapToPlay>().reinitUpdate(timeDelta, reinitDeltaX);
            if (timeDelta >= reinitDuration)
            {
                state = STATE_NONE;
                GetComponent<SpawnBlocks>().SpawnNewBlock();
                return;
            }
        }
    }

    public void OnMouseDown()
    {
        if (state != STATE_NONE || !active)
        {
            return;
        }
        if (mainCube.GetComponent<Rigidbody>().velocity.sqrMagnitude < 0.00001f)
        {
            startFloor = mainCube.GetComponent<FloorReminder>().GetLastFloor();
            state = STATE_SCRATCHING;
        }
    }

    public void OnMouseUp()
    {
        if (state == STATE_SCRATCHING && mainCube.transform.localScale.y < 1)
        {
            state = STATE_JUMPING;
            float force = (1 - mainCube.transform.localScale.y) * 1000;
            mainCube.GetComponent<Rigidbody>().AddRelativeForce(force, force, 0);
            GetComponent<SpawnBlocks>().enabled = true;
        }
    }

    void scratchCube(float delta)
    {
        mainCube.transform.localPosition += new Vector3(0, delta * Time.deltaTime, 0);
        mainCube.transform.localScale += new Vector3(0, delta * Time.deltaTime, 0);
    }

}
