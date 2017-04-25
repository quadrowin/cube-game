using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour {

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

    private Vector3 startCubePosition = new Vector3(-2, 3, 0);
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
        gameStartStep = 1;
        gameStartTime = Time.fixedTime;
    }

    void Update()
    {
        if (gameStartStep == 1)
        {
            // Выравниваем главный куб по центру
            Transform tr = mainCube.GetComponent<Transform>();
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            float gameStartPast = Time.fixedTime - gameStartTime;
            if (gameStartPast < gameStartDuration)
            {
                mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                tr.rotation = Quaternion.Lerp(tr.rotation, zeroRoration, gameStartPast / gameStartDuration / 2);
                tr.position = Vector3.Lerp(tr.position, zeroPosition, gameStartPast / gameStartDuration / 2);
                return;
            }

            tr.rotation = zeroRoration;
            tr.position = zeroPosition;
            gameStartStep = 2;
            gameStartTime = Time.fixedTime;
            print("Cube initialized 1");
            return;
        }
        if (gameStartStep == 2)
        {
            // двигаем главный куб и первую плашку влево вверх
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Transform cubeTr = mainCube.GetComponent<Transform>();
            Transform floorTr = floorBlock.GetComponent<Transform>();
            mainCube.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            gameStartStep = 0;
            GetComponent<CubeJump>().active = true;
        }
    }
	
}
