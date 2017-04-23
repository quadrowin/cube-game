using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlay : MonoBehaviour {

    public GameObject buttons;
    public GameObject startGameButton;
    public GameObject mainCube;
    public GameObject floorBlock;

    private bool clicked = false;
    private bool inGameStart = false;
    private float gameStartTime = 0;
    private float gameStartDuration = 0.7f;
    private bool gameStarted = false;
    private Quaternion zeroRoration = Quaternion.identity;
    private Vector3 zeroPosition = new Vector3(0, 0, 0);

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
        gameStartTime = Time.fixedTime;
        inGameStart = true;
        gameStarted = false;
    }

    void Update()
    {
        if (!inGameStart || gameStarted)
        {
            return;
        }
        // Выравниваем главный куб
        print("Cube initializing..." + (Time.fixedTime - gameStartTime));
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

        print("Cube initialized");

        tr.rotation = zeroRoration;
        tr.position = zeroPosition;
        mainCube.GetComponent<Rigidbody>().position = zeroPosition;
        mainCube.GetComponent<Rigidbody>().rotation = zeroRoration;
        mainCube.GetComponent<Rigidbody>().useGravity = true;
        gameStarted = true;
        GetComponent<CubeJump>().active = true;
    }
	
}
