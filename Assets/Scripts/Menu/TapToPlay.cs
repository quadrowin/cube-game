using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour {

    const int STEP_NONE = 0;
    const int STEP_TO_ZERO = 1;
    const int STEP_TO_START = 2;

    const int STUDY_STATE_NONE = 0;
    const int STUDY_STATE_ACTIVE = 1;
    const int STUDY_STATE_SHOWED = 2;

    public GameObject buttons;
    public GameObject gameName;
    public GameObject howToPlayHint;
    public GameObject startGameButton;
    public GameObject mainCube;
    public GameObject floorBlock;
    public GameObject currentScores;
    public GameObject recordScores;
    public GameObject cheeseTitle;
    public GameObject cheeseScores;
    public CheeseManager CheeseManager;

    private bool clicked = false;
    private int gameStartStep = 0;
    private float gameStartTime = 0;
    private float gameStartDuration = 1f;
    private Quaternion zeroRoration = Quaternion.identity;
    private Vector3 zeroPosition = new Vector3(0, -1, 0);
    private Vector3 gameNameInGamePosition = new Vector3(0, 300, 0);
    private int studyState = STUDY_STATE_NONE;

    // like values in CubeJump
    private Vector3 startCubePosition = new Vector3(-2, 2.5f, 0);
    private Vector3 startFloorPosition = new Vector3(-2, 1, 0);

    // Use this for initialization
    void OnMouseDown () {
        if (clicked)
        {
            return;
        }
        clicked = true;
        startGameButton.gameObject.SetActive(false);
        buttons.GetComponent<Animation>().Play("MainButtonsHide");
        floorBlock.GetComponent<Animation>().Play("FloorBlockDriveIn");
        mainCube.GetComponent<Animation>().Stop();
        mainCube.GetComponent<Rigidbody>().useGravity = false;
        mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        mainCube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameStartStep = STEP_TO_ZERO;
        gameStartTime = Time.fixedTime;
        howToPlayHint.SetActive(true);
    }

    private void OnMouseUp()
    {
        if (studyState == STUDY_STATE_NONE && clicked)
        {
            cheeseScores.GetComponent<Text>().text = ": " + CheeseManager.GetCheeseScores();
            studyState = STUDY_STATE_ACTIVE;
            howToPlayHint.SetActive(true);
            currentScores.SetActive(true);
            recordScores.SetActive(true);
            cheeseTitle.SetActive(true);
            cheeseScores.SetActive(true);
            return;
        }
        if (studyState == STUDY_STATE_ACTIVE)
        {
            studyState = STUDY_STATE_SHOWED;
            howToPlayHint.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (gameStartStep == STEP_TO_ZERO)
        {
            // Выравниваем главный куб по центру
            Transform tr = mainCube.GetComponent<Transform>();
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            mainCube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            float gameStartPast = Time.fixedTime - gameStartTime;
            if (gameStartPast < gameStartDuration)
            {
                float timeFactor = gameStartPast / gameStartDuration / 2;
                tr.localRotation = Quaternion.Lerp(tr.localRotation, zeroRoration, timeFactor);
                tr.localPosition = Vector3.Lerp(tr.localPosition, zeroPosition, timeFactor);
                gameName.GetComponent<RectTransform>().localPosition = Vector3.Lerp(gameName.GetComponent<RectTransform>().localPosition, gameNameInGamePosition, timeFactor);
                return;
            }

            tr.SetPositionAndRotation(zeroPosition, zeroRoration);

            gameStartStep = STEP_TO_START;
            gameStartTime = Time.fixedTime;
            print("Cube initialized 1");
            return;
        }
        if (gameStartStep == STEP_TO_START)
        {
            // двигаем главный куб и первую плашку влево вверх
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Transform cubeTr = mainCube.GetComponent<Transform>();
            Transform floorTr = floorBlock.GetComponent<Transform>();
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            mainCube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            float gameStartPast = Time.fixedTime - gameStartTime;
            if (gameStartPast < gameStartDuration)
            {
                float timeFactor = gameStartPast / gameStartDuration / 2;
                cubeTr.localRotation = zeroRoration;
                cubeTr.localPosition = Vector3.Lerp(cubeTr.localPosition, startCubePosition, timeFactor);
                floorTr.localPosition = Vector3.Lerp(floorTr.localPosition, startFloorPosition, timeFactor);
                return;
            }

            print("Cube initialized 2");

            mainCube.transform.localPosition = startCubePosition;
            mainCube.transform.localRotation = zeroRoration;
            mainCube.GetComponent<Rigidbody>().useGravity = true;
            gameStartStep = STEP_NONE;
            GetComponent<CubeJump>().active = true;
            GetComponent<SpawnBlocks>().enabled = true;
        }
    }

    public void reinitUpdate(float timeDelta, float deltaX)
    {
        GameObject floor = mainCube.GetComponent<FloorReminder>().GetLastFloor();
        if (!floor)
        {
            floor = floorBlock;
        }
        var newCubePosition = new Vector3(
            startCubePosition.x + deltaX,
            startCubePosition.y,
            startCubePosition.z
        );
        if (timeDelta >= 1)
        {
            mainCube.transform.localPosition = newCubePosition;
            mainCube.transform.localRotation = Quaternion.identity;
            floor.transform.localPosition = startFloorPosition;
        } else
        {
            mainCube.transform.localPosition = Vector3.Lerp(mainCube.transform.localPosition, newCubePosition, timeDelta);
            mainCube.transform.localRotation = Quaternion.Lerp(mainCube.transform.localRotation, Quaternion.identity, timeDelta);
            floor.transform.localPosition = Vector3.Lerp(floor.transform.localPosition, startFloorPosition, timeDelta);
        }
    }

}
