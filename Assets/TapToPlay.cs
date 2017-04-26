using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour {

    const int STEP_NONE = 0;
    const int STEP_TO_ZERO = 1;
    const int STEP_TO_START = 2;

    public GameObject buttons;
    public GameObject startGameButton;
    public GameObject mainCube;
    public GameObject floorBlock;

    private bool clicked = false;
    private int gameStartStep = 0;
    private float gameStartTime = 0;
    private float gameStartDuration = 1f;
    private Quaternion zeroRoration = Quaternion.identity;
    private Vector3 zeroPosition = new Vector3(0, -1, 0);

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
                tr.rotation = Quaternion.Lerp(tr.rotation, zeroRoration, gameStartPast / gameStartDuration / 2);
                tr.position = Vector3.Lerp(tr.position, zeroPosition, gameStartPast / gameStartDuration / 2);
                return;
            }

            tr.rotation = zeroRoration;
            tr.position = zeroPosition;

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
                cubeTr.rotation = zeroRoration;
                cubeTr.position = Vector3.Lerp(cubeTr.position, startCubePosition, gameStartPast / gameStartDuration / 2);
                floorTr.position = Vector3.Lerp(floorTr.position, startFloorPosition, gameStartPast / gameStartDuration / 2);
                return;
            }

            print("Cube initialized 2");

            mainCube.GetComponent<Rigidbody>().position = startCubePosition;
            mainCube.GetComponent<Rigidbody>().rotation = zeroRoration;
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
            floor.transform.localPosition = startFloorPosition;
        } else
        {
            mainCube.transform.localPosition = Vector3.Lerp(mainCube.transform.localPosition, newCubePosition, timeDelta);
            floor.transform.localPosition = Vector3.Lerp(floor.transform.localPosition, startFloorPosition, timeDelta);
        }
    }

}
