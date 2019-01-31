using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardScript : MonoBehaviour {

    public TextMeshProUGUI Score;
    // Use this for initialization
    void Start ()
    {
        Score.text = PlayerPrefs.GetString("Name", "NO SCORE") + " " + PlayerPrefs.GetInt("HighScore", 1000).ToString() + " Seconds.";
    }
	
	
}
