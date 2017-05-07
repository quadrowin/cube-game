using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseManager : MonoBehaviour {

    const string PREFS_CHEESE_SCORES = "scheeseScores";

    private int cheeseScores = 0;

    void Start () {
        cheeseScores = PlayerPrefs.GetInt(PREFS_CHEESE_SCORES);
    }
	
	public void CheeseIncrement () {
        cheeseScores++;
        PlayerPrefs.SetInt(PREFS_CHEESE_SCORES, cheeseScores);
    }

    public void CheeseDecrement(int val)
    {
        cheeseScores -= val;
        PlayerPrefs.SetInt(PREFS_CHEESE_SCORES, cheeseScores);
    }

    public int GetCheeseScores()
    {
        return cheeseScores;
    }
}
