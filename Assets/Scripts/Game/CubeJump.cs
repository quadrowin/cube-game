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
    public MainScreen MainScreen;

    public GameObject looseBackground;
    public GameObject looseButtons;
    public AudioClip looseSound;

    public AudioClip floorDownSound;
    public AudioClip eatingSound;
    public AudioClip jumpSound;
    public Text currentScoresView;
    public Text recordScoresView;
    public Text cheeseScoresView;

    public CheeseManager CheeseManager;

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
    private Vector3 jumpStartPosition = Vector3.zero;

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

        if (state == STATE_SCRATCHING)
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
        FloorReminder fr = mainCube.GetComponent<FloorReminder>();

        if (state == STATE_JUMPING && rb.IsSleeping())
        {
            reinitStartTime = Time.fixedTime;
            state = STATE_REINIT;
            reinitDeltaX = mainCube.transform.localPosition.x - fr.GetLastFloor().transform.localPosition.x;
            GameObject currentFloor = fr.GetLastFloor();
            if (currentFloor == startFloor)
            {
                print("same floor");
            }
            else
            {
                var jumpDeltaHeight = jumpStartPosition.z - mainCube.transform.localPosition.z;
                PlayGames.AddScoreToLeaderboard(GPGSIds.leaderboard_jumping_off_height, Mathf.RoundToInt(jumpDeltaHeight * 100));
                PlayGames.AddScoreToLeaderboard(GPGSIds.leaderboard_success_touchdowns, 1);
                PlayGames.IncrementAchievement(GPGSIds.achievement_accurate_jumper, 1);
                setGameScores(currentScores + 1);
                mainCube.GetComponent<AudioSource>().PlayOneShot(floorDownSound);

                if (currentScores >= 5)
                {
                    PlayGames.UnlockAchievement(GPGSIds.achievement_lucky_landing);
                }
                if (currentScores >= 10)
                {
                    PlayGames.UnlockAchievement(GPGSIds.achievement_professional_landing);
                }
            }
            return;
        }

        if (state == STATE_REINIT)
        {
            GameObject currentFloor = fr.GetLastFloor();
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

    public void OnCheeseTake()
    {
        CheeseManager.CheeseIncrement(1);
        var cheeseScores = CheeseManager.GetCheeseScores();
        cheeseScoresView.text = ": " + cheeseScores;
        mainCube.GetComponent<AudioSource>().PlayOneShot(eatingSound);
    }

    public void OnMouseDown()
    {
        if (state != STATE_NONE || !active)
        {
            return;
        }
        if (mainCube.GetComponent<Rigidbody>().IsSleeping())
        {
            jumpStartPosition = mainCube.transform.localPosition;
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
            mainCube.GetComponent<AudioSource>().PlayOneShot(jumpSound);
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

        looseBackground.GetComponent<FadeAnimation>().FadeIn();
        var anchPos = looseButtons.GetComponent<RectTransform>().anchoredPosition3D;
        looseButtons.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(anchPos.x, -300, anchPos.z);
        looseButtons.SetActive(true);
        looseButtons.GetComponent<ScrollObjects>().MoveToAnchorPosition(
            new Vector3(anchPos.x, 450, anchPos.z)
        );

        mainCube.GetComponent<AudioSource>().PlayOneShot(looseSound);
        MainScreen.HideGameInterface();
        MainScreen.ShowMainMenu();
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
        var curScaleY = mainCube.transform.localScale.y;
        var newScaleY = Mathf.Min(1, Mathf.Max(minScratch, curScaleY + delta * Time.deltaTime));
        var deltaY = newScaleY - curScaleY;

        mainCube.transform.localScale = new Vector3(1, newScaleY, 1);
        mainCube.transform.localPosition = new Vector3(
            mainCube.transform.localPosition.x, 
            mainCube.transform.localPosition.y + deltaY, 
            mainCube.transform.localPosition.z
        );
    }

}
