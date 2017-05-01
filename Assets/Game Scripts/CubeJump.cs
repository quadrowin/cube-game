using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeJump : MonoBehaviour
{

    const string PREFS_RECORD_SCORES = "recordScores";

    const int STATE_NONE = 0;
    const int STATE_SCRATCHING = 1;

    const int STATE_JUMPING = 3;
    const int STATE_REINIT = 4;
    const int STATE_LOOSE_START = 5;
    const int STATE_LOOSE_STAND = 6;

    public bool active = false;
    public GameObject mainCube;
    public GameObject mainButtons;
    public GameObject looseButtons;
    public Text currentScoresView;
    public Text recordScoresView;
    /// <summary>
    /// Пол в момент начала прыжка
    /// </summary>
    private GameObject startFloor;
    private float startJumpTime;
    public float minScratch = 0.4f;
    public float scratchSpeed = 0.5f;
    public float jumpAcceleration = 3f;
    public float reinitDuration = 0.5f;
    public float reinitStartTime = 0;
    public float reinitDeltaX = 0;

    private int state = STATE_NONE;
    private int currentScores = 0;
    private int recordScores = 0;

    // like values in TapToPlay
    private Vector3 removeFloorPosition = new Vector3(-7, 11, 0);

    private void Start()
    {
        recordScores = PlayerPrefs.GetInt(PREFS_RECORD_SCORES);
        recordScoresView.text = "Record: " + recordScores;
    }

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

        if (mainCube.transform.localPosition.y < -5f)
        {
            // death
            playerLoose();
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


        Rigidbody rb = mainCube.GetComponent<Rigidbody>();

        if (state == STATE_JUMPING && mainCube.GetComponent<Rigidbody>().IsSleeping())
        {
            mainCube.transform.rotation = Quaternion.identity;
            reinitStartTime = Time.fixedTime;
            state = STATE_REINIT;
            reinitDeltaX = mainCube.transform.localPosition.x - mainCube.GetComponent<FloorReminder>().GetLastFloor().transform.localPosition.x;
            GameObject currentFloor = mainCube.GetComponent<FloorReminder>().GetLastFloor();
            if (currentFloor == startFloor)
            {
                print("same floor");
            }
            else
            {
                setGameScores(currentScores + 1);
            }
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
                Destroy(startFloor, 1);
                startFloor = currentFloor;
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
        if (mainCube.GetComponent<Rigidbody>().IsSleeping())
        {
            startJumpTime = Time.time;
            startFloor = mainCube.GetComponent<FloorReminder>().GetLastFloor();
            state = STATE_SCRATCHING;
        }
    }

    public void OnMouseUp()
    {
        if (state == STATE_SCRATCHING && mainCube.transform.localScale.y < 1)
        {
            state = STATE_JUMPING;
            float timeDelta = Mathf.Min(2, Time.time - startJumpTime) * 200;
            float force = timeDelta;
            mainCube.GetComponent<Rigidbody>().AddRelativeForce(force, force, 0);
            GetComponent<SpawnBlocks>().enabled = true;
        }
    }

    void playerLoose()
    {
        print("playerLoose");
        active = false;
        state = STATE_NONE;

        Rigidbody rb = mainCube.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        mainButtons.GetComponent<ScrollObjects>().MoveToPosition(Vector3.zero);
        looseButtons.SetActive(true);
        looseButtons.transform.localPosition = new Vector3(0, -300, looseButtons.transform.localPosition.z);
        looseButtons.GetComponent<ScrollObjects>().MoveToPosition(
            new Vector3(0, -50, looseButtons.transform.localPosition.z)
        );
    }

    void setGameScores(int newValue)
    {
        currentScores = newValue;
        currentScoresView.text = "Scores: " + currentScores;
        if (newValue > recordScores)
        {
            recordScores = newValue;
            recordScoresView.text = "Record: " + recordScores;
            PlayerPrefs.SetInt(PREFS_RECORD_SCORES, recordScores);
        }
    }

    void scratchCube(float delta)
    {
        mainCube.transform.localPosition += new Vector3(0, delta * Time.deltaTime, 0);
        mainCube.transform.localScale += new Vector3(0, delta * Time.deltaTime, 0);
    }

}
