using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseManager : MonoBehaviour {

    const string PREFS_CHEESE_SCORES = "scheeseScores";

    public int CheeseScores = 0;

    void Start () {
        CheeseScores = PlayerPrefs.GetInt(PREFS_CHEESE_SCORES);
    }
	
	public void CheeseIncrement (int val) {
        CheeseScores += val;
        PlayerPrefs.SetInt(PREFS_CHEESE_SCORES, CheeseScores);
    }

    public void CheeseDecrement(int val)
    {
        CheeseScores -= val;
        PlayerPrefs.SetInt(PREFS_CHEESE_SCORES, CheeseScores);
    }

    public int GetCheeseScores()
    {
        return CheeseScores;
    }
}
