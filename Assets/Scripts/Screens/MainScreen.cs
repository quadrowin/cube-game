using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour {

    public GameObject[] StartEnabled;
    public GameObject[] StartDisabled;

    public GameObject DebugInfo;

    public GameObject MainMenu;
    public Vector3 MainMenuNormalPos = new Vector3(0, 216, 0);
    public Vector3 MainMenuGamePos;

    public GameObject GameName;
    public Vector3 GameNameNormalPos;
    public Vector3 GameNameGamePos = new Vector3(0, 300, 0);

    public GameObject GameStatPanel;

    void Start () {
		foreach (var go in StartEnabled)
        {
            go.SetActive(true);
        }
        foreach (var go in StartDisabled)
        {
            go.SetActive(false);
        }
        MainMenu.GetComponent<ScrollObjects>().MoveToAnchorPosition(MainMenuNormalPos);
    }

    public void ShowMainMenu()
    {
        MainMenu.GetComponent<ScrollObjects>().MoveToAnchorPosition(MainMenuNormalPos);
    }

    public void ShowGameInterface()
    {
        GameStatPanel.SetActive(true);
    }

    public void HideMainMenu()
    {
        MainMenu.GetComponent<ScrollObjects>().MoveToAnchorPosition(MainMenuGamePos);
        GameName.GetComponent<ScrollObjects>().MoveToAnchorPosition(GameNameGamePos);
        DebugInfo.SetActive(false);
    }

    public void HideGameInterface()
    {
        GameStatPanel.SetActive(false);
    }

}
